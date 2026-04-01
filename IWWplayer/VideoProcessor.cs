using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace IWWplayer
{
    class VideoProcessor
    {
        private readonly int NUMBEROFFRAMES = 10; //number of frames to analyze for windowboxing, EXPONENTIALY INCREASES PROCESSING TIME!!!
        private readonly int BLACKPIXELTHRESHOLD = 3; //threshold for counting a pixel as black, can be adjusted for different videos, but should be low to avoid cutting into the actual video content
        private readonly bool LETTERBOXING = true;
        private readonly bool PILLARBOXING = false;

        private String processedVideoFile;
        private bool removeWindowbox = false;
        private int letterboxingHeight = 0;
        private int pillarboxingWidth = 0;


        public Mat processFrame(Mat frame, String videoFile)
        {
            if (removeWindowbox)
            {
                if (videoFile != processedVideoFile)
                {
                    setWindowboxing(videoFile);
                }
                return removeWindowboxing(frame);
            }
            else
            {
                return frame;
            }
        }

        public void toggleWindowboxing(String videoFile)
        {
            if (removeWindowbox)
            {
                removeWindowbox = false;
                if (videoFile != processedVideoFile )
                {
                    resetWindowboxing();
                }
            }
            else
            {
                removeWindowbox = true;
                if (videoFile != null && videoFile != processedVideoFile)
                {
                    setWindowboxing(videoFile);
                }
            }
        }

        private Mat removeWindowboxing(Mat frame)
        {
            if (letterboxingHeight != 0 || pillarboxingWidth != 0)
            {
                Rectangle windowBoxing = new Rectangle(pillarboxingWidth, letterboxingHeight, frame.Width - pillarboxingWidth * 2, frame.Height - letterboxingHeight * 2);
                Mat croppedFrame = new Mat(frame, windowBoxing);
                frame.Dispose();
                return croppedFrame;
            }
            else
            {
                return frame;
            }
        }

        private int findSmallestBoxSize(List <Mat> frames, bool letterboxing)
        {
            int smallestBoxSize = int.MaxValue;
            foreach (Mat frame in frames)
            {
                if (smallestBoxSize > CountBlackPixels(frame, letterboxing))
                {
                    smallestBoxSize = CountBlackPixels(frame, letterboxing);
                }
            }
            if(smallestBoxSize == int.MaxValue)
            {
                smallestBoxSize = 0;
            }

            return smallestBoxSize;
        }

        private void resetWindowboxing()
        {
            letterboxingHeight = 0;
            pillarboxingWidth = 0;
        }

        private void setWindowboxing(String videofile)
        {
            List <Mat> frames = gatherFrames(videofile, NUMBEROFFRAMES);
            letterboxingHeight = findSmallestBoxSize(frames, LETTERBOXING);
            pillarboxingWidth = findSmallestBoxSize(frames, PILLARBOXING);
            processedVideoFile = videofile;
        }

        private List<Mat> gatherFrames(String videoFile, int numberOfFrames)
        {
            List<Mat> frames = new List<Mat>();
            using (VideoCapture capture = new VideoCapture(videoFile))
            {
                double totalFrames = capture.Get(Emgu.CV.CvEnum.CapProp.FrameCount);
                double frameInterval = totalFrames / numberOfFrames;

                for (int i = 0; i < numberOfFrames; i++)
                {
                    double frameNumber = i * frameInterval;
                    capture.Set(Emgu.CV.CvEnum.CapProp.PosFrames, frameNumber);

                    Mat frame = new Mat();
                    capture.Read(frame);
                    frames.Add(frame);
                }
                capture.Dispose();
            }
            return frames;
        }

        private int CountBlackPixels(Mat frame, bool letterboxing)
        {
            int count = 0;
            int maxIndex = letterboxing ? frame.Height : frame.Width;
            Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();

            Func<int, Bgr> getPixel;
            if (letterboxing)
            {
                getPixel = (index) => image[index, frame.Width / 2];
            }
            else
            {
                getPixel = (index) => image[frame.Height / 2, index];
            }

            for (int index = 0; index < maxIndex; index++)
            {
                Bgr pixel = getPixel(index);
                if (pixel.Blue < BLACKPIXELTHRESHOLD || pixel.Green < BLACKPIXELTHRESHOLD || pixel.Red < BLACKPIXELTHRESHOLD)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            return count;
        }

    }
}
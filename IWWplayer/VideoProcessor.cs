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

namespace IWWplayer
{
    class VideoProcessor
    {
        private int BLACKPIXELTHRESHOLD = 3;
        private int letterboxingHeight = 0;
        private int pillarboxingWidth = 0;

        public Mat removeWindowboxing(Mat frame)
        {
            if (letterboxingHeight != 0 || pillarboxingWidth != 0)
            {
                Rectangle windowBoxing = new Rectangle(pillarboxingWidth, letterboxingHeight, frame.Width - pillarboxingWidth * 2, frame.Height - letterboxingHeight * 2);
                Mat croppedFrame = new Mat(frame, windowBoxing);
                return croppedFrame;
            }
            else
            {
                return frame;
            }
        }

        private int findSmallestBoxSize(String videoFile, bool letterboxing)
        {
            int smallestBoxSize = int.MaxValue;
            int numberOfFrames = 10;
            List<Mat> frames = gatherFrames(videoFile, numberOfFrames);
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

        public void resetWindowboxing()
        {
            letterboxingHeight = 0;
            pillarboxingWidth = 0;
        }

        public void setWindowboxing(String videofile)
        {
            letterboxingHeight = findSmallestBoxSize(videofile, true);
            pillarboxingWidth = findSmallestBoxSize(videofile, false);
        }

        private List<Mat> gatherFrames(String videoFile, int numberOfFrames)
        {
            List<Mat> frames = new List<Mat>();
            using (VideoCapture capture = new VideoCapture(videoFile))
            {
                if (!capture.IsOpened)
                {
                    throw new Exception("Could not open video file.");
                }

                double totalFrames = capture.Get(Emgu.CV.CvEnum.CapProp.FrameCount);
                double frameInterval = totalFrames / numberOfFrames; //magicnm

                for (int i = 0; i < numberOfFrames; i++)
                {
                    double frameNumber = i * frameInterval;
                    capture.Set(Emgu.CV.CvEnum.CapProp.PosFrames, frameNumber);

                    Mat frame = new Mat();
                    capture.Read(frame);
                    frames.Add(frame);
                }
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
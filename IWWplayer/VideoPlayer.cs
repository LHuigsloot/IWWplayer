using Emgu.CV;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IWWplayer
{

    internal class VideoPlayer
    {
        private readonly double ASECOND_IN_MILLISECONDS = 1000.0;

        private VideoCapture capture;
        private bool captureInProgress;
        private Stopwatch stopwatch;
        private double frameTime;
        private List<Mat> frameBuffer = new List<Mat>();

        public VideoPlayer()
        {
            stopwatch = new Stopwatch();
        }

        public void prepareCapture(String videoFile)
        {
            capture = new VideoCapture(videoFile);
            frameTime = ASECOND_IN_MILLISECONDS / capture.Get(Emgu.CV.CvEnum.CapProp.Fps);
            startCapture();
        }

        private void startCapture()
        {
            if (capture != null)
            {
                capture.Start();
                captureInProgress = true;
                capture.ImageGrabbed += addFrameToBuffer;
            }
        }

        public void pauseCapture()
        {
            if (capture != null && captureInProgress)
            {
                capture.Pause();
                captureInProgress = false;
            }
        }

        public void playVideo(PictureBox videoBox)
        {
            lock (frameBuffer)
            {
                if (capture != null && frameBuffer.Count > 0)
                {
                    displayAtFramerate(videoBox);
                }
            }
        }

        private void addFrameToBuffer(object sender, EventArgs e)
        {
            if (frameBuffer.Count <= 100) //TODO: MAGICNUMBER
            {
                Mat frame = new Mat();
                capture.Retrieve(frame);
                if (!frame.IsEmpty)
                {
                    frameBuffer.Add(frame);
                }
            }
            else
            {
                pauseCapture();
            }
        }

        private void displayAtFramerate(PictureBox videoBox)
        {
            if (capture != null)
            {
                stopwatch.Start();
                if (stopwatch.ElapsedMilliseconds >= frameTime)
                {
                    stopwatch.Reset();
                    Mat frame = frameBuffer.First();
                    crossThreadSafeDisplayFrame(frame, videoBox);
                    frameBuffer.RemoveAt(0);
                    frame.Dispose();
                    if (frameBuffer.Count <= 100) //TODO: MAGINNUMBER
                    {
                       startCapture();
                    }
                }

            }
        }

        private void crossThreadSafeDisplayFrame(Mat frame, PictureBox videoBox)
        {
            if (videoBox.InvokeRequired)
            {
                videoBox.Invoke(new Action(() =>
                {
                    displayFrame(frame, videoBox);
                }));
            }
            else
            {
                displayFrame(frame, videoBox);
            }
        }

        private void displayFrame(Mat frame, PictureBox videoBox)
        {
            var currentImage = videoBox.Image;
            videoBox.Image = frame.ToBitmap();
            currentImage?.Dispose();
        }
    }
}

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

        private VideoCapture capture;
        private bool captureInProgress;
        private Stopwatch stopwatch;
        private double frameTime;

        public VideoPlayer()
        {
            stopwatch = new Stopwatch();
        }

        public void loadVideo()
        {
            pauseVideo();
            if (capture != null)
            {
                capture.Dispose();
                capture = null;
            }
        }

        public void playVideo(String videoFile)
        {
            if (!captureInProgress)
            {
                if (capture == null)
                {
                    startNewCapture(videoFile);
                }
                else
                {
                    startCapture();
                }
            }
        }

        public void pauseVideo()
        {
            if (capture != null && captureInProgress)
            {
                capture.ImageGrabbed -= processFrame;
                capture.Pause();
                captureInProgress = false;
                stopwatch.Stop();
            }
        }

        private void startNewCapture(String videoFile)
        {
            capture = new VideoCapture(videoFile);
            double aSecondInMilliSeconds = 1000.0;
            frameTime = aSecondInMilliSeconds / capture.Get(Emgu.CV.CvEnum.CapProp.Fps);
            startCapture();
        }

        private void startCapture()
        {
            capture.Start();
            captureInProgress = true;
            capture.ImageGrabbed += processFrame;
        }

        //1. gather frames into buffer
        //2. process frames if needed.
        //3. display frames at correct time.

        //HHMMMMM, Where should this be done?
        private void processFrame(object sender, EventArgs e)
        {
            if (capture != null && capture.Ptr != IntPtr.Zero && captureInProgress)
            {
                stopwatch.Start();

                Mat frame = new Mat();
                capture.Retrieve(frame);

                //FIX: frame = videoProcessor.processFrame(frame);
           
                //FIX: crossThreadSafeDisplayFrame(frame);

                double elapsedTime = stopwatch.Elapsed.TotalMilliseconds;
                double remainingTime = frameTime - elapsedTime;
                if (remainingTime > 0)
                {
                    System.Threading.Thread.Sleep((int)remainingTime);
                }
                frame.Dispose();

                stopwatch.Reset();
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

        //Things to implement:
        //A buffer?
        //Multithreading for processing and displaying.

    }
}

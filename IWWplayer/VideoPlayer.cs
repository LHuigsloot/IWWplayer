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
        private PictureBox videoBox;
        private String videoFile;
        private bool removeWindowboxing = false;
        private Stopwatch stopwatch;
        private double frameTime;

        private VideoProcessor videoProcessor;

        public VideoPlayer(PictureBox videoBox, VideoProcessor videoProcessor)
        {
            this.videoBox = videoBox;
            this.videoProcessor = videoProcessor;
            stopwatch = new Stopwatch();
        }

        public bool videoFileLoaded()
        {
            return videoFile != null;
        }

        public String getVideoFile()
        {
            return videoFile;
        }

        public void loadVideo()
        {
            pauseMedia();
            if (capture != null)
            {
                capture.Dispose();
                capture = null;
            }
            if (videoFile != null)
            {
                videoFile = null;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP4 files (*.mp4)|*.mp4";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                videoFile = openFileDialog.FileName;
            }
        }

        public void playVideo()
        {
            if (!captureInProgress)
            {
                if (capture == null)
                {
                    startNewCapture();
                }
                else
                {
                    startCapture();
                }
            }
        }

        public void pauseMedia()
        {
            if (capture != null && captureInProgress)
            {
                capture.ImageGrabbed -= processFrame;
                capture.Pause();
                captureInProgress = false;
                stopwatch.Stop();
            }
        }

        public void toggleWindowboxing()
        {
            removeWindowboxing = !removeWindowboxing;
            if (removeWindowboxing)
            {
                videoProcessor.resetWindowboxing();
                videoProcessor.setWindowboxing(videoFile);
            }
        }

        private void startNewCapture()
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

        private void processFrame(object sender, EventArgs e)
        {
            if (capture != null && capture.Ptr != IntPtr.Zero && captureInProgress)
            {
                stopwatch.Start();
                Mat frame = new Mat();
                capture.Retrieve(frame);

                if (removeWindowboxing)
                {
                    frame = videoProcessor.removeWindowboxing(frame);
                }
           
                crossThreadSafeDisplayFrame(frame);

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

        private void crossThreadSafeDisplayFrame(Mat frame)
        {
            if (videoBox.InvokeRequired)
            {
                videoBox.Invoke(new Action(() =>
                {
                    displayFrame(frame);
                }));
            }
            else
            {
                displayFrame(frame);
            }
        }

        private void displayFrame(Mat frame)
        {
            var currentImage = videoBox.Image;
            videoBox.Image = frame.ToBitmap();
            currentImage?.Dispose();
        }

    }
}

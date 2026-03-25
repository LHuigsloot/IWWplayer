using Emgu.CV;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IWWplayer
{

    internal class VideoPlayer
    {

        private VideoCapture capture;
        private bool captureInProgress;
        private ImageBox videoBox;
        private String videoFile;
        private bool removeWindowboxing = false;
        private Stopwatch stopwatch;
        private double frameTime;

        private VideoProcessor videoProcessor;

        public VideoPlayer(ImageBox videoBox, VideoProcessor videoProcessor)
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
                    startCapture();
                }
                else
                {
                    resumeCapture();
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

        private void startCapture()
        {
            capture = new VideoCapture(videoFile);
            double aSecondInMilliSeconds = 1000.0;
            frameTime = aSecondInMilliSeconds / capture.Get(Emgu.CV.CvEnum.CapProp.Fps);
            resumeCapture();
        }

        private void resumeCapture()
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
           
                videoBox.Image = frame;

                double elapsedTime = stopwatch.Elapsed.TotalMilliseconds;
                double remainingTime = frameTime - elapsedTime;
                if (remainingTime > 0)
                {
                    System.Threading.Thread.Sleep((int)remainingTime);
                }
                stopwatch.Reset();
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

    }
}

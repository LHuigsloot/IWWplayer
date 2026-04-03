using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IWWplayer
{
    internal class VideoController
    {
        private VideoPlayer videoPlayer;
        private VideoProcessor videoProcessor;
        private PictureBox videoBox;

        private String videoFile;
        private bool playingVideo = false;

        public VideoController(PictureBox videoBox)
        {
            this.videoPlayer = new VideoPlayer();
            this.videoProcessor = new VideoProcessor();
            this.videoBox = videoBox;
        }

        public void playVideo()
        {
            if (videoFileLoaded())
            {
                playingVideo = true;
                Task.Run(() => videoPlayingLoop());
            } 
        }

        public void pauseVideo()
        {
            playingVideo = false;
            videoPlayer.pauseCapture();
        }

        public bool videoFileLoaded()
        {
            return videoFile != null;
        }

        public String getVideoFile()
        {
            return videoFile;
        }

        public void loadVideoFile()
        {
            if (videoFileLoaded())
            {
                videoPlayer.pauseCapture();
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP4 files (*.mp4)|*.mp4";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                videoFile = openFileDialog.FileName;
            }
            videoPlayer.prepareCapture(videoFile);
        }

        public void toggleWindowboxing()
        {
            videoProcessor.toggleWindowboxing(videoFile);
        }

        private void videoPlayingLoop()
        {
            while (playingVideo)
            {
                videoPlayer.playVideo(videoBox);
            }
        }

    }
}

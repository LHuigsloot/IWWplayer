using System;
using System.Collections.Generic;
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

        public VideoController(VideoPlayer videoPlayer, VideoProcessor videoProcessor, PictureBox videoBox)
        {
            this.videoPlayer = videoPlayer;
            this.videoProcessor = videoProcessor;
            this.videoBox = videoBox;

        }

        public void playVideo()
        {
            if (videoFileLoaded())
            {
                videoPlayer.playVideo(videoFile);
            }
        }

        public void pauseVideo()
        {
            videoPlayer.pauseVideo();
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP4 files (*.mp4)|*.mp4";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                videoFile = openFileDialog.FileName;
            }
        }

        public void toggleWindowboxing()
        {
            videoProcessor.toggleWindowboxing(videoFile);
        }
    }
}

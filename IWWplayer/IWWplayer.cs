using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using System.Text.RegularExpressions;
using System.Diagnostics;
using WMPLib;


namespace IWWplayer
{
    public partial class IWWplayer : Form
    {
        private VideoPlayer videoPlayer;
        private VideoProcessor videoProcessor;
        private WindowsMediaPlayer audioPlayer;
        
        public IWWplayer()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            videoProcessor = new VideoProcessor();
            videoPlayer = new VideoPlayer(imageBox1, videoProcessor);
            audioPlayer = new WindowsMediaPlayer();
        }

        private void removeWindowboxing_CheckedChanged(object sender, EventArgs e)
        {
            videoPlayer.toggleWindowboxing();
        }

        private void loadMedia_Click(object sender, EventArgs e)
        {
            videoPlayer.loadVideo();
            //audioPlayer is still WIP. Comments: Needs to be on a separate thread for performance.
            //comments: needs a separate class and better integration.
            audioPlayer.URL = videoPlayer.getVideoFile();
            audioPlayer.controls.stop();
            if (videoPlayer.videoFileLoaded())
            {
                playMedia.Enabled = true;
            }
        }

        private void playMedia_Click(object sender, EventArgs e)
        {
            videoPlayer.playVideo();
            //WIP
            audioPlayer.controls.play();
        }

        private void pauseMedia_Click(object sender, EventArgs e)
        {
            videoPlayer.pauseMedia();
            //WIP
            audioPlayer.controls.pause();
        }
    }
}



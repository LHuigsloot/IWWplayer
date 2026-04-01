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
        private VideoController videoController;
        private WindowsMediaPlayer audioPlayer;
        
        public IWWplayer()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            videoController = new VideoController(new VideoPlayer(), new VideoProcessor(), imageBox1);
            audioPlayer = new WindowsMediaPlayer();
        }

        private void removeWindowboxing_CheckedChanged(object sender, EventArgs e)
        {
            videoController.toggleWindowboxing();
        }

        private void loadMedia_Click(object sender, EventArgs e)
        {
            videoController.loadVideoFile();
            //audioPlayer is still WIP. Comments: Needs to be on a separate thread for performance.
            //comments: needs a separate class and better integration.
            audioPlayer.URL = videoController.getVideoFile();
            audioPlayer.controls.stop();
            if (videoController.videoFileLoaded())
            {
                playMedia.Enabled = true;
            }
        }

        private void playMedia_Click(object sender, EventArgs e)
        {
            videoController.playVideo();
            //WIP
            audioPlayer.controls.play();
        }

        private void pauseMedia_Click(object sender, EventArgs e)
        {
            videoController.pauseVideo();
            //WIP
            audioPlayer.controls.pause();
        }
    }
}



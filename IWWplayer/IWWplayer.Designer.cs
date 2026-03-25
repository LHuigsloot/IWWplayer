
namespace IWWplayer
{
    partial class IWWplayer
    {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mediaControlsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.removeWindowboxing = new System.Windows.Forms.CheckBox();
            this.loadMedia = new System.Windows.Forms.Button();
            this.playMedia = new System.Windows.Forms.Button();
            this.pauseMedia = new System.Windows.Forms.Button();
            this.mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.mediaControlsPanel.SuspendLayout();
            this.mainLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mediaControlsPanel
            // 
            this.mediaControlsPanel.Controls.Add(this.removeWindowboxing);
            this.mediaControlsPanel.Controls.Add(this.loadMedia);
            this.mediaControlsPanel.Controls.Add(this.playMedia);
            this.mediaControlsPanel.Controls.Add(this.pauseMedia);
            this.mediaControlsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediaControlsPanel.Location = new System.Drawing.Point(3, 408);
            this.mediaControlsPanel.Name = "mediaControlsPanel";
            this.mediaControlsPanel.Size = new System.Drawing.Size(794, 39);
            this.mediaControlsPanel.TabIndex = 3;
            // 
            // removeWindowboxing
            // 
            this.removeWindowboxing.AutoSize = true;
            this.removeWindowboxing.Location = new System.Drawing.Point(3, 3);
            this.removeWindowboxing.Name = "removeWindowboxing";
            this.removeWindowboxing.Size = new System.Drawing.Size(136, 17);
            this.removeWindowboxing.TabIndex = 0;
            this.removeWindowboxing.Text = "Remove windowboxing";
            this.removeWindowboxing.UseVisualStyleBackColor = true;
            this.removeWindowboxing.CheckedChanged += new System.EventHandler(this.removeWindowboxing_CheckedChanged);
            // 
            // loadMedia
            // 
            this.loadMedia.AutoSize = true;
            this.loadMedia.Location = new System.Drawing.Point(145, 3);
            this.loadMedia.Name = "loadMedia";
            this.loadMedia.Size = new System.Drawing.Size(75, 23);
            this.loadMedia.TabIndex = 1;
            this.loadMedia.Text = "Load media";
            this.loadMedia.UseVisualStyleBackColor = true;
            this.loadMedia.Click += new System.EventHandler(this.loadMedia_Click);
            // 
            // playMedia
            // 
            this.playMedia.Enabled = false;
            this.playMedia.Location = new System.Drawing.Point(226, 3);
            this.playMedia.Name = "playMedia";
            this.playMedia.Size = new System.Drawing.Size(75, 23);
            this.playMedia.TabIndex = 3;
            this.playMedia.Text = "Play media";
            this.playMedia.UseVisualStyleBackColor = true;
            this.playMedia.Click += new System.EventHandler(this.playMedia_Click);
            // 
            // pauseMedia
            // 
            this.pauseMedia.AutoSize = true;
            this.pauseMedia.Location = new System.Drawing.Point(307, 3);
            this.pauseMedia.Name = "pauseMedia";
            this.pauseMedia.Size = new System.Drawing.Size(78, 23);
            this.pauseMedia.TabIndex = 2;
            this.pauseMedia.Text = "Pause media";
            this.pauseMedia.UseVisualStyleBackColor = true;
            this.pauseMedia.Click += new System.EventHandler(this.pauseMedia_Click);
            // 
            // mainLayoutPanel
            // 
            this.mainLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainLayoutPanel.AutoSize = true;
            this.mainLayoutPanel.ColumnCount = 1;
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.mainLayoutPanel.Controls.Add(this.mediaControlsPanel, 0, 1);
            this.mainLayoutPanel.Controls.Add(this.imageBox1, 0, 0);
            this.mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainLayoutPanel.Name = "mainLayoutPanel";
            this.mainLayoutPanel.RowCount = 2;
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainLayoutPanel.Size = new System.Drawing.Size(800, 450);
            this.mainLayoutPanel.TabIndex = 0;
            // 
            // imageBox1
            // 
            this.imageBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox1.Location = new System.Drawing.Point(3, 3);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(794, 399);
            this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // IWWplayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainLayoutPanel);
            this.Name = "IWWplayer";
            this.Text = "IWWplayer";
            this.mediaControlsPanel.ResumeLayout(false);
            this.mediaControlsPanel.PerformLayout();
            this.mainLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel mediaControlsPanel;
        private System.Windows.Forms.CheckBox removeWindowboxing;
        private System.Windows.Forms.Button loadMedia;
        private System.Windows.Forms.Button pauseMedia;
        private System.Windows.Forms.TableLayoutPanel mainLayoutPanel;
        private System.Windows.Forms.Button playMedia;
        private Emgu.CV.UI.ImageBox imageBox1;
    }
}


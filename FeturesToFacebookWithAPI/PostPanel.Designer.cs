namespace EX
{
    partial class PostPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label updateTimeLabel;
            System.Windows.Forms.Label messageLabel;
            this.panelPost1 = new System.Windows.Forms.Panel();
            this.PostUserPicture = new System.Windows.Forms.PictureBox();
            this.PostCreator = new System.Windows.Forms.Label();
            this.PostCreatedTime = new System.Windows.Forms.Label();
            this.PostIcon = new System.Windows.Forms.PictureBox();
            this.PostMessege = new System.Windows.Forms.Label();
            this.PostUpdateTime = new System.Windows.Forms.Label();
            this.PostPictureBox = new System.Windows.Forms.PictureBox();
            updateTimeLabel = new System.Windows.Forms.Label();
            messageLabel = new System.Windows.Forms.Label();
            this.panelPost1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PostUserPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // updateTimeLabel
            // 
            updateTimeLabel.AutoSize = true;
            updateTimeLabel.Location = new System.Drawing.Point(237, 138);
            updateTimeLabel.Name = "updateTimeLabel";
            updateTimeLabel.Size = new System.Drawing.Size(71, 13);
            updateTimeLabel.TabIndex = 6;
            updateTimeLabel.Text = "Update Time:";
            // 
            // messageLabel
            // 
            messageLabel.AutoSize = true;
            messageLabel.Location = new System.Drawing.Point(50, 92);
            messageLabel.Name = "messageLabel";
            messageLabel.Size = new System.Drawing.Size(53, 13);
            messageLabel.TabIndex = 4;
            messageLabel.Text = "Message:";
            // 
            // panelPost1
            // 
            this.panelPost1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelPost1.Controls.Add(this.PostUserPicture);
            this.panelPost1.Controls.Add(this.PostCreator);
            this.panelPost1.Controls.Add(this.PostCreatedTime);
            this.panelPost1.Controls.Add(this.PostIcon);
            this.panelPost1.Controls.Add(updateTimeLabel);
            this.panelPost1.Controls.Add(messageLabel);
            this.panelPost1.Controls.Add(this.PostMessege);
            this.panelPost1.Controls.Add(this.PostUpdateTime);
            this.panelPost1.Controls.Add(this.PostPictureBox);
            this.panelPost1.Location = new System.Drawing.Point(3, 3);
            this.panelPost1.Name = "panelPost1";
            this.panelPost1.Size = new System.Drawing.Size(411, 160);
            this.panelPost1.TabIndex = 9;
            // 
            // PostUserPicture
            // 
            this.PostUserPicture.Location = new System.Drawing.Point(11, 9);
            this.PostUserPicture.Name = "PostUserPicture";
            this.PostUserPicture.Size = new System.Drawing.Size(45, 40);
            this.PostUserPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PostUserPicture.TabIndex = 21;
            this.PostUserPicture.TabStop = false;
            // 
            // PostCreator
            // 
            this.PostCreator.AutoSize = true;
            this.PostCreator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.PostCreator.ForeColor = System.Drawing.Color.SteelBlue;
            this.PostCreator.Location = new System.Drawing.Point(62, 13);
            this.PostCreator.Name = "PostCreator";
            this.PostCreator.Size = new System.Drawing.Size(41, 13);
            this.PostCreator.TabIndex = 20;
            this.PostCreator.Text = "label1";
            // 
            // PostCreatedTime
            // 
            this.PostCreatedTime.Location = new System.Drawing.Point(80, 26);
            this.PostCreatedTime.Name = "PostCreatedTime";
            this.PostCreatedTime.Size = new System.Drawing.Size(100, 23);
            this.PostCreatedTime.TabIndex = 11;
            this.PostCreatedTime.Text = "label3";
            // 
            // PostIcon
            // 
            this.PostIcon.Location = new System.Drawing.Point(382, 3);
            this.PostIcon.Name = "PostIcon";
            this.PostIcon.Size = new System.Drawing.Size(26, 23);
            this.PostIcon.TabIndex = 13;
            this.PostIcon.TabStop = false;
            // 
            // PostMessege
            // 
            this.PostMessege.Location = new System.Drawing.Point(129, 92);
            this.PostMessege.Name = "PostMessege";
            this.PostMessege.Size = new System.Drawing.Size(135, 46);
            this.PostMessege.TabIndex = 15;
            this.PostMessege.Text = "label3";
            // 
            // PostUpdateTime
            // 
            this.PostUpdateTime.Location = new System.Drawing.Point(314, 138);
            this.PostUpdateTime.Name = "PostUpdateTime";
            this.PostUpdateTime.Size = new System.Drawing.Size(81, 13);
            this.PostUpdateTime.TabIndex = 19;
            this.PostUpdateTime.Text = "label3";
            // 
            // PostPictureBox
            // 
            this.PostPictureBox.Location = new System.Drawing.Point(270, 38);
            this.PostPictureBox.Name = "PostPictureBox";
            this.PostPictureBox.Size = new System.Drawing.Size(125, 92);
            this.PostPictureBox.TabIndex = 17;
            this.PostPictureBox.TabStop = false;
            // 
            // PostPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelPost1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PostPanel";
            this.Size = new System.Drawing.Size(415, 166);
            this.panelPost1.ResumeLayout(false);
            this.panelPost1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PostUserPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPost1;
        private System.Windows.Forms.PictureBox PostUserPicture;
        private System.Windows.Forms.Label PostCreator;
        private System.Windows.Forms.Label PostCreatedTime;
        private System.Windows.Forms.PictureBox PostIcon;
        private System.Windows.Forms.Label PostMessege;
        private System.Windows.Forms.Label PostUpdateTime;
        private System.Windows.Forms.PictureBox PostPictureBox;
    }
}

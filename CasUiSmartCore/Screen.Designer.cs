
using CASTerms;

namespace CAS.UI
{
    partial class Screen
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Screen));
            this.panelImageContainer = new System.Windows.Forms.Panel();
            this.linkLabelContinueLoading = new System.Windows.Forms.LinkLabel();
            this.labelWelcomeText = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.copyrightControlWide1 = new CAS.UI.UIControls.Auxiliary.CopyrightControl();
            this.panelImageContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelImageContainer
            // 
            this.panelImageContainer.Controls.Add(this.linkLabelContinueLoading);
            this.panelImageContainer.Controls.Add(this.labelWelcomeText);
            this.panelImageContainer.Controls.Add(this.pictureBox1);
            this.panelImageContainer.Location = new System.Drawing.Point(205, 12);
            this.panelImageContainer.Name = "panelImageContainer";
            this.panelImageContainer.Size = new System.Drawing.Size(731, 642);
            this.panelImageContainer.TabIndex = 0;
            // 
            // linkLabelContinueLoading
            // 
            this.linkLabelContinueLoading.ActiveLinkColor = System.Drawing.Color.White;
            this.linkLabelContinueLoading.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabelContinueLoading.ForeColor = System.Drawing.Color.White;
            this.linkLabelContinueLoading.LinkColor = System.Drawing.Color.White;
            this.linkLabelContinueLoading.Location = new System.Drawing.Point(228, 565);
            this.linkLabelContinueLoading.Name = "linkLabelContinueLoading";
            this.linkLabelContinueLoading.Size = new System.Drawing.Size(289, 37);
            this.linkLabelContinueLoading.TabIndex = 0;
            this.linkLabelContinueLoading.TabStop = true;
            this.linkLabelContinueLoading.Text = "press here to continue";
            this.linkLabelContinueLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelContinueLoading.VisitedLinkColor = System.Drawing.Color.White;
            this.linkLabelContinueLoading.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelContinueLoading_LinkClicked);
            // 
            // labelWelcomeText
            // 
            this.labelWelcomeText.Font = new System.Drawing.Font("Verdana", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWelcomeText.ForeColor = System.Drawing.Color.White;
            this.labelWelcomeText.Location = new System.Drawing.Point(3, 407);
            this.labelWelcomeText.Name = "labelWelcomeText";
            this.labelWelcomeText.Size = new System.Drawing.Size(725, 112);
            this.labelWelcomeText.TabIndex = 1;
            this.labelWelcomeText.Text = "Welcome to\r\nContinuing Airworthiness System";
            this.labelWelcomeText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CAS.UI.Properties.Resources.AirplaneStartLogo;
            this.pictureBox1.Location = new System.Drawing.Point(186, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(373, 363);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // copyrightControlWide1
            // 
            this.copyrightControlWide1.BackColor = System.Drawing.Color.Transparent;
            this.copyrightControlWide1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.copyrightControlWide1.ForeColor = System.Drawing.Color.White;
            this.copyrightControlWide1.Location = new System.Drawing.Point(0, 637);
            this.copyrightControlWide1.Name = "copyrightControlWide1";
            this.copyrightControlWide1.Size = new System.Drawing.Size(1087, 40);
            this.copyrightControlWide1.TabIndex = 1;
            // 
            // Screen
            // 
            this.AcceptButton = this.linkLabelContinueLoading;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(139)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(1087, 677);
            this.Controls.Add(this.copyrightControlWide1);
            this.Controls.Add(this.panelImageContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Screen";
            this.ShowInTaskbar = false;
            this.Text = new GlobalTermsProvider()["SystemName"].ToString();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SizeChanged += new System.EventHandler(this.Screen_SizeChanged);
            this.panelImageContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelImageContainer;
        private System.Windows.Forms.Label labelWelcomeText;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabelContinueLoading;
        private CAS.UI.UIControls.Auxiliary.CopyrightControl copyrightControlWide1;


    }
}
namespace Viewer.View.MDI
{
    partial class GWControl
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
            this.trackBarWidth = new System.Windows.Forms.TrackBar();
            this.groupBoxWidth = new System.Windows.Forms.GroupBox();
            this.trackBarCenter = new System.Windows.Forms.TrackBar();
            this.groupBoxCenter = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).BeginInit();
            this.groupBoxWidth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCenter)).BeginInit();
            this.groupBoxCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBarWidth
            // 
            this.trackBarWidth.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackBarWidth.Location = new System.Drawing.Point(3, 52);
            this.trackBarWidth.Name = "trackBarWidth";
            this.trackBarWidth.Size = new System.Drawing.Size(244, 45);
            this.trackBarWidth.TabIndex = 0;
            this.trackBarWidth.Scroll += new System.EventHandler(this.trackBarWidth_Scroll);
            // 
            // groupBoxWidth
            // 
            this.groupBoxWidth.Controls.Add(this.trackBarWidth);
            this.groupBoxWidth.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxWidth.Location = new System.Drawing.Point(0, 0);
            this.groupBoxWidth.Name = "groupBoxWidth";
            this.groupBoxWidth.Size = new System.Drawing.Size(250, 100);
            this.groupBoxWidth.TabIndex = 0;
            this.groupBoxWidth.TabStop = false;
            this.groupBoxWidth.Text = "Width (Contrast) : 40";
            // 
            // trackBarCenter
            // 
            this.trackBarCenter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackBarCenter.Location = new System.Drawing.Point(3, 52);
            this.trackBarCenter.Name = "trackBarCenter";
            this.trackBarCenter.Size = new System.Drawing.Size(244, 45);
            this.trackBarCenter.TabIndex = 0;
            this.trackBarCenter.Scroll += new System.EventHandler(this.trackBarCenter_Scroll);
            // 
            // groupBoxCenter
            // 
            this.groupBoxCenter.Controls.Add(this.trackBarCenter);
            this.groupBoxCenter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxCenter.Location = new System.Drawing.Point(0, 150);
            this.groupBoxCenter.Name = "groupBoxCenter";
            this.groupBoxCenter.Size = new System.Drawing.Size(250, 100);
            this.groupBoxCenter.TabIndex = 1;
            this.groupBoxCenter.TabStop = false;
            this.groupBoxCenter.Text = "Center (Brightness) : 100";
            // 
            // GWControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxCenter);
            this.Controls.Add(this.groupBoxWidth);
            this.Name = "GWControl";
            this.Size = new System.Drawing.Size(250, 250);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).EndInit();
            this.groupBoxWidth.ResumeLayout(false);
            this.groupBoxWidth.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCenter)).EndInit();
            this.groupBoxCenter.ResumeLayout(false);
            this.groupBoxCenter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarWidth;
        private System.Windows.Forms.GroupBox groupBoxWidth;
        private System.Windows.Forms.TrackBar trackBarCenter;
        private System.Windows.Forms.GroupBox groupBoxCenter;



    }
}

namespace basicDef_Block
{
    partial class App_Flow
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
            this.Applications_Flow = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // Applications_Flow
            // 
            this.Applications_Flow.AutoScroll = true;
            this.Applications_Flow.BackColor = System.Drawing.Color.Transparent;
            this.Applications_Flow.Location = new System.Drawing.Point(0, 0);
            this.Applications_Flow.Margin = new System.Windows.Forms.Padding(0);
            this.Applications_Flow.Name = "Applications_Flow";
            this.Applications_Flow.Size = new System.Drawing.Size(790, 380);
            this.Applications_Flow.TabIndex = 0;
            // 
            // App_Flow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Applications_Flow);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "App_Flow";
            this.Size = new System.Drawing.Size(790, 380);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.FlowLayoutPanel Applications_Flow;
    }
}

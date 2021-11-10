namespace basicDef_Block
{
    partial class Main_Application
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Application));
            this.Close_Label = new System.Windows.Forms.Label();
            this.Flow_Panel_Holder = new System.Windows.Forms.Panel();
            this.Logo_Picture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Logo_Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // Close_Label
            // 
            this.Close_Label.AutoSize = true;
            this.Close_Label.BackColor = System.Drawing.Color.Transparent;
            this.Close_Label.Font = new System.Drawing.Font("Maassslicer", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Close_Label.ForeColor = System.Drawing.Color.Lime;
            this.Close_Label.Location = new System.Drawing.Point(768, 13);
            this.Close_Label.Name = "Close_Label";
            this.Close_Label.Size = new System.Drawing.Size(20, 20);
            this.Close_Label.TabIndex = 1;
            this.Close_Label.Text = "X";
            this.Close_Label.Click += new System.EventHandler(this.Close_Label_Click);
            // 
            // Flow_Panel_Holder
            // 
            this.Flow_Panel_Holder.Location = new System.Drawing.Point(15, 50);
            this.Flow_Panel_Holder.Margin = new System.Windows.Forms.Padding(0);
            this.Flow_Panel_Holder.Name = "Flow_Panel_Holder";
            this.Flow_Panel_Holder.Size = new System.Drawing.Size(770, 380);
            this.Flow_Panel_Holder.TabIndex = 2;
            // 
            // Logo_Picture
            // 
            this.Logo_Picture.Image = global::basicDef_Block.Properties.Resources.basicDef;
            this.Logo_Picture.Location = new System.Drawing.Point(12, 3);
            this.Logo_Picture.Name = "Logo_Picture";
            this.Logo_Picture.Size = new System.Drawing.Size(122, 44);
            this.Logo_Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Logo_Picture.TabIndex = 0;
            this.Logo_Picture.TabStop = false;
            // 
            // Main_Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowText;
            this.BackgroundImage = global::basicDef_Block.Properties.Resources.hatter;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Flow_Panel_Holder);
            this.Controls.Add(this.Close_Label);
            this.Controls.Add(this.Logo_Picture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main_Application";
            this.Text = "basicDef block";
            this.Shown += new System.EventHandler(this.Main_Application_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_Application_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_Application_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.Logo_Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Logo_Picture;
        private System.Windows.Forms.Label Close_Label;
        private System.Windows.Forms.Panel Flow_Panel_Holder;
    }
}


namespace basicDef_Block
{
    partial class Apps
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
            this.components = new System.ComponentModel.Container();
            this.Display_name = new System.Windows.Forms.Label();
            this.Icon_List = new System.Windows.Forms.ImageList(this.components);
            this.App_displayicon = new System.Windows.Forms.PictureBox();
            this.app_button_picture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.App_displayicon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.app_button_picture)).BeginInit();
            this.SuspendLayout();
            // 
            // Display_name
            // 
            this.Display_name.AutoSize = true;
            this.Display_name.Font = new System.Drawing.Font("Maassslicer", 8F);
            this.Display_name.ForeColor = System.Drawing.Color.Lime;
            this.Display_name.Location = new System.Drawing.Point(50, 18);
            this.Display_name.Name = "Display_name";
            this.Display_name.Size = new System.Drawing.Size(86, 14);
            this.Display_name.TabIndex = 2;
            this.Display_name.Text = "Display_name";
            // 
            // Icon_List
            // 
            this.Icon_List.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.Icon_List.ImageSize = new System.Drawing.Size(30, 30);
            this.Icon_List.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // App_displayicon
            // 
            this.App_displayicon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.App_displayicon.Location = new System.Drawing.Point(10, 10);
            this.App_displayicon.Name = "App_displayicon";
            this.App_displayicon.Size = new System.Drawing.Size(30, 30);
            this.App_displayicon.TabIndex = 1;
            this.App_displayicon.TabStop = false;
            // 
            // app_button_picture
            // 
            this.app_button_picture.BackgroundImage = global::basicDef_Block.Properties.Resources.Button_active;
            this.app_button_picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.app_button_picture.Location = new System.Drawing.Point(280, 15);
            this.app_button_picture.Name = "app_button_picture";
            this.app_button_picture.Size = new System.Drawing.Size(75, 20);
            this.app_button_picture.TabIndex = 3;
            this.app_button_picture.TabStop = false;
            this.app_button_picture.Click += new System.EventHandler(this.app_button_picture_Click);
            // 
            // Apps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::basicDef_Block.Properties.Resources.app_border;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.app_button_picture);
            this.Controls.Add(this.Display_name);
            this.Controls.Add(this.App_displayicon);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.Name = "Apps";
            this.Size = new System.Drawing.Size(380, 50);
            ((System.ComponentModel.ISupportInitialize)(this.App_displayicon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.app_button_picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox App_displayicon;
        private System.Windows.Forms.Label Display_name;
        private System.Windows.Forms.ImageList Icon_List;
        private System.Windows.Forms.PictureBox app_button_picture;
    }
}

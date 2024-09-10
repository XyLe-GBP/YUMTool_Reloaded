
namespace YUMTool_R
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            button1 = new System.Windows.Forms.Button();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            linkLabel2 = new System.Windows.Forms.LinkLabel();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // linkLabel1
            // 
            resources.ApplyResources(linkLabel1, "linkLabel1");
            linkLabel1.Name = "linkLabel1";
            linkLabel1.TabStop = true;
            linkLabel1.LinkClicked += LinkLabel1_LinkClicked;
            // 
            // linkLabel2
            // 
            resources.ApplyResources(linkLabel2, "linkLabel2");
            linkLabel2.Name = "linkLabel2";
            linkLabel2.TabStop = true;
            linkLabel2.LinkClicked += LinkLabel2_LinkClicked;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.imas_basic_logo;
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.pg_hrk;
            resources.ApplyResources(pictureBox2, "pictureBox2");
            pictureBox2.Name = "pictureBox2";
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // pictureBox3
            // 
            resources.ApplyResources(pictureBox3, "pictureBox3");
            pictureBox3.Name = "pictureBox3";
            pictureBox3.TabStop = false;
            pictureBox3.Paint += pictureBox3_Paint;
            // 
            // FormAbout
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ControlBox = false;
            Controls.Add(pictureBox3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(linkLabel2);
            Controls.Add(linkLabel1);
            Controls.Add(button1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "FormAbout";
            Load += FormAbout_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}
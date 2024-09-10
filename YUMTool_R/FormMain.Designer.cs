
namespace YUMTool_R
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            listView1 = new System.Windows.Forms.ListView();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            backgroundWorker_Decompress = new System.ComponentModel.BackgroundWorker();
            backgroundWorker_Recompress = new System.ComponentModel.BackgroundWorker();
            groupBox1 = new System.Windows.Forms.GroupBox();
            textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(listView1, "listView1");
            listView1.Name = "listView1";
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = System.Windows.Forms.View.Details;
            listView1.ColumnWidthChanging += ListView1_ColumnWidthChanging;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.logo_sp;
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2, toolStripMenuItem3 });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem4, toolStripMenuItem6, toolStripSeparator1, toolStripMenuItem8 });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem5, toolStripMenuItem7 });
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(toolStripMenuItem5, "toolStripMenuItem5");
            toolStripMenuItem5.Click += ToolStripMenuItem5_Click;
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            resources.ApplyResources(toolStripMenuItem7, "toolStripMenuItem7");
            toolStripMenuItem7.Click += ToolStripMenuItem7_Click;
            // 
            // toolStripMenuItem6
            // 
            resources.ApplyResources(toolStripMenuItem6, "toolStripMenuItem6");
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Click += ToolStripMenuItem6_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.ForeColor = System.Drawing.Color.Red;
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            resources.ApplyResources(toolStripMenuItem8, "toolStripMenuItem8");
            toolStripMenuItem8.Click += ToolStripMenuItem8_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem9 });
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // toolStripMenuItem9
            // 
            toolStripMenuItem9.Name = "toolStripMenuItem9";
            resources.ApplyResources(toolStripMenuItem9, "toolStripMenuItem9");
            toolStripMenuItem9.Click += ToolStripMenuItem9_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem10, toolStripSeparator2, toolStripMenuItem12 });
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // toolStripMenuItem10
            // 
            toolStripMenuItem10.Name = "toolStripMenuItem10";
            resources.ApplyResources(toolStripMenuItem10, "toolStripMenuItem10");
            toolStripMenuItem10.Click += ToolStripMenuItem10_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripMenuItem12
            // 
            toolStripMenuItem12.Name = "toolStripMenuItem12";
            resources.ApplyResources(toolStripMenuItem12, "toolStripMenuItem12");
            toolStripMenuItem12.Click += ToolStripMenuItem12_Click;
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
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.logo_not;
            resources.ApplyResources(pictureBox2, "pictureBox2");
            pictureBox2.Name = "pictureBox2";
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // button2
            // 
            resources.ApplyResources(button2, "button2");
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            // 
            // backgroundWorker2
            // 
            backgroundWorker2.WorkerReportsProgress = true;
            backgroundWorker2.DoWork += BackgroundWorker2_DoWork;
            backgroundWorker2.ProgressChanged += BackgroundWorker2_ProgressChanged;
            // 
            // backgroundWorker_Decompress
            // 
            backgroundWorker_Decompress.WorkerReportsProgress = true;
            backgroundWorker_Decompress.DoWork += BackgroundWorker_Decompress_DoWork;
            // 
            // backgroundWorker_Recompress
            // 
            backgroundWorker_Recompress.WorkerReportsProgress = true;
            backgroundWorker_Recompress.DoWork += BackgroundWorker_Recompress_DoWork;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox1);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // textBox1
            // 
            resources.ApplyResources(textBox1, "textBox1");
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pictureBox2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(listView1);
            Controls.Add(menuStrip1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "FormMain";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Decompress;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Recompress;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox textBox1;
    }
}


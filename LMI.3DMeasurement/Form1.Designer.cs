namespace LMI._3DMeasurement
{
    partial class FormTest
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonGrab = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.checkBox3D = new System.Windows.Forms.CheckBox();
            this.checkBox2D = new System.Windows.Forms.CheckBox();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.buttonDraw = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGrab
            // 
            this.buttonGrab.Location = new System.Drawing.Point(46, 58);
            this.buttonGrab.Name = "buttonGrab";
            this.buttonGrab.Size = new System.Drawing.Size(75, 23);
            this.buttonGrab.TabIndex = 1;
            this.buttonGrab.Text = "Grab";
            this.buttonGrab.UseVisualStyleBackColor = true;
            this.buttonGrab.Click += new System.EventHandler(this.buttonGrab_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonClear);
            this.splitContainer1.Panel2.Controls.Add(this.buttonGrab);
            this.splitContainer1.Size = new System.Drawing.Size(983, 735);
            this.splitContainer1.SplitterDistance = 806;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.buttonDraw);
            this.splitContainer2.Panel1.Controls.Add(this.checkBox3D);
            this.splitContainer2.Panel1.Controls.Add(this.checkBox2D);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.hWindowControl1);
            this.splitContainer2.Size = new System.Drawing.Size(806, 735);
            this.splitContainer2.SplitterDistance = 68;
            this.splitContainer2.TabIndex = 0;
            // 
            // checkBox3D
            // 
            this.checkBox3D.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox3D.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.checkBox3D.Location = new System.Drawing.Point(134, 3);
            this.checkBox3D.Name = "checkBox3D";
            this.checkBox3D.Size = new System.Drawing.Size(83, 62);
            this.checkBox3D.TabIndex = 1;
            this.checkBox3D.Text = "3D";
            this.checkBox3D.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox3D.UseVisualStyleBackColor = false;
            // 
            // checkBox2D
            // 
            this.checkBox2D.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox2D.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.checkBox2D.Location = new System.Drawing.Point(45, 3);
            this.checkBox2D.Name = "checkBox2D";
            this.checkBox2D.Size = new System.Drawing.Size(83, 62);
            this.checkBox2D.TabIndex = 0;
            this.checkBox2D.Text = "2D";
            this.checkBox2D.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox2D.UseVisualStyleBackColor = false;
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(0, 0);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(806, 663);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(806, 663);
            // 
            // buttonDraw
            // 
            this.buttonDraw.Location = new System.Drawing.Point(262, 8);
            this.buttonDraw.Name = "buttonDraw";
            this.buttonDraw.Size = new System.Drawing.Size(86, 53);
            this.buttonDraw.TabIndex = 2;
            this.buttonDraw.Text = "DrawROI";
            this.buttonDraw.UseVisualStyleBackColor = true;
            this.buttonDraw.Click += new System.EventHandler(this.buttonDraw_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(46, 417);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "Clear Display";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 735);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormTest";
            this.Text = "Test";
            this.Load += new System.EventHandler(this.FormTest_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonGrab;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox checkBox3D;
        private System.Windows.Forms.CheckBox checkBox2D;
        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.Button buttonDraw;
        private System.Windows.Forms.Button buttonClear;
    }
}


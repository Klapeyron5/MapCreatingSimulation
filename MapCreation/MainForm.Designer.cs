using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MapCreation
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.numericUpDown4sgm_psi = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown5scan_noise = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown6n_phi = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3sgm_lmax = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2r_robot = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1r_scan = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4sgm_psi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5scan_noise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6n_phi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3sgm_lmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2r_robot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1r_scan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.numericUpDown4sgm_psi);
            this.panel3.Controls.Add(this.numericUpDown5scan_noise);
            this.panel3.Controls.Add(this.numericUpDown6n_phi);
            this.panel3.Controls.Add(this.numericUpDown3sgm_lmax);
            this.panel3.Controls.Add(this.numericUpDown2r_robot);
            this.panel3.Controls.Add(this.numericUpDown1r_scan);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.button2);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // numericUpDown6n_phi
            // 
            resources.ApplyResources(this.numericUpDown6n_phi, "numericUpDown6n_phi");
            this.numericUpDown6n_phi.Name = "numericUpDown6n_phi";
            numericUpDown6n_phi.Minimum = 10;
            numericUpDown6n_phi.Maximum = 500;
            numericUpDown6n_phi.Value = Parameters.getN_phi();
            // 
            // numericUpDown2r_robot
            // 
            resources.ApplyResources(this.numericUpDown2r_robot, "numericUpDown2r_robot");
            this.numericUpDown2r_robot.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown2r_robot.Name = "numericUpDown2r_robot";
            this.numericUpDown2r_robot.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // numericUpDown1r_scan
            // 
            resources.ApplyResources(this.numericUpDown1r_scan, "numericUpDown1r_scan");
            this.numericUpDown1r_scan.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown1r_scan.Name = "numericUpDown1r_scan";
            this.numericUpDown1r_scan.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // numericUpDown3sgm_lmax
            // 
            resources.ApplyResources(this.numericUpDown3sgm_lmax, "numericUpDown3sgm_lmax");
            this.numericUpDown3sgm_lmax.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown3sgm_lmax.Name = "numericUpDown3sgm_lmax";
            this.numericUpDown3sgm_lmax.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numericUpDown4sgm_psi
            // 
            resources.ApplyResources(this.numericUpDown4sgm_psi, "numericUpDown4sgm_psi");
            this.numericUpDown4sgm_psi.Name = "numericUpDown4sgm_psi";
            // 
            // numericUpDown5scan_noise
            // 
            resources.ApplyResources(this.numericUpDown5scan_noise, "numericUpDown5scan_noise");
            this.numericUpDown5scan_noise.Name = "numericUpDown5scan_noise";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // radioButton3
            // 
            resources.ApplyResources(this.radioButton3, "radioButton3");
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.TabStop = true;
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.TabStop = true;
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.label8.Name = "label8";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4sgm_psi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5scan_noise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6n_phi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3sgm_lmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2r_robot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1r_scan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;

        /// <summary>
        /// Вызвать до начала создания компонентов на panel1
        /// </summary>
        public void panel1SuspendLayout()
        {
            panel2.SuspendLayout();
        }

        /// <summary>
        /// Вызвать после создания компонентов на panel1
        /// </summary>
        public void panel1ReleaseLayout()
        {
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
        }

        /// <summary>
        /// Добавляет компонент на panel1
        /// </summary>
        /// <param name="control"></param>
        public void addComponentToPanel1(System.Windows.Forms.Control control)
        {
            panel2.Controls.Add(control);
        }

        /// <summary>
        /// Освобождает panel1
        /// </summary>
        public void clearPanel1()
        {
            panel1SuspendLayout();
            for (int i = 0; i < panel2.Controls.Count; i++)
                panel2.Controls[i].Dispose();
            panel2.Controls.Clear();
            panel1ReleaseLayout();
        }

        public Size getPictureBox1Size()
        {
            return pictureBox1.Size;
        }
        
        public void setPictureBox1MouseDownHandler(MouseEventHandler handler)
        {
            pictureBox1.MouseDown += handler;
        }
        
        public void setPictureBox1MouseMoveHandler(MouseEventHandler handler)
        {
            pictureBox1.MouseMove += handler;
        }

        public void removePictureBox1MouseDownHandler(MouseEventHandler handler)
        {
            pictureBox1.MouseDown -= handler;
        }

        public void removePictureBox1MouseMoveHandler(MouseEventHandler handler)
        {
            pictureBox1.MouseMove -= handler;
        }

        /// <summary>
        /// Обновляет значение лога label1 и изображение точной карты
        /// </summary>
        public void updateEnvironmentProjection()
        {
            switch(environment.isMapLoaded())
            {
                case 0:
                    label1.Text = "map has not loaded";
                    if (mode1ManualCrosslinking != null)
                    {
                        mode1ManualCrosslinking.destroy();
                        mode1ManualCrosslinking = null;
                    }
                    break;
                case 1:
                    label1.Text = "map has loaded";
                    break;
                case 2:
                    label1.Text = "map has not loaded, format exception";
                    break;
            }
            drawBitmapOnPictureBox1(environment.getPreciseBmp());
        }

        /// <summary>
        /// Рисует битман на pictureBox1 (который левые на форме, где precise map), при этом не размывает, если битмап меньше реальных размеров pictureBox'а
        /// </summary>
        /// <param name="bmp">битмап, который надо отрисовать</param>
        public void drawBitmapOnPictureBox1(Bitmap bmp)
        {
            drawBitmapOnPictureBox(pictureBox1,bmp);
        }

        /// <summary>
        /// Рисует битман на данном pictureBox, при этом не размывает, если битмап меньше реальных размеров pictureBox'а
        /// </summary>
        /// <param name="pictureBox">на нем будет нарисован битмап</param>
        /// <param name="bmp">битмап, который надо отрисовать</param>
        public static void drawBitmapOnPictureBox(PictureBox pictureBox, Bitmap bmp)
        {
            if (pictureBox.Size != bmp.Size)
            {
                float zoom = 60.0f;
                Bitmap zoomed = new Bitmap((int)(bmp.Width * zoom), (int)(bmp.Height * zoom));

                using (Graphics g = Graphics.FromImage(zoomed))
                {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.DrawImage(bmp, new Rectangle(Point.Empty, zoomed.Size));
                }
                pictureBox.Image = zoomed;
            }
            else
                pictureBox.Image = bmp;
        }

        private Button button2;
        private Panel panel3;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private NumericUpDown numericUpDown2r_robot;
        private NumericUpDown numericUpDown1r_scan;
        private NumericUpDown numericUpDown3sgm_lmax;
        private NumericUpDown numericUpDown4sgm_psi;
        private NumericUpDown numericUpDown5scan_noise;
        private NumericUpDown numericUpDown6n_phi;
        private Label label8;
    }
}


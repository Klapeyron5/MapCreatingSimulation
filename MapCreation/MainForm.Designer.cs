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
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
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
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
    }
}


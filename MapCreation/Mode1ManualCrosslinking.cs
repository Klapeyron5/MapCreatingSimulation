using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MapCreation
{
    class Mode1ManualCrosslinking:Mode
    {
        /// <summary>
        /// Сразу загружает необходимый UI интерфейс для данного режима
        /// </summary>
        /// <param name="mainForm"></param>
        public Mode1ManualCrosslinking(MainForm mainForm):base(mainForm)
        {
            if (this.mainForm != null)
            {
                mouseMoveMap = new PixelMap(mainForm.environment.preciseMap);
                initialize();
            }
        }

        public void initialize()
        {
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.button2Scan0Center = new System.Windows.Forms.Button();
            this.button3Scan1Center = new System.Windows.Forms.Button();
            this.button4SupposedScan1Center = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.textBox1Scan0Center = new System.Windows.Forms.TextBox();
            this.textBox2Scan1Center = new System.Windows.Forms.TextBox();
            this.textBox3SupposedScan1Center = new System.Windows.Forms.TextBox();
            this.button1Crosslink = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            mainForm.panel1SuspendLayout();
            mainForm.setPictureBox1MouseDownHandler(new MouseEventHandler(pictureBox1_MouseDown));
            mainForm.setPictureBox1MouseMoveHandler(new MouseEventHandler(pictureBox1_MouseMove));
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox2.Location = new System.Drawing.Point(0, 32);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(Parameters.getD_scan()+1, Parameters.getD_scan() + 1);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox3.Location = new System.Drawing.Point(0, Parameters.getD_scan() + 52 +10);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(Parameters.getD_scan() + 1, Parameters.getD_scan() + 1);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 12;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox4.Location = new System.Drawing.Point(Parameters.getD_scan() + 1 + 5, 32);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(Parameters.getD_scan() + Parameters.getR_scan() + 1, Parameters.getD_scan() + Parameters.getR_scan() + 1);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 13;
            this.pictureBox4.TabStop = false;
            // 
            // button2Scan0Center
            // 
            this.button2Scan0Center.AutoSize = true;
            this.button2Scan0Center.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.button2Scan0Center.Location = new System.Drawing.Point(0, 6);
            this.button2Scan0Center.Name = "button2";
            this.button2Scan0Center.Size = new System.Drawing.Size(41, 13);
            this.button2Scan0Center.TabIndex = 15;
            this.button2Scan0Center.Text = "Scan 0";
            button2Scan0Center.MouseClick += button2Scan0Center_Click;
            // 
            // button3Scan1Center
            // 
            this.button3Scan1Center.AutoSize = true;
            this.button3Scan1Center.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.button3Scan1Center.Location = new System.Drawing.Point(0, Parameters.getD_scan() + 36);
            this.button3Scan1Center.Name = "button3";
            this.button3Scan1Center.Size = new System.Drawing.Size(41, 13);
            this.button3Scan1Center.TabIndex = 16;
            this.button3Scan1Center.Text = "Scan 1";
            button3Scan1Center.MouseClick += button3Scan1Center_Click;
            // 
            // button4SupposedScan1Center
            // 
            this.button4SupposedScan1Center.AutoSize = true;
            this.button4SupposedScan1Center.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.button4SupposedScan1Center.Location = new System.Drawing.Point(0, pictureBox3.Location.Y + pictureBox3.Height + 2);
            this.button4SupposedScan1Center.Name = "button4";
            this.button4SupposedScan1Center.Size = new System.Drawing.Size(41, 13);
            this.button4SupposedScan1Center.TabIndex = 16;
            this.button4SupposedScan1Center.Text = "Supposed";
            button4SupposedScan1Center.MouseClick += button4SupposedScan1Center_Click;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.label4.Location = new System.Drawing.Point(pictureBox4.Location.X, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Crosslinked";
            // 
            // label5
            // 
            this.labelError.AutoSize = true;
            this.labelError.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.labelError.Location = new System.Drawing.Point(pictureBox4.Location.X, pictureBox4.Location.Y + pictureBox4.Height + 4);
            this.labelError.Name = "label5";
            this.labelError.Size = new System.Drawing.Size(61, 13);
            this.labelError.TabIndex = 17;
            this.labelError.Text = "error: ";
            // 
            // textBox1
            // 
            this.textBox1Scan0Center.Size = new System.Drawing.Size(50, 13);
            this.textBox1Scan0Center.Location = new System.Drawing.Point(pictureBox2.Location.X+pictureBox2.Width-textBox1Scan0Center.Width, button2Scan0Center.Location.Y+1);
            this.textBox1Scan0Center.Name = "textBox1";
            this.textBox1Scan0Center.TabIndex = 17;
            this.textBox1Scan0Center.Text = "x,y";
            // 
            // textBox2
            // 
            this.textBox2Scan1Center.Size = new System.Drawing.Size(50, 13);
            this.textBox2Scan1Center.Location = new System.Drawing.Point(pictureBox2.Location.X + pictureBox2.Width - textBox2Scan1Center.Width, button3Scan1Center.Location.Y + 1);
            this.textBox2Scan1Center.Name = "textBox2";
            this.textBox2Scan1Center.TabIndex = 17;
            this.textBox2Scan1Center.Text = "x,y";
            // 
            // textBox3
            // 
            this.textBox3SupposedScan1Center.Size = new System.Drawing.Size(50, 13);
            this.textBox3SupposedScan1Center.Location = new System.Drawing.Point(pictureBox2.Location.X + pictureBox2.Width - textBox3SupposedScan1Center.Width, pictureBox3.Location.Y + pictureBox3.Height + 3);
            this.textBox3SupposedScan1Center.Name = "textBox2";
            this.textBox3SupposedScan1Center.TabIndex = 17;
            this.textBox3SupposedScan1Center.Text = "x,y";
            // 
            // button1
            // 
            this.button1Crosslink.Location = new System.Drawing.Point(pictureBox4.Location.X + pictureBox4.Width - button1Crosslink.Size.Width, pictureBox4.Location.Y + pictureBox4.Height);
            this.button1Crosslink.Name = "button1";
            this.button1Crosslink.Size = new System.Drawing.Size(75, 23);
            this.button1Crosslink.TabIndex = 18;
            this.button1Crosslink.Text = "Closslink";
            this.button1Crosslink.UseVisualStyleBackColor = true;
            this.button1Crosslink.Click += new System.EventHandler(this.button1Crosslink_Click);
            // 
            // Form1
            // 
            mainForm.BackColor = System.Drawing.SystemColors.ActiveCaption;
            mainForm.ClientSize = new System.Drawing.Size(1454, 873);
            mainForm.addComponentToPanel1(button1Crosslink);
            mainForm.addComponentToPanel1(this.labelError);
            mainForm.addComponentToPanel1(this.label4);
            mainForm.addComponentToPanel1(this.button4SupposedScan1Center);
            mainForm.addComponentToPanel1(this.button3Scan1Center);
            mainForm.addComponentToPanel1(this.button2Scan0Center);
            mainForm.addComponentToPanel1(this.textBox3SupposedScan1Center);
            mainForm.addComponentToPanel1(this.textBox2Scan1Center);
            mainForm.addComponentToPanel1(this.textBox1Scan0Center);
            mainForm.addComponentToPanel1(this.pictureBox4);
            mainForm.addComponentToPanel1(this.pictureBox3);
            mainForm.addComponentToPanel1(this.pictureBox2);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            mainForm.panel1ReleaseLayout();
        }

        public void destroy()
        {
            mainForm.removePictureBox1MouseDownHandler(pictureBox1_MouseDown);
            mainForm.removePictureBox1MouseMoveHandler(pictureBox1_MouseMove);
            mainForm.clearPanel2();
        }

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button button2Scan0Center;
        private System.Windows.Forms.Button button3Scan1Center;
        private System.Windows.Forms.Button button4SupposedScan1Center;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.TextBox textBox1Scan0Center;
        private System.Windows.Forms.TextBox textBox2Scan1Center;
        private System.Windows.Forms.TextBox textBox3SupposedScan1Center;
        private System.Windows.Forms.Button button1Crosslink;
        
        /// <summary>
        /// Обработчик кликов на pictureBox1 (где отрисована preciseMap).
        /// Клик обрабатывается только в случае попадания его в indoor-среду.
        /// Первый клик устанавливает центр нулевого скана
        /// Второй клик устанавливает центр первого скана: расстояние от центра scan1 до центра scan0 должно быть меньше l_max
        /// Третий клик устанавливает supposed центр скана 1: может быть установлен только в зоне погрешности скана 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int X = e.X * mainForm.environment.preciseMap.Width / mainForm.getPictureBox1Size().Width;
            int Y = e.Y * mainForm.environment.preciseMap.Height / mainForm.getPictureBox1Size().Height;
            switch (positionCounter)
            {
                case 0:
                    setScan0(X, Y);
                    break;
                case 1:
                    setScan1(X, Y);
                    break;
                case 2:
                    setSupposedScan1(X, Y);
                    break;
            }
        }

        /// <summary>
        /// отрисовать все три центра
        /// отрисовать радиусы для scan0 и scan1
        /// </summary>
        /// <param name="preciseMapBmp"></param>
        /// <param name="graphics"></param>
        private void interfaceDrawing(Bitmap preciseMapBmp, Graphics graphics)
        {
            try
            {
                Pen pen;
                int X0 = crosslinker.getXY0()[0];
                int Y0 = crosslinker.getXY0()[1];
                int X1 = crosslinker.getXY1()[0];
                int Y1 = crosslinker.getXY1()[1];
                int X2 = crosslinker.getXY2()[0];
                int Y2 = crosslinker.getXY2()[1];
                if ((X0 >= 0) && (Y0 >= 0))
                {
                    preciseMapBmp.SetPixel(X0, Y0, Parameters.startColor);
                    pen = new Pen(Parameters.startColor);
                    graphics.DrawEllipse(pen, X0 - Parameters.getR_scan(), Y0 - Parameters.getR_scan(), Parameters.getD_scan(), Parameters.getD_scan());
                    graphics.DrawEllipse(pen, X0 - Parameters.getR_robot(), Y0 - Parameters.getR_robot(), Parameters.getD_robot(), Parameters.getD_robot());
                }
                if ((X1 >= 0) && (Y1 >= 0))
                {
                    preciseMapBmp.SetPixel(X1, Y1, Parameters.finishColor);
                    pen = new Pen(Parameters.finishColor);
                    graphics.DrawEllipse(pen, X1 - Parameters.getR_scan(), Y1 - Parameters.getR_scan(), Parameters.getD_scan(), Parameters.getD_scan());
                    graphics.DrawEllipse(pen, X1 - Parameters.getR_robot(), Y1 - Parameters.getR_robot(), Parameters.getD_robot(), Parameters.getD_robot());
                }
                if ((X2 >= 0) && (Y2 >= 0))
                {
                    preciseMapBmp.SetPixel(X2, Y2, Parameters.routeColor);
                }
            }
            catch (Exception ex) { }
            mouseMoveMap = new PixelMap(preciseMapBmp);
            mainForm.drawBitmapOnPictureBox1(preciseMapBmp);
        }

        /// <summary>
        /// Подсветка пикселя, на который наведена мышка (для дебага на мелких картах)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int X = e.X * mainForm.environment.preciseMap.Width / mainForm.getPictureBox1Size().Width;
            int Y = e.Y * mainForm.environment.preciseMap.Height / mainForm.getPictureBox1Size().Height;
            Bitmap bmp = new Bitmap(mouseMoveMap.GetBitmap());
            bmp.SetPixel(X, Y, Parameters.mouseMoveColor);
            mainForm.drawBitmapOnPictureBox1(bmp);
        }

        /// <summary>
        /// Сшить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1Crosslink_Click(object sender, EventArgs e)
        {
            if ((positionCounter == 0) && (crosslinker.getXY2()[0] >= 0))
            {
                int[] real_coords = crosslinker.getRealCoords4();
                drawCrosslinkedScans(real_coords[0], real_coords[1]);
                labelError.Text = "error: "+Math.Pow(real_coords[2],0.5) + "\n"+"err cone points: "+real_coords[3]+"\n"+"Min sum points: "+real_coords[4];
            }
        }

        private void button2Scan0Center_Click(object sender, EventArgs e)
        {
            try
            {
                String[] s = textBox1Scan0Center.Text.Split(',');
                if (s.Length == 2)
                {
                    uint X = UInt32.Parse(s[0]);
                    uint Y = UInt32.Parse(s[1]);
                    setScan0((int)X, (int)Y);
                }
            }
            catch (Exception ex) {}
        }

        private void button3Scan1Center_Click(object sender, EventArgs e)
        {
            try
            {
                String[] s = textBox2Scan1Center.Text.Split(',');
                if (s.Length == 2)
                {
                    uint X = UInt32.Parse(s[0]);
                    uint Y = UInt32.Parse(s[1]);
                    setScan1((int)X,(int)Y);
                }
            }
            catch (Exception ex) { }
        }

        private void button4SupposedScan1Center_Click(object sender, EventArgs e)
        {
            String[] s = textBox3SupposedScan1Center.Text.Split(',');
            if (s.Length == 2)
            {
                uint X = UInt32.Parse(s[0]);
                uint Y = UInt32.Parse(s[1]);
                setSupposedScan1((int)X,(int)Y);
            }
        }

        private void setScan0(int X, int Y)
        {
            if (positionCounter == 0)
            {
                Bitmap preciseMapBmp = mainForm.environment.preciseMap.GetBitmap();
                Graphics graphics = Graphics.FromImage(preciseMapBmp);
                if (mainForm.environment.canRobotStayOnThisPoint(X, Y))
                {
                    crosslinker.setCenter0(X, Y);
                    crosslinker.scan0 = mainForm.environment.getScan(X, Y, Parameters.startColor);
                    MainForm.drawBitmapOnPictureBox(pictureBox2, crosslinker.scan0.getBitmap());
                    positionCounter++;
                    textBox1Scan0Center.Text = X + "," + Y;
                    interfaceDrawing(preciseMapBmp, graphics);
                }
            }
        }

        private void setScan1(int X, int Y)
        {
            if (positionCounter == 1)
            {
                Bitmap preciseMapBmp = mainForm.environment.preciseMap.GetBitmap();
                Graphics graphics = Graphics.FromImage(preciseMapBmp);
                if (mainForm.environment.canRobotStayOnThisPoint(X,Y))
                {
                    double l_rl2 = Parameters.getSquaredDistance(crosslinker.getXY0(),X,Y);
                    if (l_rl2 <= Parameters.getL_max2())
                    {
                        crosslinker.setCenter1(X,Y);
                        crosslinker.scan1 = mainForm.environment.getScan(X,Y, Parameters.finishColor);
                        MainForm.drawBitmapOnPictureBox(pictureBox3, crosslinker.scan1.getBitmap());
                        positionCounter++;
                        drawPieSupposedZone(preciseMapBmp);
                        textBox2Scan1Center.Text = X + "," + Y;
                        interfaceDrawing(preciseMapBmp, graphics);
                    }
                }
            }
        }

        private void setSupposedScan1(int X, int Y)
        {
            if (positionCounter == 2)
            {
                Bitmap preciseMapBmp = mainForm.environment.preciseMap.GetBitmap();
                Graphics graphics = Graphics.FromImage(preciseMapBmp);
                if (crosslinker.isPointInSupposedZone(X,Y))
                {
                    if (mainForm.environment.canRobotStayOnThisPoint(X,Y))
                    {
                        crosslinker.setCenter2(X,Y);
                        positionCounter = 0;
                        textBox3SupposedScan1Center.Text = X + "," + Y;
                        interfaceDrawing(preciseMapBmp, graphics);
                    }
                }
            }
        }

        /// <summary>
        /// DEBUG для отображения пикселя мышки
        /// </summary>
        private PixelMap mouseMoveMap;
        
        /// <summary>
        /// Счетчик номера клика (в цикле из трех)
        /// </summary>
        private byte positionCounter = 0;

        private Crosslinker crosslinker = new Crosslinker();

        /// <summary>
        /// Рисует зону на данном битмапе в абсолютных координатах, в которой может быть
        /// supposed положение робота относительно real положения в центре scan1
        /// </summary>
        /// <param name="bmp"></param>
        private void drawPieSupposedZone(Bitmap bmp)
        {
            List<int[]> pieZone = crosslinker.getPieSupposedZone();
            for (int i = 0; i < pieZone.Count; i++)
            {
                bmp.SetPixel(pieZone[i][0], pieZone[i][1], Parameters.routeColor);
            }
        }
        
        /// <summary>
        /// Рисует сшитые сканы на одном холсте.
        /// </summary>
        /// <param name="X1_rl"></param>
        /// <param name="Y1_rl"></param>
        private void drawCrosslinkedScans(int X1_rl, int Y1_rl)
        {
            int X0 = crosslinker.getXY0()[0];
            int Y0 = crosslinker.getXY0()[1];
            //
        //    int X2 = crosslinker.getXY2()[0];
        //    int Y2 = crosslinker.getXY2()[1];
            //
            PixelMap scan01 = new PixelMap(Parameters.getD_scan1() + Parameters.getR_scan(), Parameters.getD_scan1() + Parameters.getR_scan(), 0, 0, 0);
            int C = (Parameters.getD_scan() + Parameters.getR_scan()) / 2;
            for (int i = 0; i < crosslinker.scan0.xyScan.Count; i++)
                scan01[crosslinker.scan0.xyScan[i][0] + C, crosslinker.scan0.xyScan[i][1] + C] = new Pixel(Parameters.startColor);
            for (int i = 0; i < crosslinker.scan1.xyScan.Count; i++)
                scan01[crosslinker.scan1.xyScan[i][0] + X1_rl - X0 + C, crosslinker.scan1.xyScan[i][1] + Y1_rl - Y0 + C] = new Pixel(Parameters.finishColor);
            MainForm.drawBitmapOnPictureBox(pictureBox4, scan01.GetBitmap());
        }
    }
}

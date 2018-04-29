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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
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
            this.pictureBox2.Size = new System.Drawing.Size(141, 141);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox3.Location = new System.Drawing.Point(0, 192);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(141, 141);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 12;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox4.Location = new System.Drawing.Point(141+5, 32);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(211, 211);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 13;
            this.pictureBox4.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.label2.Location = new System.Drawing.Point(0, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Scan 0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.label3.Location = new System.Drawing.Point(0, 173+3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Scan 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.label4.Location = new System.Drawing.Point(141 + 5, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Crosslinked";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(141 + 5 - button1.Size.Width, 336);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Closslink";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            mainForm.BackColor = System.Drawing.SystemColors.ActiveCaption;
            mainForm.ClientSize = new System.Drawing.Size(1454, 873);
            mainForm.addComponentToPanel1(button1);
            mainForm.addComponentToPanel1(this.label4);
            mainForm.addComponentToPanel1(this.label3);
            mainForm.addComponentToPanel1(this.label2);
            mainForm.addComponentToPanel1(this.label1);
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
            mainForm.setPictureBox1MouseDownHandler(pictureBox1_MouseDown);
            mainForm.setPictureBox1MouseMoveHandler(pictureBox1_MouseMove);
            mainForm.clearPanel1();
        }

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        
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

            if (mainForm.environment.canRobotStayOnThisPoint(X,Y))
            {
                Bitmap preciseMapBmp = mainForm.environment.preciseMap.GetBitmap();
                Pen pen;
                SolidBrush brush;
                Graphics graphics = Graphics.FromImage(preciseMapBmp);
                switch (positionCounter)
                {
                    case 0:
                        {
                            crosslinker.setCenter0(X, Y);
                            crosslinker.scan0 = mainForm.environment.getScan(X, Y, Parameters.startColor);
                            MainForm.drawBitmapOnPictureBox(pictureBox2, crosslinker.scan0.getBitmap());
                            positionCounter++;
                        }
                        break;
                    case 1:
                        {
                            double l_rl2 = Parameters.getSquaredDistance(crosslinker.getXY0(), X, Y);
                            double l_rl = Math.Pow(l_rl2, 0.5);
                            int l_rl_rounded = (int)Math.Round(l_rl);
                            double psi_rl_rad = Parameters.getAngleRadian(crosslinker.getXY0(), X, Y);
                            //    psi_rl_deg = psi_rl_rad * 180 / Math.PI;
                            if (l_rl2 <= Parameters.l_max2)
                            {
                                crosslinker.setCenter1(X, Y);
                                crosslinker.scan1 = mainForm.environment.getScan(X, Y, Parameters.finishColor);
                                MainForm.drawBitmapOnPictureBox(pictureBox3, crosslinker.scan1.getBitmap());
                                positionCounter++;
                                drawPieZone(preciseMapBmp, crosslinker.getXY0(), crosslinker.getXY1());
                            }
                        }
                        break;
                    case 2:
                        {
                            //пока простая проверка: ровный разброс по углу и по длине
                            double l_sp2 = Parameters.getSquaredDistance(crosslinker.getXY0(), X, Y);
                            double l_sp = Math.Pow(l_sp2, 0.5);
                            double psi_rl_rad = Parameters.getAngleRadian(crosslinker.getXY0(), crosslinker.getXY1());
                            double psi_sp_rad = Parameters.getAngleRadian(crosslinker.getXY0(), X, Y);
                            bool angleFlag = false; //входит ли по угловой зоне

                            double l_rl2 = Parameters.getSquaredDistance(crosslinker.getXY0(), crosslinker.getXY1());
                            double l_rl = Math.Pow(l_rl2, 0.5);
                            double sgm_lrl = Parameters.getSgm_l(l_rl);
                            double l_rlPlus3sgm = l_rl + 3 * sgm_lrl;
                            double l_rlMinus3sgm = l_rl - 3 * sgm_lrl;
                            if ((psi_rl_rad > Math.PI / 2) && (psi_sp_rad < -Math.PI / 2))
                            {
                                if ((psi_sp_rad + 2 * Math.PI >= psi_rl_rad - 3 * Parameters.sgm_psi_rad) && (psi_sp_rad + 2 * Math.PI <= psi_rl_rad + 3 * Parameters.sgm_psi_rad)) angleFlag = true;
                                else angleFlag = false;
                            }
                            else
                            {
                                if ((psi_rl_rad < -Math.PI / 2) && (psi_sp_rad > Math.PI / 2))
                                {
                                    if ((psi_sp_rad - 2 * Math.PI >= psi_rl_rad - 3 * Parameters.sgm_psi_rad) && (psi_sp_rad - 2 * Math.PI <= psi_rl_rad + 3 * Parameters.sgm_psi_rad)) angleFlag = true;
                                    else angleFlag = false;
                                }
                                else
                                {
                                    if ((psi_sp_rad >= psi_rl_rad - 3 * Parameters.sgm_psi_rad) && (psi_sp_rad <= psi_rl_rad + 3 * Parameters.sgm_psi_rad)) angleFlag = true;
                                }
                            }
                            if (((l_sp >= l_rlMinus3sgm) && (l_sp <= l_rlPlus3sgm)) && angleFlag)
                            {
                                crosslinker.setCenter2(X, Y);
                                positionCounter = 0;
                            }
                            else
                                drawPieZone(preciseMapBmp, crosslinker.getXY0(), crosslinker.getXY1());
                        }
                        break;
                }
                //отрисовать все три центра
                //отрисовать радиусы для scan0 и scan1
                try
                {
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
                        graphics.DrawEllipse(pen, X0 - Parameters.r_scan, Y0 - Parameters.r_scan, Parameters.d_scan, Parameters.d_scan);
                        graphics.DrawEllipse(pen, X0 - Parameters.r_robot, Y0 - Parameters.r_robot, Parameters.d_robot, Parameters.d_robot);
                    }
                    if ((X1 >= 0) && (Y1 >= 0))
                    {
                        preciseMapBmp.SetPixel(X1, Y1, Parameters.finishColor);
                        pen = new Pen(Parameters.finishColor);
                        graphics.DrawEllipse(pen, X1 - Parameters.r_scan, Y1 - Parameters.r_scan, Parameters.d_scan, Parameters.d_scan);
                        graphics.DrawEllipse(pen, X1 - Parameters.r_robot, Y1 - Parameters.r_robot, Parameters.d_robot, Parameters.d_robot);
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
        private void button1_Click(object sender, EventArgs e)
        {

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
        /// Рисует зону, в которой может быть supposed положение робота относительно real положения в центре scan1.
        /// Зона рисуется примерная, к сожалению.
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="X1"></param>
        /// <param name="Y1"></param>
        /// <param name="X2"></param>
        /// <param name="Y2"></param>
        private void drawPieZone(Bitmap bmp, int X1, int Y1, int X2, int Y2)
        {
            List<int[]> pieZone = crosslinker.pieErrorZoneSearch(X1, Y1, X2, Y2);
            for (int i = 0; i < pieZone.Count; i++)
            {
                bmp.SetPixel(pieZone[i][0], pieZone[i][1], Parameters.routeColor);
            }
        }

        private void drawPieZone(Bitmap bmp, int[] xy1, int[] xy2)
        {
            List<int[]> pieZone = crosslinker.pieErrorZoneSearch(xy1[0], xy1[1], xy2[0], xy2[1]);
            for (int i = 0; i < pieZone.Count; i++)
            {
                bmp.SetPixel(pieZone[i][0], pieZone[i][1], Parameters.routeColor);
            }
        }
    }
}

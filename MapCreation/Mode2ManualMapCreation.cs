using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MapCreation
{
    class Mode2ManualMapCreation:Mode
    {
        public Mode2ManualMapCreation(MainForm mainForm) : base(mainForm)
        {
            if (this.mainForm != null)
            {
                initialize();
                //устанавливаем размеры предсказанной карты такие же, как и оригинальной (оригинальная сразу должна быть с запасом)
                predictedMap = new PixelMap(mainForm.environment.getPreciseBmp().Width, mainForm.environment.getPreciseBmp().Height,0,0,0);
                MainForm.drawBitmapOnPictureBox(pictureBox2,predictedMap.GetBitmap());
            }
        }

        public void initialize()
        {
            mainForm.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            // 
            // pictureBox1
            // 
            pictureBox2.BackColor = System.Drawing.SystemColors.ControlDark;
            pictureBox2.Location = new System.Drawing.Point(0, 32);
            pictureBox2.Name = "pictureBox1";
            pictureBox2.Size = mainForm.getMainPictureBoxSize();
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            mainForm.setPictureBox1MouseDownHandler(new MouseEventHandler(pictureBox1_MouseDown));

            mainForm.addComponentToPanel1(pictureBox2);
            ((System.ComponentModel.ISupportInitialize)(pictureBox2)).EndInit();
        }

        public void destroy()
        {
            mainForm.clearPanel2();
        }

        private System.Windows.Forms.PictureBox pictureBox2;

        private PixelMap predictedMap;

        private Crosslinker crosslinker = new Crosslinker();

        /// <summary>
        /// Текущая позиция робота на карте (позиция последнего скана)
        /// </summary>
        private int currentRobotPosX = 0;
        private int currentRobotPosY = 0;

        /// <summary>
        /// Счетчик номера клика (в цикле из трех)
        /// </summary>
        private int positionCounter = -1;

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine("ENTER");
            if (e.KeyData == Keys.Enter)
            {
                Console.WriteLine("ENTER");
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int X = e.X * mainForm.environment.preciseMap.Width / mainForm.getPictureBox1Size().Width;
            int Y = e.Y * mainForm.environment.preciseMap.Height / mainForm.getPictureBox1Size().Height;
            switch (positionCounter)
            {
                case -1:
                    setInitialScan(X,Y);
                    break;
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


        private void setInitialScan(int X, int Y)
        {
            positionCounter = 0;
            currentRobotPosX = X;
            currentRobotPosY = Y;
            setScan0(X, Y);

            //на предсказанную карту нанесем initial scan
            for(int i = 0; i < crosslinker.scan0.xyScan.Count; i++)
            {
                predictedMap[crosslinker.scan0.xyScan[i][0]+X, crosslinker.scan0.xyScan[i][1]+Y] = new Pixel(Parameters.wallColor);
            }
            MainForm.drawBitmapOnPictureBox(pictureBox2, predictedMap.GetBitmap());
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
                    positionCounter++;
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
                if (mainForm.environment.canRobotStayOnThisPoint(X, Y))
                {
                    double l_rl2 = Parameters.getSquaredDistance(crosslinker.getXY0(), X, Y);
                    if (l_rl2 <= Parameters.getL_max2())
                    {
                        crosslinker.setCenter1(X, Y);
                        crosslinker.scan1 = mainForm.environment.getScan(X, Y, Parameters.finishColor);
                        positionCounter++;
                        drawPieSupposedZone(preciseMapBmp);
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
                if (crosslinker.isPointInSupposedZone(X, Y))
                {
                    if (mainForm.environment.canRobotStayOnThisPoint(X, Y))
                    {
                        crosslinker.setCenter2(X, Y);
                        positionCounter = 0;
                        interfaceDrawing(preciseMapBmp, graphics);
                    }
                }
            }
        }

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
            mainForm.drawBitmapOnPictureBox1(preciseMapBmp);
        }
    }
}

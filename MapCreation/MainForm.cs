using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System;
using System.Collections.Generic;

namespace MapCreation
{
    public partial class MainForm : Form
    {//TODO проверка на отсутствие белых пикселей на indoor-карте в зоне supposed пути (от scan0 до scan1) робота (пока что сектор)
        //Как рисовать мелкую картинку с BPP == 4, а не 3: в PS сохраняем как bmp, а настройки такие: Изображение->Режим->RGB, 8 бит/канал
        public MainForm()
        {
            InitializeComponent();
            button1.MouseClick += button1_MouseClick;
            environment = new Environment(@"./Maps/PreciseMap1.png"); //default map
            updateEnvironmentProjection();
            setMode1();
        }

        /// <summary>
        /// Загружаем новую карту для environment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.RestoreDirectory = true;
                dlg.Title = "Open Image";
                dlg.Filter = "images (*.png;*.jpg;*bmp)|*.png;*.jpg;*bmp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    String s = dlg.FileName;
                    environment = new Environment(s);
                    updateEnvironmentProjection();
                }
            }
        }

        public Environment environment;

        private Mode1ManualCrosslinking mode1ManualCrosslinking;
        private Mode2ManualMapCreation mode2ManualMapCreation;
        private Mode3MapCreation mode3MapCreation;

        private void setMode1()
        {
            disposeModes();
            if (environment.isMapLoaded() == 1)
                mode1ManualCrosslinking = new Mode1ManualCrosslinking(this);
        }

        private void setMode2()
        {
            disposeModes();
            if (environment.isMapLoaded() == 1)
                mode2ManualMapCreation = new Mode2ManualMapCreation(this);
        }

        private void setMode3()
        {
            disposeModes();
            if (environment.isMapLoaded() == 1)
                mode3MapCreation = new Mode3MapCreation(this);
        }

        private void disposeModes()
        {
            if (mode1ManualCrosslinking != null)
                mode1ManualCrosslinking.destroy();
            if (mode2ManualMapCreation != null)
                mode2ManualMapCreation.destroy();
            if (mode3MapCreation != null)
                mode3MapCreation.destroy();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Parameters.setN_phi((int)numericUpDown6n_phi.Value);
            if (Parameters.getR_robot() != (int)numericUpDown2r_robot.Value)
            {
                Parameters.setR_robot((int)numericUpDown2r_robot.Value);
                environment.recalculateIndoorMap();
            }
            Parameters.setR_scan((int)numericUpDown1r_scan.Value);
            Parameters.setSgm_lmax((int)numericUpDown3sgm_lmax.Value);
            Parameters.setSgm_psi_deg((int)numericUpDown4sgm_psi.Value);
            Environment.setR_scanNoiseMode((byte)numericUpDown5scan_noise.Value);

            setMode1();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            setMode1();
        }
        
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            setMode2();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            setMode3();
        }
    }
}

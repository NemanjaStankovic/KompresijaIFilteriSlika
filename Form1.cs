
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMS
{
    public partial class Form1 : Form
    {
        private System.Drawing.Bitmap m_Bitmap;
        private System.Drawing.Bitmap m_Undo;
        private double Zoom = 1.0;
        
        public Form1()
        {
            InitializeComponent();
            m_Bitmap = new Bitmap(2, 2);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(m_Bitmap, new Rectangle(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, (int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom)));
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|GIF files(*.gif)|*.gif|PNG files(*.png)|*.png|All valid files|*.bmp/*.jpg/*.gif/*.png";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                m_Bitmap = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false);
                //pictureBox2.Load(openFileDialog.FileName);
                this.AutoScroll = true;
                this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
                this.Invalidate();
            }
        }

        private void downscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_Undo = (Bitmap)m_Bitmap.Clone();
            Downscale();
            this.Invalidate();
            //    double Y, Pr, Pb;
            //    double pixelR, pixelG, pixelB;

            //    //Bitmap img = new Bitmap(pictureBox1.Image); m_Bitmap 

            //    for (int i = quadY; i < quadY + 8; i++)
            //    {
            //        for (int j = quadX; j < quadX + 8; j++)
            //        {
            //            pixelR = img.GetPixel(j, i).R;
            //            pixelG = img.GetPixel(j, i).G;
            //            pixelB = img.GetPixel(j, i).B;

            //            Y = (219.0 * (0.59 * pixelR + 0.30 * pixelG + 0.11 * pixelB) / 255.0) + 16.0;
            //            Pr = (224.0 * (0.50 * pixelR - 0.42 * pixelG - 0.08 * pixelB) / 255.0) + 128.0;
            //            Pb = (224.0 * (-0.17 * pixelR - 0.33 * pixelG + 0.50 * pixelB) / 255.0) + 128.0;
            //            SetYSpace(i, j, Y, Pr, Pb);
            //        }
            //    }

        }
        public void Downscale()
        {
            int blue, green, red, blue2, green2, red2;
            double y, y2, Cb, Cr;
            BitmapData bmData = m_Bitmap.LockBits(new Rectangle(0, 0, m_Bitmap.Width, m_Bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - m_Bitmap.Width * 6;

                for (int z = 0; z < m_Bitmap.Height; ++z)
                {
                    for (int x = 0; x < m_Bitmap.Width/2; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];
                        blue2 = p[3];
                        green2 = p[4];
                        red2 = p[5];

                        y = 0.2989 * red / 256 + 0.5866 * green / 256 + 0.1145 * blue / 256;
                        y2 = 0.2989 * red2 / 256 + 0.5866 * green2/ 256 + 0.1145 * blue2 / 256;
                        Cb = -0.1687 * red / 256 - 0.3313 * green/ 256 + 0.5000 * blue/ 256;
                        Cr = 0.5000 * red / 256 - 0.4184 * green / 256 - 0.0816 * blue / 256;

                        p[0] = ((byte)((byte)Max(0.0f, Min(1.0f, (float)(y + 0.0000 * Cb + 1.4022 * Cr)))*255));
                        p[1] = ((byte)((byte)Max(0.0f, Min(1.0f, (float)(y - 0.3456 * Cb - 0.7145 * Cr)))*255));
                        p[2] = ((byte)((byte)Max(0.0f, Min(1.0f, (float)(y + 1.7710 * Cb + 0.0000 * Cr)))*255));

                        p[3] = ((byte)((byte)Max(0.0f, Min(1.0f, (float)(y2 + 0.0000 * Cb + 1.4022 * Cr))) * 255));
                        p[4] = ((byte)((byte)Max(0.0f, Min(1.0f, (float)(y2 - 0.3456 * Cb - 0.7145 * Cr))) * 255));
                        p[5] = ((byte)((byte)Max(0.0f, Min(1.0f, (float)(y2 + 1.7710 * Cb + 0.0000 * Cr))) * 255));
                        p += 6;
                    }
                    p += nOffset;
                }
            }
            m_Bitmap.UnlockBits(bmData);

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                m_Bitmap.Save(saveFileDialog.FileName);
            }
        }

        static float Min(float a, float b)
        {
            return a <= b ? a : b;
        }

        static float Max(float a, float b)
        {
            return a >= b ? a : b;
        }

        private void uTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_Undo = (Bitmap)m_Bitmap.Clone();

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter = "All valid files|*.txt";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                File.WriteAllText(saveFileDialog.FileName, Boja.shannonFano(m_Bitmap));
            }

            
        }

        private void izTxtUSlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String path="";
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "All valid files|*.txt";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                m_Bitmap = Boja.izFajl(openFileDialog.FileName);
                this.Invalidate();
            }
        }

        private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContrastPom dlg = new ContrastPom();
            dlg.ConTxt = 0;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                m_Undo = (Bitmap)m_Bitmap.Clone();
                if (Boja.Contrast(m_Bitmap, (sbyte)dlg.ConTxt))
                    this.Invalidate();
            }
        }

        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_Undo = (Bitmap)m_Bitmap.Clone();
            if (Boja.Sharpen(m_Bitmap, 11))
                this.Invalidate();
        }

        private void edgeDetectDifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContrastPom dlg = new ContrastPom();
            dlg.ConTxt = 0;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                m_Undo = (Bitmap)m_Bitmap.Clone();
                if (Boja.EdgeDetectDifference(m_Bitmap, (byte)dlg.ConTxt))
                    this.Invalidate();
            }
        }

        private void randomJitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContrastPom dlg = new ContrastPom();
            dlg.ConTxt = 0;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                m_Undo = (Bitmap)m_Bitmap.Clone();
                if (Boja.RandomJitter(m_Bitmap, (byte)dlg.ConTxt))
                    this.Invalidate();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_Undo != null)
            {
                m_Bitmap = m_Undo;
                this.Invalidate();
            }
                
        }
    }
}

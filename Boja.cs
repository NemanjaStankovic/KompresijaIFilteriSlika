using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace MMS
{
    public class ConvMatrix
    {
        public int TopLeft = 0, TopMid = 0, TopRight = 0;
        public int MidLeft = 0, Pixel = 1, MidRight = 0;
        public int BottomLeft = 0, BottomMid = 0, BottomRight = 0;
        public int Factor = 1;
        public int Offset = 0;
        public void SetAll(int nVal)
        {
            TopLeft = TopMid = TopRight = MidLeft = Pixel = MidRight = BottomLeft = BottomMid = BottomRight = nVal;
        }
    }

    public class Boja
    {
        public System.Drawing.Color Vrednost { get; set; }
        public int Probability { get; set; }
        public string Code { get; set; }

        public Boja(Color b, int p, string c)
        {
            Vrednost = b;
            Probability = p;
            Code = c;
        }
        public static String shannonFano(Bitmap mapa)
        {
            List<Boja> pom = izracunajProbability(mapa);
            int start = 0;
            int end = pom.Count-1;
            GenerateCodes(pom, start, end);
            String zaUpis = "";
            zaUpis += mapa.Width + ":" + mapa.Height+"\n";
            zaUpis += novaMapa(pom, mapa);
            for (int i = 0; i < pom.Count; i++)
            {
                zaUpis += "\n";
                zaUpis += pom[i].Code+","+pom[i].Vrednost.A+"," + pom[i].Vrednost.R + "," + pom[i].Vrednost.G + "," + pom[i].Vrednost.B + "," + pom[i].Probability;
            }

            return zaUpis;
        }

        public static String novaMapa(List<Boja> kodBoja, Bitmap mapa)
        {
            String kod="";
            for (int i = 0; i < mapa.Height; i++)
            {
                for (int j = 0; j < mapa.Width; j++)
                {
                    Color c = mapa.GetPixel(j, i);
                    Boja nadjena = kodBoja.Find(p => p.Vrednost == c);
                    if (nadjena != null)
                        kod += nadjena.Code;
                }
            }
            return kod;

        }
        public static List<Boja> izracunajProbability(Bitmap mapa)
        {
            List<Boja> pom = new List<Boja>();
            for (int i = 0; i < mapa.Height; i++)
            {
                for (int j = 0; j < mapa.Width; j++)
                {
                    System.Drawing.Color c = mapa.GetPixel(j, i);
                    Boja nadjen = pom.Find(obj => obj.Vrednost == c);
                    if (nadjen == null)
                        pom.Add(new Boja(c, 1, ""));
                    else
                    {
                        nadjen.Probability += 1;
                    }
                }
            }
            pom = pom.OrderByDescending(p => p.Probability).ToList();
            return pom;
        }
        private static void GenerateCodes(List<Boja> boje, int start, int end)
        {
            if (start >= end)
                return;

            double totalProb = 0.0;
            for (int i = start; i <= end; i++)
                totalProb += boje[i].Probability;

            double halfProb = totalProb / 2.0;
            double currentProb = 0.0;
            int splitIndex = -1;

            for (int i = start; i <= end; i++)
            {
                currentProb += boje[i].Probability;
                if (currentProb >= halfProb)
                {
                    splitIndex = i;
                    break;
                }
            }

            for (int i = start; i <= end; i++)
            {
                if (i <= splitIndex)
                    boje[i].Code += "0";
                else
                    boje[i].Code += "1";
            }

            GenerateCodes(boje, start, splitIndex);
            GenerateCodes(boje, splitIndex + 1, end);
        }

        public static Bitmap izFajl(String path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;

                // Read each line until a new row is encountered
                line = reader.ReadLine();
                string[] atributiBoje = line.Split(':');
                int sirina = Int32.Parse(atributiBoje[0]);
                int visina = Int32.Parse(atributiBoje[1]);
                string slika = reader.ReadLine();
                List<Boja> boje = new List<Boja>();

                while ((line = reader.ReadLine()) != null)
                {
                    atributiBoje = line.Split(',');
                    Color c = Color.FromArgb(Int32.Parse(atributiBoje[1]), Int32.Parse(atributiBoje[2]), Int32.Parse(atributiBoje[3]), Int32.Parse(atributiBoje[4]));
                    boje.Add(new Boja(c, Int32.Parse(atributiBoje[5]), atributiBoje[0]));
                    boje = boje.OrderByDescending(p => p.Probability).ToList();
                    
                }
                String zaAnalizu = "";
                Bitmap novaMapa= new Bitmap(sirina, visina);
                int x = 0, y = 0;
                for (int i = 0; i < slika.Length; i++)
                {
                    zaAnalizu+=slika[i];
                    Boja b = boje.Find(p => p.Code == zaAnalizu);
                    if (b != null)
                    {
                        novaMapa.SetPixel(x, y, b.Vrednost);
                        zaAnalizu = "";
                        if (x < sirina-1)
                            x += 1;
                        else { y += 1; x = 0; }
                    }
                }
                return novaMapa;
            }
        }

        public static bool Contrast(Bitmap b, sbyte nContrast)
        {
            if (nContrast < -100) return false;
            if (nContrast > 100) return false;

            double pixel = 0, contrast = (100.0 + nContrast) / 100.0;

            contrast *= contrast;

            int red, green, blue;

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        pixel = red / 255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[2] = (byte)pixel;

                        pixel = green / 255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[1] = (byte)pixel;

                        pixel = blue / 255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[0] = (byte)pixel;

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        public static bool Sharpen(Bitmap mapa, int tezina)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(0);
            m.Pixel = tezina;
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = -2;
            m.Factor = tezina - 8;

            return Conv3x3(mapa, m);
        }

        private static bool Conv3x3(Bitmap mapa, ConvMatrix m)
        {
            if (0 == m.Factor) return false;

            Bitmap bSrc = (Bitmap)mapa.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = mapa.LockBits(new Rectangle(0, 0, mapa.Width, mapa.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride; //kolko bita po liniji piksela
            int stride2 = stride * 2;   //puta 2
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = stride - mapa.Width * 3;
                int nWidth = mapa.Width - 2;
                int nHeight = mapa.Height - 2;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)  //ako je intenzitet sredisnjeg piksela jaci od onih koji ga okruzuju poveca mu se intenzitet inace se smanji
                    {
                        nPixel = ((((pSrc[2] * m.TopLeft) + (pSrc[5] * m.TopMid) + (pSrc[8] * m.TopRight) +
                            (pSrc[2 + stride] * m.MidLeft) + (pSrc[5 + stride] * m.Pixel) + (pSrc[8 + stride] * m.MidRight) +
                            (pSrc[2 + stride2] * m.BottomLeft) + (pSrc[5 + stride2] * m.BottomMid) + (pSrc[8 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[5 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[1] * m.TopLeft) + (pSrc[4] * m.TopMid) + (pSrc[7] * m.TopRight) +
                            (pSrc[1 + stride] * m.MidLeft) + (pSrc[4 + stride] * m.Pixel) + (pSrc[7 + stride] * m.MidRight) +
                            (pSrc[1 + stride2] * m.BottomLeft) + (pSrc[4 + stride2] * m.BottomMid) + (pSrc[7 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * m.TopLeft) + (pSrc[3] * m.TopMid) + (pSrc[6] * m.TopRight) +
                            (pSrc[0 + stride] * m.MidLeft) + (pSrc[3 + stride] * m.Pixel) + (pSrc[6 + stride] * m.MidRight) +
                            (pSrc[0 + stride2] * m.BottomLeft) + (pSrc[3 + stride2] * m.BottomMid) + (pSrc[6 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[3 + stride] = (byte)nPixel;

                        p += 3;
                        pSrc += 3;
                    }
                    p += nOffset;
                    pSrc += nOffset;
                }
            }

            mapa.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }
        public static bool EdgeDetectDifference(Bitmap mapa, byte nTreshold)
        {
            Bitmap mapa2 = (Bitmap)mapa.Clone();

            BitmapData bmData = mapa.LockBits(new Rectangle(0, 0, mapa.Width, mapa.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmData2 = mapa2.LockBits(new Rectangle(0, 0, mapa2.Width, mapa2.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr Scan02 = bmData2.Scan0;

            unsafe{
                byte* p = (byte*)(void*)Scan0;
                byte* p2 = (byte*)(void*)Scan02;

                int nOffset = stride - 3 * mapa.Width;
                int sirinaUByte = mapa.Width * 3;

                int nPixel = 0, m = 0;

                p += stride+3;
                p2 += stride+3;

                for (int i = 1; i < mapa.Height - 1; ++i)
                {
                    for (int j = 3; j < sirinaUByte-3; ++j)
                    {
                        m = Math.Abs((p2 - stride + 3)[0] - (p2 + stride - 3)[0]); //C-G
                        nPixel = Math.Abs((p2+ stride + 3)[0]-(p2-stride -3)[0]);  //I-A
                        if (nPixel > m) m = nPixel;

                        nPixel = Math.Abs((p2 - stride)[0] - (p2 + stride)[0]); //H-B
                        if (nPixel > m) m = nPixel;

                        nPixel = Math.Abs((p2 + 3)[0] - (p2 - 3)[0]); //F-D
                        if (nPixel > m) m = nPixel;

                        if (m < nTreshold) m = 0;

                        p[0] = (byte)m;

                        ++p;    //radi posebno za r,g i b
                        ++p2;
                    }
                    p += 6 + nOffset;   //prelazak na novi red (preskace se zadnji piksel i prvi sledeceg reda
                    p2 += 6 + nOffset;
                }
                mapa.UnlockBits(bmData);
                mapa2.UnlockBits(bmData2);
            }
            return true;
        }
        public static bool RandomJitter(Bitmap mapa, int nDegree)
        {
            int newX, newY;
            short nHalf = (short)Math.Floor((double)nDegree / 2);
            Random rnd = new Random();
            
            int sirina = mapa.Width;
            int visina = mapa.Height;

            Point[,] ptRandJitter = new Point[sirina, visina];

            for (int x = 0; x < sirina; x++)
            {
                for (int y = 0; y < visina; y++)
                {
                    newX = rnd.Next(nDegree) - nHalf;
                    if (x + newX > 0 && x + newX < sirina)
                        ptRandJitter[x, y].X = newX;
                    else
                        ptRandJitter[x, y].X = 0;

                    newY = rnd.Next(nDegree) - nHalf;
                    if (y + newY > 0 && y + newY < sirina)
                        ptRandJitter[x, y].Y = newY;
                    else
                        ptRandJitter[x, y].Y = 0;
                }
            }

            Bitmap mapa2 = (Bitmap)mapa.Clone();

            BitmapData bmData = mapa.LockBits(new Rectangle(0, 0, mapa.Width, mapa.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = mapa2.LockBits(new Rectangle(0, 0, mapa2.Width, mapa2.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int sirinauByte = bmData.Stride;

            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;
                int nWidth = mapa.Width;
                int nHeight = mapa.Height;
                int nOffset = bmData.Stride - nWidth * 3;

                int xOffset, yOffset;

                for (int y = 0; y < nHeight; y++)
                {
                    for (int x = 0; x < nWidth; x++)
                    {
                        xOffset = ptRandJitter[x, y].X;
                        yOffset = ptRandJitter[x, y].Y;
                        if (x + xOffset < nWidth && x + xOffset >= 0 && y + yOffset >= 0 && y + yOffset < nHeight)
                        {
                            p[0] = pSrc[(x + xOffset) * 3 + (y + yOffset) * sirinauByte];
                            p[1] = pSrc[(x + xOffset) * 3 + (y + yOffset) * sirinauByte+1];
                            p[2] = pSrc[(x + xOffset) * 3 + (y + yOffset) * sirinauByte+2];
                        }
                        p += 3;
                    }
                    p += nOffset;
                }
                mapa.UnlockBits(bmData);
                mapa2.UnlockBits(bmSrc);

                return true;
            }

        }
    }
    
}

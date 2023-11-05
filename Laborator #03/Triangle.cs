using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

// ======================
// Laborator #03
// Bîrsan Dorin-Alexandru
// grupa 3132a
// ======================

namespace Laborator__03
{
    class Triangle
    {
        private Vector3 pointA;
        private Vector3 pointB;
        private Vector3 pointC;
        private Color color;
        private bool visibility;
        private float linewidth;
        private float pointsize;
        private PolygonMode polMode;
        static private string numeFisier = "coordonate.txt";
        static private int minim = -20;
        static private int maxim = 20;

        public Triangle()
        {
            // Laborator #03
            // 8. coordonatele acestuia vor fi încărcate dintr-un fișier text
            int[] coordonate = new int[9];

            if (!File.Exists(numeFisier))
            {
                Console.WriteLine($"Fisierul {numeFisier} nu exista.");

                string[] lines = {
                    "0",
                    "0",
                    "12",
                    "0",
                    "-20",
                    "0",
                    "18",
                    "0",
                    "0"
                };

                try
                {
                    using (StreamWriter sw = new StreamWriter(numeFisier))
                    {
                        foreach (string line in lines)
                        {
                            sw.WriteLine(line);
                        }
                    }

                    Console.WriteLine("Fisierul \"coordonate.txt\" a fost creat cu succes.");
                }
                catch (IOException e)
                {
                    Console.WriteLine("A aparut o eroare la operarea cu fisierul: " + e.Message);
                }
            }

            if (File.Exists(numeFisier))
            {
                coordonate = CitesteNumere(numeFisier, minim, maxim);

                if (coordonate != null)
                {
                    Console.WriteLine("Coordonatele citite din fisier sunt:");
                    Console.WriteLine("(" + coordonate[0] + ", " + coordonate[1] + ", " + coordonate[2] + ")");
                    Console.WriteLine("(" + coordonate[3] + ", " + coordonate[4] + ", " + coordonate[5] + ")");
                    Console.WriteLine("(" + coordonate[6] + ", " + coordonate[7] + ", " + coordonate[8] + ")");
                }
                else
                {
                    Console.WriteLine("Nu s-au gasit numere valide în fisier.");
                    coordonate = new int[9];
                }
            }
            else
                Console.WriteLine($"Fisierul {numeFisier} nu exista.");

            Console.WriteLine();

            pointA = new Vector3(coordonate[0], coordonate[1], coordonate[2]);
            pointB = new Vector3(coordonate[3], coordonate[4], coordonate[5]);
            pointC = new Vector3(coordonate[6], coordonate[7], coordonate[8]);
            ChangeColor(0);

            Inits();
        }

        static int[] CitesteNumere(string numeFisier, int minim, int maxim)
        {
            string[] linii = File.ReadAllLines(numeFisier);
            int[] numere = new int[9];
            int numereValide = 0;

            foreach (string linie in linii)
            {
                if (int.TryParse(linie, out int numar) && numar >= minim && numar <= maxim)
                {
                    numere[numereValide] = numar;
                    numereValide++;

                    if (numereValide == 9)
                        return numere;
                }
            }

            return null;
        }

        private void Inits()
        {
            visibility = true;
            linewidth = 3.0f;
            pointsize = 3.0f;
            polMode = PolygonMode.Fill;
        }

        public void Draw()
        {
            if (visibility)
            {
                GL.PointSize(pointsize);
                GL.LineWidth(linewidth);
                GL.PolygonMode(MaterialFace.FrontAndBack, polMode);
                GL.Begin(PrimitiveType.Triangles);
                GL.Color3(color);
                GL.Vertex3(pointA);
                GL.Vertex3(pointB);
                GL.Vertex3(pointC);
                GL.End();
            }
        }

        public void ChangeColor(int canal)
        {
            Random random = new Random();

            int genR = color.R;
            int genG = color.G;
            int genB = color.B;

            switch (canal)
            {
                case 0:
                    genR = random.Next(0, 256);
                    genG = random.Next(0, 256);
                    genB = random.Next(0, 256);
                    break;

                case 1:
                    genR = random.Next(0, 256);
                    Console.WriteLine("S-a schimbat valoarea canalului RED");
                    break;

                case 2:
                    genG = random.Next(0, 256);
                    Console.WriteLine("S-a schimbat valoarea canalului GREEN");
                    break;

                case 3:
                    genB = random.Next(0, 256);
                    Console.WriteLine("S-a schimbat valoarea canalului BLUE");
                    break;
            }

            color = Color.FromArgb(genR, genG, genB);

            Console.WriteLine("RGB(" + genR.ToString() + ", " + genG.ToString() + ", " + genB.ToString() + ")");
        }
    }
}

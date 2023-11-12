using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

// ======================
// Laborator #04
// Bîrsan Dorin-Alexandru
// grupa 3132a
// ======================

namespace Laborator__04
{
    class Cube
    {
        private Color color;

        private int[,] objVertices = new int[3, 36];
        private Color[] colorVertices = { 
            Color.White, 
            Color.LawnGreen, 
            Color.WhiteSmoke, 
            Color.Tomato, 
            Color.Turquoise, 
            Color.OldLace, 
            Color.Olive, 
            Color.OliveDrab, 
            Color.PowderBlue, 
            Color.PeachPuff, 
            Color.LavenderBlush, 
            Color.MediumAquamarine };

        public Cube()
        {
            string NumeFisier = "coordonate_lab4.txt";
            string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string caleCompletaFisier = locatieFisierSolutie + "\\" + NumeFisier;

            AdministrareFisier adminFisier = new AdministrareFisier(caleCompletaFisier);

            objVertices = adminFisier.GetCoordonate();
        }

        public void DrawCube()
        {
            GL.Begin(PrimitiveType.Triangles);
            for (int i = 0; i < 35; i = i + 3)
            {
                //For i As Integer = 0 To 35 Step 3
                GL.Color3(colorVertices[i / 3]);
                GL.Vertex3(objVertices[0, i], objVertices[1, i], objVertices[2, i]);
                GL.Vertex3(objVertices[0, i + 1], objVertices[1, i + 1], objVertices[2, i + 1]);
                GL.Vertex3(objVertices[0, i + 2], objVertices[1, i + 2], objVertices[2, i + 2]);
            }
            GL.End();
        }

        public void ChangeColor(int canal, bool full)
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

            if(full)
            {
                for (int i = 0; i < 12; i++)
                {
                    colorVertices[i] = color;
                }
            }
            else
            {
                colorVertices[6] = color;
                colorVertices[7] = color;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

// ======================
// Laborator #04
// Bîrsan Dorin-Alexandru
// grupa 3132a
// ======================

namespace Laborator__04
{
    internal class Program : GameWindow
    {
        float rotation_speed = 0.0f;
        bool showCube = true;
        KeyboardState lastKeyPress;
        private const int XYZ_SIZE = 75;
        private bool axesControl = true;
        private int transStep = 0;
        private int radStep = 0;
        private int attStep = 0;

        private Cube cube;

        private bool newStatus = false;

        private Program() : base(800, 600, new GraphicsMode(32, 24, 0, 16)) {}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.MidnightBlue);
            GL.Enable(EnableCap.DepthTest);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);

            cube = new Cube();

            DisplayHelp();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 lookat = Matrix4.LookAt(30, 30, 30, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            showCube = true;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard[Key.Escape])
            {
                Exit();
                return;
            }

            if (keyboard[Key.P] && !keyboard.Equals(lastKeyPress))
            {
                if (showCube)
                {
                    showCube = false;
                }
                else
                {
                    showCube = true;
                }
            }

            if (keyboard[Key.R] && !keyboard.Equals(lastKeyPress))
            {
                if (newStatus)
                {
                    newStatus = false;
                }
                else
                {
                    newStatus = true;
                }
            }

            if (keyboard[Key.T] && !keyboard.Equals(lastKeyPress))
            {
                if (rotation_speed == 0.0f)
                {
                    rotation_speed = 1.0f;
                }
                else
                {
                    rotation_speed = 0.0f;
                }
            }

            if (keyboard.IsKeyDown(Key.Number1) && !keyboard.Equals(lastKeyPress))
            {
                cube.ChangeColor(1, false);
            }

            if (keyboard.IsKeyDown(Key.Number2) && !keyboard.Equals(lastKeyPress))
            {
                cube.ChangeColor(2, false);
            }

            if (keyboard.IsKeyDown(Key.Number3) && !keyboard.Equals(lastKeyPress))
            {
                cube.ChangeColor(3, false);
            }

            if (keyboard.IsKeyDown(Key.AltLeft) && keyboard.IsKeyDown(Key.Number1) && !keyboard.Equals(lastKeyPress))
            {
                cube.ChangeColor(1, true);
            }

            if (keyboard.IsKeyDown(Key.AltLeft) && keyboard.IsKeyDown(Key.Number2) && !keyboard.Equals(lastKeyPress))
            {
                cube.ChangeColor(2, true);
            }

            if (keyboard.IsKeyDown(Key.AltLeft) && keyboard.IsKeyDown(Key.Number3) && !keyboard.Equals(lastKeyPress))
            {
                cube.ChangeColor(3, true);
            }

            if (keyboard[Key.A])
            {
                transStep--;
            }
            if (keyboard[Key.D])
            {
                transStep++;
            }

            if (keyboard[Key.W])
            {
                radStep--;
            }
            if (keyboard[Key.S])
            {
                radStep++;
            }

            if (keyboard[Key.Up])
            {
                attStep++;
            }
            if (keyboard[Key.Down])
            {
                attStep--;
            }

            lastKeyPress = keyboard;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (newStatus)
            {
                cube.DrawCube();
            }

            GL.Rotate(rotation_speed, 0.0f, 1.0f, 0.0f);

            if (axesControl)
            {
                DrawAxes();
            }

            if (showCube == true)
            {
                GL.PushMatrix();
                GL.Translate(transStep, attStep, radStep);
                cube.DrawCube();
                GL.PopMatrix();
            }

            GL.Flush();

            SwapBuffers();
        }

        private void DrawAxes()
        {
            // Desenează axa Ox (cu roșu).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(XYZ_SIZE, 0, 0);
            GL.End();

            // Desenează axa Oy (cu galben).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, XYZ_SIZE, 0); ;
            GL.End();

            // Desenează axa Oz (cu verde).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, XYZ_SIZE);
            GL.End();
        }

        private void DisplayHelp()
        {
            Console.WriteLine("======================");
            Console.WriteLine("Laborator #04");
            Console.WriteLine("Bîrsan Dorin-Alexandru");
            Console.WriteLine("grupa 3132a");
            Console.WriteLine("======================");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("1");
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("2");
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("3");
            Console.ResetColor();
            Console.WriteLine(" pentru a modifica culoarea fetei de sus a cubului (valori random) 1-RED, 2-GREEN, 3-BLUE");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Alt + 1");
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Alt + 2");
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Alt + 3");
            Console.ResetColor();
            Console.WriteLine(" pentru a modifica culoarea tuturor fetelor cubului (valori random)");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("T");
            Console.ResetColor();
            Console.WriteLine(" pentru a activa/dezactiva rotirea cubului");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("P");
            Console.ResetColor();
            Console.WriteLine(" pentru a ascunde/afisa cubul");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("R");
            Console.ResetColor();
            Console.WriteLine(" pentru a ascunde/afisa un al doilea cub");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("W");
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("A");
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("S");
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("D");
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Up");
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Down");
            Console.ResetColor();
            Console.WriteLine(" pentru a misca cubul");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\nESC");
            Console.ResetColor();
            Console.WriteLine(" pentru a inchide programul");
            Console.WriteLine();
        }

        [STAThread]
        static void Main(string[] args)
        {

            using (Program example = new Program())
            {
                example.Run(30.0, 0.0);
            }

        }
    }
}

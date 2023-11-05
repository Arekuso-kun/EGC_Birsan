using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

// ======================
// Laborator #03
// Bîrsan Dorin-Alexandru
// grupa 3132a
// ======================

namespace Laborator__03
{
    class Program : GameWindow
    {
        private const int XYZ_SIZE = 75;

        private KeyboardState previousKeyboard;
        private Triangle triangle;

        private float cameraYaw = 0.0f; 
        private float cameraPitch = 0.0f; 
        private float cameraSpeed = 0.1f;  
        private Vector2 lastMousePos;
        private bool isMouseCaptured = false;

        public Program() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            Console.WriteLine("OpenGl versiunea: " + GL.GetString(StringName.Version));
            Title = "OpenGl versiunea: " + GL.GetString(StringName.Version) + " (mod imediat)";

            DisplayHelp();

            triangle = new Triangle();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Blue);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
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


        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState currentKeyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (currentKeyboard[Key.Escape])
            {
                Exit();
            }

            // Laborator #03
            // 9. Modificați aplicația pentru a manipula valorile RGB pentru fiecare
            // vertex ce definește un triunghi.Afișați valorile RGB în consolă.
            if (currentKeyboard.IsKeyDown(OpenTK.Input.Key.R) && !currentKeyboard.Equals(previousKeyboard))
            {
                triangle.ChangeColor(1);
            }

            if (currentKeyboard.IsKeyDown(OpenTK.Input.Key.G) && !currentKeyboard.Equals(previousKeyboard))
            {
                triangle.ChangeColor(2);
            }

            if (currentKeyboard.IsKeyDown(OpenTK.Input.Key.B) && !currentKeyboard.Equals(previousKeyboard))
            {
                triangle.ChangeColor(3);
            }

            if (Mouse.GetState().IsButtonDown(MouseButton.Left) && !isMouseCaptured)
            {
                isMouseCaptured = true;
                lastMousePos = new Vector2(mouse.X, mouse.Y);
                CursorVisible = false;
            }
            else if (!Mouse.GetState().IsButtonDown(MouseButton.Left) && isMouseCaptured)
            {
                isMouseCaptured = false;
                CursorVisible = true;
            }

            if (isMouseCaptured)
            {
                Vector2 currentMousePos = new Vector2(mouse.X, mouse.Y);
                Vector2 mouseDelta = currentMousePos - lastMousePos;
                lastMousePos = currentMousePos;

                cameraYaw -= mouseDelta.X * cameraSpeed;
                cameraPitch -= mouseDelta.Y * cameraSpeed;

                if (cameraPitch > 89.0f)
                    cameraPitch = 89.0f;
                if (cameraPitch < -89.0f)
                    cameraPitch = -89.0f;

                Matrix4 lookat = Matrix4.LookAt(
                    new Vector3(30, 30, 30),
                    new Vector3(0, 0, 0),
                    new Vector3(0, 1, 0)
                );

                lookat *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(cameraPitch));
                lookat *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(cameraYaw));

                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadMatrix(ref lookat);
            }

            previousKeyboard = currentKeyboard;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            DrawAxes();

            triangle.Draw();

            SwapBuffers();
        }

        private void DrawAxes()
        {
            GL.PointSize(30.0f);
            GL.Begin(PrimitiveType.Points);

            GL.Color3(Color.Aqua);
            GL.Vertex3(0, 0, 0);

            GL.End();

            // Laborator #03
            // 1. Desenați axele de coordonate din aplicația-template folosind un singur apel GL.Begin().
            GL.LineWidth(3.0f);
            GL.Begin(PrimitiveType.Lines);

            // Desenează axa Ox (cu roșu).
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(XYZ_SIZE, 0, 0);

            // Desenează axa Oy (cu galben).
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, XYZ_SIZE, 0);

            // Desenează axa Oz (cu verde).
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, XYZ_SIZE);

            GL.End();
        }

        private void DisplayHelp()
        {
            Console.WriteLine("======================");
            Console.WriteLine("Laborator #03");
            Console.WriteLine("Bîrsan Dorin-Alexandru");
            Console.WriteLine("grupa 3132a");
            Console.WriteLine("======================");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("R");
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("G");
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("B");
            Console.ResetColor();
            Console.WriteLine(" pentru a modifica culoarea triunghiului (valori random)");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Left Mouse Button");
            Console.ResetColor();
            Console.WriteLine(" pentru a modifica unghiului camerei");

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

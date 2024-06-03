using System.Diagnostics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

using Anda.AndaFramework;
using AndaFramework.Graphics.Shader;
using AndaFramework.Graphics.Drawable;
using Path = AndaFramework.Collections.Path;
using AndaFramework.Graphics;
using AndaFramework.Input;
using AndaFramework.Logging;
using Convert = AndaFramework.Utils.Convert;

namespace Anda
{
    public sealed class AndaWindowRuntime : GameWindow
    {
        // Integer
        private int _vertexArray;
        private int _buffer;

        // Float
        private float _time;
        private float _deltaTime;
        private float _fov = 60f;
        private float _speed = 1f;

        // String
        private string _keyPress = "-";

        // List
        private List<AndaDrawable> _drawableObjects = new List<AndaDrawable>();

        //VectorX
        private Vector3 _translation = new Vector3(0, 0, 0);
        private Vector4 _position = new Vector4(0f, 0f, 0f, 1f);

        // MatrixX
        private Matrix4 _projectionMatrix;
        private Matrix4 _model;
        private Matrix4 _view;

        // Framework
        //private Program _program;
        private Input _input;
        private Program _solidProgram;
        private Program _textureProgram;


        public AndaWindowRuntime(int width, int height, string title)
            : base(GameWindowSettings.Default, new NativeWindowSettings()
            {
                Size = (width, height),
                Title = title,
            })
        {
        }

        protected override void OnLoad()
        {
            Stopwatch loadTimer = new Stopwatch();
            Logger.Log("RUNTIME", "Loading runtime....");
            loadTimer.Start();

            CursorVisible = true;
            VSync = VSyncMode.On;

            CreateProjection();

            _solidProgram = new Program();
            _solidProgram.AttachShaderFromFile(ShaderType.VertexShader,
                    "/home/andofwinds/Desktop/Anda/Anda/shader.vert");
            _solidProgram.AttachShaderFromFile(ShaderType.FragmentShader,
                    "/home/andofwinds/Desktop/Anda/Anda/shader.frag");
            _solidProgram.LinkAll();

            _textureProgram = new Program();
            _textureProgram.AttachShaderFromFile(ShaderType.VertexShader,
                    "/home/andofwinds/Desktop/Anda/Anda/texture_shader.vert");
            _textureProgram.AttachShaderFromFile(ShaderType.FragmentShader,
                    "/home/andofwinds/Desktop/Anda/Anda/texture_shader.frag");
            _textureProgram.LinkAll();

            _drawableObjects.Add(
                    new TexturedDrawableObject(
                        VertexFactory.GenerateTexturedCube(0.2f, 256, 256),
                        _textureProgram.Id,
                        "/home/andofwinds/Desktop/Anda/Anda/Textures/dotted.png")
                    );

            _drawableObjects.Add(
                    new DrawableObject(
                            VertexFactory.GenerateRect(new Vector2(0, 0.1f), 0.2f, Convert.RgbaToColor4("#EAB2C2")),
                            _solidProgram.Id
                        )
                    );

            _drawableObjects.Add(
                    new TexturedDrawableObject(
                            VertexFactory.GenerateTexturedRect(new Vector2(0, 0), 0.2f, new Vector2(180, 180), 0.2f),
                            _textureProgram.Id,
                            "/home/andofwinds/Desktop/Anda/Anda/Textures/gmd.png"
                        )
                    );

            _drawableObjects.Add(
                    new TexturedDrawableObject(
                        VertexFactory.GenerateTexturedCube(0.2f, 180, 180),
                        _textureProgram.Id,
                        "/home/andofwinds/Desktop/Anda/Anda/Textures/gmd.png"
                        )
                    );

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
            GL.PointSize(5);
            GL.Enable(EnableCap.DepthTest);


            loadTimer.Stop();
            Logger.Log("RUNTIME", $"Loading completed in {loadTimer.Elapsed.TotalSeconds} seconds! Processing main loop.");
            Console.WriteLine("----- Anda runtime -----");
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"Anda (Framework: {Metadata._ver} | VSync: {VSync} | FPS: {MathF.Round(1f / _deltaTime)} | Frame time: {_deltaTime:F3} | Run time: {_time:F1}) | Input (Last press: {_keyPress})";

            GL.ClearColor(0.1f, 0.2f, 0.3f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Render 

            float xpos = -0.5f;
            int drawablesCount = _drawableObjects.Count + 1;
            foreach (AndaDrawable o in _drawableObjects)
            {
                o.Bind();
                for (int i = 0; i < drawablesCount; i++)
                {
                    //o.Bind();
                    _translation.X = xpos;
                    _model = Matrix4.CreateTranslation(_translation);

                    GL.UniformMatrix4(22, false, ref _model);
                    GL.UniformMatrix4(21, false, ref _view);
                    GL.UniformMatrix4(20, false, ref _projectionMatrix);

                    o.Render();
                }

                xpos += 0.5f;
            }

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            _deltaTime = (float)e.Time;
            _time += _deltaTime;
            float _rspeed = 13f;
            float k = _time * 0.05f;


            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            if (KeyboardState.IsKeyDown(Keys.D))
            {
                _translation.X += _deltaTime * _speed;
            }
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                _translation.X -= _deltaTime * _speed;
            }
            if (KeyboardState.IsKeyDown(Keys.W))
            {
                _translation.Y += _deltaTime * _speed;
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                _translation.Y -= _deltaTime * _speed;
            }

            if (KeyboardState.IsKeyDown(Keys.Home))
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            }
            if (KeyboardState.IsKeyDown(Keys.End))
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            }
            if (KeyboardState.IsKeyDown(Keys.Insert))
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Point);
            }

            if (KeyboardState.IsKeyDown(Keys.LeftBracket))
            {
                _fov -= 1;
                CreateProjection();
            }

            if (KeyboardState.IsKeyDown(Keys.RightBracket))
            {
                _fov += 1;
                CreateProjection();
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            //GL.Viewport(0, 0, Size.X, Size.Y);
            GL.Viewport(0, 0, e.Width, e.Height);
            CreateProjection();
        }

        protected override void OnUnload()
        {
            Logger.Log("RUNTIME", "Exit signal emitted. Exiting.");

            GL.DeleteVertexArray(_vertexArray);
            foreach (AndaDrawable o in _drawableObjects)
            {
                o.Dispose();
            }
            GL.DeleteProgram(_textureProgram.Id);
            GL.DeleteProgram(_solidProgram.Id);

            Console.WriteLine("----- End runtime -----");
        }
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            _keyPress = e.Key.ToString();
        }

        private void CreateProjection()
        {
            float aspectRatio = (float)Size.X / Size.Y;

            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(_fov),
                aspectRatio,
                0.1f,
                4000f);

            _view = Matrix4.CreateTranslation(0f, 0f, -2f);
        }
    }
}



/*
 * TODO
 * - Textured rectangle
 * - Anda SDK
 * - Warns / Errors in AndaFramework.Logging
 * - Code refractoring
 * - Text
 */

using System.Diagnostics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Image = SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

using Anda.AndaFramework;
using AndaFramework.Graphics.Shader;
using AndaFramework.Graphics.Drawable;
using Path = AndaFramework.Collections.Path;
using AndaFramework.Graphics;
using AndaFramework.Input;
using AndaFramework.Logging;
using Convert = AndaFramework.Utils.Convert;
using AndaFramework.Graphics.Text;
using AndaFramework.Graphics.Object;
using AndaFramework.Graphics.Camera;

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
        private float _fov = 45f;
        private float _speed = 1f;

        // String
        private string _keyPress = "-";

        // Boolean
        private bool _isFullscreen = true;

        // List
        private List<AndaDrawable> _drawableObjects = new List<AndaDrawable>();
        private List<AndaObject> _objects = new List<AndaObject>();

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
        private Program _textProgram;
        private AndaObject _testRectObject;
        private AndaCamera _camera;


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

            Directory.SetCurrentDirectory(".");
            CursorVisible = true;
            VSync = VSyncMode.On;

            WindowState = WindowState.Fullscreen;
            WindowBorder = WindowBorder.Hidden;

            CreateProjection();
            AndaFont.GenerateFont();

            _solidProgram = new Program();
            _solidProgram.AttachShaderFromFile(ShaderType.VertexShader,
                    "Shaders/shader.vert");
            _solidProgram.AttachShaderFromFile(ShaderType.FragmentShader,
                    "Shaders/shader.frag");
            _solidProgram.LinkAll();

            _textureProgram = new Program();
            _textureProgram.AttachShaderFromFile(ShaderType.VertexShader,
                    "Shaders/texture_shader.vert");
            _textureProgram.AttachShaderFromFile(ShaderType.FragmentShader,
                    "Shaders/texture_shader.frag");
            _textureProgram.LinkAll();

            _textProgram = new Program();
            _textProgram.AttachShaderFromFile(ShaderType.VertexShader,
                    "Shaders/text_shader.vert");
            _textProgram.AttachShaderFromFile(ShaderType.FragmentShader,
                    "Shaders/text_shader.frag");
            _textProgram.LinkAll();

            /*_testRectObject = new TestObject(
                    new DrawableObject(
                        VertexFactory.GenerateRect(
                                new Vector2(-0.5f, 0.5f), 0.2f, Convert.RgbaToColor4("#EAB2C2")
                            ),
                        _solidProgram.Id
                        ),
                    new Vector4(0, -1f, -2.7f, 0),
                    Vector4.Zero,
                    Vector4.Zero,
                    0
                    );*/

            _testRectObject = new TestObject(
                    new GeneratedMipmapDrawableObject(
                        VertexFactory.GenerateTexturedRect(
                            new Vector2(0, 0),
                            1f,
                            new Vector2(1, 1),
                            1f
                            ),

                        _textureProgram.Id,
                        "Textures/bwa.jpg",
                        8),

                    new Vector4(1f, 0f, 2, 1),
                    Vector4.Zero,
                    new Vector4(20, 0, 0, 0),
                    0
                    );

            AndaObject sphere = new TestObject(
                    new GeneratedMipmapDrawableObject(
                        new DrawableSphere().Create(3),
                        _textureProgram.Id,
                        "Textures/bwa.jpg",
                        8),

                    new Vector4(1f, 0, 2, 1),
                    Vector4.Zero,
                    Vector4.Zero,
                    0
                    );

            AndaDrawable andaTextDrawable = new TexturedDrawableObject(
                    VertexFactory.GenerateCharacter(),
                    _textProgram.Id,
                    "Fonts/Fredoka.ttf.png"
                    );

            AndaText andaText = new AndaText(
                    andaTextDrawable,
                    new Vector4(0, 0, -0.4f, 1),
                    Color4.Red,
                    "Hello world!"
                    );

            //_objects.Add(_testRectObject);
            //_objects.Add(sphere);
            _objects.Add(andaText);

            _camera = new StaticCamera();

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
            GL.PointSize(5);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            loadTimer.Stop();
            Logger.Log("RUNTIME", $"Loading completed in {loadTimer.Elapsed.TotalSeconds} seconds! Processing main loop.");
            Console.WriteLine("----- Anda runtime -----");
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"Anda (Framework: {Metadata._ver} | VSync: {VSync} | FPS: {MathF.Round(1f / _deltaTime)} | Frame time: {_deltaTime:F3} | Run time: {_time:F1}) | Input (Last press: {_keyPress})";
            GL.ClearColor(0.1f, 0.2f, 0.3f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            CreateProjection();

            int lastProg = -1;
            Vector2 off = new Vector2(0.5f, 0);
            foreach (AndaObject o in _objects)
            {
                lastProg = RenderElement(o, lastProg);
            }

            SwapBuffers();
        }

        private int RenderElement(AndaObject obj, int lastProgram)
        {
            int program = obj.Drawable.GetProgram();

            if (lastProgram != program)
            {
                GL.UniformMatrix4(20, false, ref _projectionMatrix);
            }

            lastProgram = obj.Drawable.GetProgram();
            obj.Render(_camera);

            return lastProgram;
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

            if (KeyboardState.IsKeyDown(Keys.Delete))
            {
                ToggleFullscreen();
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
                0.01f,
                4000f);

            _view = Matrix4.CreateTranslation(0f, 0f, -2f);
        }

        private void ToggleFullscreen()
        {
            if (_isFullscreen)
            {
                WindowState = WindowState.Normal;
                WindowBorder = WindowBorder.Resizable;
            }
            else
            {
                WindowState = WindowState.Fullscreen;
                WindowBorder = WindowBorder.Hidden;
            }

            _isFullscreen = !_isFullscreen;
        }
    }
}



/*
 * TODO
 * - Textured rectangle +
 * - Anda SDK
 * - Warns / Errors in AndaFramework.Logging
 * - Code refractoring
 * - Text
 */


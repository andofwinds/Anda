using AndaFramework.Logging;
using OpenTK.Graphics.OpenGL4;

namespace AndaFramework.Graphics.Shader
{
    /// <summary>
    /// Anda Framework's shader
    /// </summary>
    public class Shader
    {
        private int _shader;

        /// <summary>
        /// Creates new shader
        /// </summary>
        /// <param name="path">Path to shader</param>
        /// <param name="type">Type of shader</param>
        public Shader(string path, ShaderType type)
        {
            _shader = GL.CreateShader(type);
            GL.ShaderSource(_shader, File.ReadAllText(path));
        }

        /// <summary>
        /// Compiles current shader
        /// </summary>
        public void Compile()
        {
            GL.CompileShader(_shader);

            GL.GetShader(_shader, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                Logger.Err("SHADER", $"{GL.GetShaderInfoLog(_shader)}");
            }
        }

        /// <summary>
        /// Returns current shader
        /// </summary>
        /// <returns>Current shader</returns>
        public int Get()
        {
            return _shader;
        }

        /// <summary>
        /// Deletes current shader
        /// </summary>
        public void Delete()
        {
            GL.DeleteShader(_shader);
        }
    }


    /// <summary>
    /// Anda Framework's shader program
    /// </summary>
    public class Program : IDisposable
    {
        public int Id => _program;

        private int _program;
        private readonly List<int> _shaders = new List<int>();


        /// <summary>
        /// Creates new shader program
        /// </summary>
        public Program()
        {
            _program = GL.CreateProgram();
        }

        /// <summary>
        /// Attaches shader to current program
        /// </summary>
        /// <param name="shader">Shader to link</param>
        public void AttachShaderFromFile(ShaderType type, string filename)
        {
            int shader = GL.CreateShader(type);
            string source = File.ReadAllText(filename);

            GL.ShaderSource(shader, source);
            GL.CompileShader(shader);

            string infoLog = GL.GetShaderInfoLog(shader);
            if (!string.IsNullOrWhiteSpace(infoLog))
            {
                Logger.Err("SHADER", infoLog);
            }

            _shaders.Add(shader);
        }

        public void LinkAll()
        {
            foreach (int shader in _shaders)
            {
                GL.AttachShader(_program, shader);
            }

            GL.LinkProgram(_program);
            string infoLog = GL.GetProgramInfoLog(_program);
            if (!string.IsNullOrWhiteSpace(infoLog))
            {
                Logger.Err("PROGRAM", infoLog);
            }

            foreach (int shader in _shaders)
            {
                GL.DetachShader(_program, shader);
                GL.DeleteShader(shader);
            }

            Logger.Log("PROGRAM", "New program linked!");
        }

        // -- DISPOSE --

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool Disposing)
        {
            if (Disposing)
            {
                GL.DeleteProgram(_program);
            }
        }
    }
}


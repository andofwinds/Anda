using System;
using OpenTK.Graphics.OpenGL4;

using AndaFramework.Logging;

namespace AndaFramework.Graphics.Drawable
{
    public abstract class AndaDrawable : IDisposable
    {
        protected readonly int _program;
        protected readonly int _vertexArray;
        protected readonly int _buffer;
        protected readonly int _verticesCount;

        protected AndaDrawable(int program, int vertexCount)
        {
            _program = program;
            _verticesCount = vertexCount;
            _vertexArray = GL.GenVertexArray();
            _buffer = GL.GenBuffer();

            GL.BindVertexArray(_vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffer);

            Logger.Log("DRAWABLE", "New Drawable Object created!");
        }

        public virtual void Bind()
        {
            GL.UseProgram(_program);
            GL.BindVertexArray(_vertexArray);
        }

        public virtual int GetProgram()
        {
            return _program;
        }

        public virtual void Render()
        {
            GL.DrawArrays(PrimitiveType.Triangles, 0, _verticesCount);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool Disposing)
        {
            if (Disposing)
            {
                GL.DeleteVertexArray(_vertexArray);
                GL.DeleteBuffer(_buffer);
            }
        }
    }
}

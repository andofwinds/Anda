using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using AndaFramework.Graphics;
using AndaFramework.Logging;

namespace AndaFramework.Graphics.Drawable
{
    public class DrawableObject : AndaDrawable
    {
        private Matrix4 _view;

        public DrawableObject(Vertex[] vertices, int program)
            : base(program, vertices.Length)

        {

            //Create vertex buffer
            GL.NamedBufferStorage(
                _buffer,
                Vertex.Size * vertices.Length,
                vertices,
                BufferStorageFlags.MapWriteBit);


            // Position (index 0, size 4, offset 0)
            GL.VertexArrayAttribBinding(_vertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(_vertexArray, 0);
            GL.VertexArrayAttribFormat(
                _vertexArray,
                0,
                4,
                VertexAttribType.Float,
                false,
                0);

            // Color (index 1, size 4, offset 4^2 = 16)
            GL.VertexArrayAttribBinding(_vertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(_vertexArray, 1);
            GL.VertexArrayAttribFormat(
                _vertexArray,
                1,
                4,
                VertexAttribType.Float,
                false,
                16);

            // Link everything together
            GL.VertexArrayVertexBuffer(
                _vertexArray,
                0,
                _buffer,
                IntPtr.Zero,
                Vertex.Size);
        }
    }
}

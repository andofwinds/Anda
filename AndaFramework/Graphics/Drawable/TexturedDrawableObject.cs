using SixLabors.ImageSharp;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace AndaFramework.Graphics.Drawable
{
    public class TexturedDrawableObject : AndaDrawable
    {
        private int _texture;

        public TexturedDrawableObject(TexturedVertex[] vertices, int program, string filename)
            : base(program, vertices.Length)
        {
            // Vertex buffer
            GL.NamedBufferStorage(
                _buffer,
                TexturedVertex.Size * vertices.Length,
                vertices,
                BufferStorageFlags.MapWriteBit);

            GL.VertexArrayAttribBinding(_vertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(_vertexArray, 0);
            GL.VertexArrayAttribFormat(
                _vertexArray,
                0,
                4,
                VertexAttribType.Float,
                false,
                0);

            GL.VertexArrayAttribBinding(_vertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(_vertexArray, 1);
            GL.VertexArrayAttribFormat(
                _vertexArray,
                1,
                2,
                VertexAttribType.Float,
                false,
                16);

            GL.VertexArrayVertexBuffer(_vertexArray,
                0,
                _buffer,
                IntPtr.Zero,
                TexturedVertex.Size);

            _texture = InitTextures(filename);
        }

        private int InitTextures(string filename)
        {
            int width, height;
            float[] data = LoadTexture(filename, out width, out height);
            int texture;

            GL.CreateTextures(TextureTarget.Texture2D, 1, out texture);
            GL.TextureStorage2D(
                texture,
                1,
                SizedInternalFormat.Rgba32f,
                width,
                height);

            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TextureSubImage2D(
                texture,
                0,
                0,
                0,
                width,
                height,
                PixelFormat.Rgba,
                PixelType.Float,
                data);

            return texture;
        }

        private float[] LoadTexture(string filename, out int width, out int height)
        {
            float[] r;

            using (var bmp = Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(filename))
            {
                width = bmp.Width;
                height = bmp.Height;

                r = new float[width * height * 4];
                int index = 0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var pixel = bmp[x, y];
                        r[index++] = pixel.R / 255f;
                        r[index++] = pixel.G / 255f;
                        r[index++] = pixel.B / 255f;
                        r[index++] = pixel.A / 255f;
                    }
                }
            }

            return r;
        }

        public override void Bind()
        {
            base.Bind();
            GL.BindTexture(TextureTarget.Texture2D, _texture);
        }

        protected override void Dispose(bool Disposing)
        {
            if (Disposing)
            {
                GL.DeleteTexture(_texture);
            }

            base.Dispose(Disposing);
        }
    }
}

using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

using AndaFramework.Graphics.Shader;

namespace AndaFramework.Graphics.Drawable
{
    public class MipmapDrawableObject : AndaDrawable
    {
        public struct Miplevel
        {
            public int Level;
            public int Width;
            public int Height;
            public float[] Data;
        };

        private int _minMipmapLevel = 0;
        private int _maxMipMapLevel;
        private int _texture;

        public MipmapDrawableObject(TexturedVertex[] vertices,
                                    int program,
                                    string filename,
                                    int maxMipmapLevel)
          : base(program, vertices.Length)
        {
            _maxMipMapLevel = maxMipmapLevel;

            GL.NamedBufferStorage(
                _buffer,
                TexturedVertex.Size * vertices.Length,
                vertices,
                BufferStorageFlags.MapWriteBit
                );

            GL.VertexArrayAttribBinding(_vertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(_vertexArray, 0);
            GL.VertexArrayAttribFormat(
                _vertexArray,
                0,
                4,
                VertexAttribType.Float,
                false,
                0
                );

            GL.VertexArrayAttribBinding(_vertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(_vertexArray, 1);
            GL.VertexArrayAttribFormat(
                _vertexArray,
                1,
                2,
                VertexAttribType.Float,
                false,
                16
                );

            GL.VertexArrayVertexBuffer(
                _vertexArray,
                0,
                _buffer,
                IntPtr.Zero,
                TexturedVertex.Size
                );

            InitTextures(filename);
        }

        public void InitTextures(string filename)
        {
            List<Miplevel> data = LoadTexture(filename);

            GL.CreateTextures(TextureTarget.Texture2D, 1, out _texture);

            GL.BindTexture(TextureTarget.Texture2D, _texture);
            GL.TextureStorage2D(
                _texture,
                _maxMipMapLevel,
                SizedInternalFormat.Rgba32f,
                data.First().Width,
                data.First().Height
                );

            for (int i = 0; i < data.Count; i++)
            {
                Miplevel miplevel = data[i];

                GL.TextureSubImage2D(
                    _texture,
                    i,
                    0,
                    0,
                    miplevel.Width,
                    miplevel.Height,
                    PixelFormat.Rgba,
                    PixelType.Float,
                    miplevel.Data
                    );

                var textureMin = (int)All.LinearMipmapLinear;
                var textureMag = (int)All.Linear;

                GL.TextureParameterI(_texture, TextureParameterName.TextureMinFilter, ref textureMin);
                GL.TextureParameterI(_texture, TextureParameterName.TextureMagFilter, ref textureMag);

            }
        }

        public List<Miplevel> LoadTexture(string filename)
        {
            List<Miplevel> miplevels = new List<Miplevel>();

            using (var image = Image<Rgba32>.Load(filename))
            {
                int xOff = 0;
                int yOff;
                int w = image.Width;
                int h = (image.Height / 3) * 2;
                int index;

                int originHeight = h;

                for (int mip = 0; mip < _maxMipMapLevel; mip++)
                {
                    xOff += mip == 0 || mip == 1 ? 0 : w * 2;
                    yOff = mip == 0 ? 0 : originHeight;

                    Miplevel level;
                    level.Level = mip;
                    level.Width = w;
                    level.Height = h;
                    level.Data = new float[level.Width * level.Height * 4];
                    index = 0;

                    ExtractMipmapLevel(yOff, xOff, level, (Image<Rgba32>)image, index);

                    miplevels.Add(level);

                    if (w == 1 || h == 1)
                    {
                        _maxMipMapLevel = mip;
                        break;
                    }

                    w /= 2;
                    h /= 2;
                }
            }

            return miplevels;
        }

        private static void ExtractMipmapLevel(int yOff,
                                               int xOff,
                                               Miplevel level,
                                               Image<Rgba32> image,
                                               int index)
        {
            int w = xOff + level.Width;
            int h = yOff + level.Height;

            for (int y = yOff; y < h; y++)
            {
                for (int x = xOff; x < w; x++)
                {
                    var pixel = image[x, y];
                    level.Data[index++] = pixel.R / 255;
                    level.Data[index++] = pixel.G / 255;
                    level.Data[index++] = pixel.B / 255;
                    level.Data[index++] = pixel.A / 255;
                }
            }
        }
    }
}

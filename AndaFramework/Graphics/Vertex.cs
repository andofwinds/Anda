using OpenTK.Mathematics;

namespace AndaFramework.Graphics
{
    public struct Vertex
    {
        public const int Size = (4 + 4) * 4;

        private readonly Vector4 _position;
        private readonly Color4 _color;

        public Vertex(Vector4 position, Color4 color)
        {
            _position = position;
            _color = color;
        }

        public Vertex(Vector2 position2d, Color4 color)
        {
            _position = new Vector4(position2d.X, position2d.Y, 0.5f, 1f);
            _color = color;
        }
    }

    public struct TexturedVertex
    {
        public const int Size = (4 + 2) * 4;

        private readonly Vector4 _position;
        private readonly Vector2 _textureCoord;

        public TexturedVertex(Vector4 position, Vector2 textureCoord)
        {
            _position = position;
            _textureCoord = textureCoord;
        }
    }


}

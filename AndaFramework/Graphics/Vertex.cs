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

    public static class VertexFactory
    {
        /// <summary>
        /// Generates vertices for rectangle from 2 points
        /// </summary>
        /// <param name="pos1">Point 1</param>
        /// <param name="size">Size of each side</param>
        /// <returns>Vertices array for this rectangle</returns>
        public static Vertex[] GenerateRect(Vector2 pos1, float size, Color4 color)
        {
            Vector2 pos2 = new Vector2(pos1.X - size, pos1.Y - size);
            Vector2 pos0 = new Vector2(pos1.X, pos2.Y);
            Vector2 pos3 = new Vector2(pos2.X, pos1.Y);

            Vertex[] _vertices =
            {
                new Vertex(pos1, color),
                new Vertex(pos0, color),
                new Vertex(pos3, color),
                new Vertex(pos2, color),
                new Vertex(pos0, color),
                new Vertex(pos3, color),
            };

            return _vertices;
        }

        public static TexturedVertex[] GenerateTexturedRect(Vector2 pos, float size, Vector2 textureSize, float zIndex)
        {
            pos = new Vector2(pos.X - (size / 2), pos.Y - (size / 2));
            float w = textureSize.X;
            float h = textureSize.Y;

            Vector4 pos0 = new Vector4(pos.X, pos.Y, zIndex, 1f);
            Vector4 pos1 = new Vector4(pos.X + size, pos.Y, zIndex, 1f);
            Vector4 pos2 = new Vector4(pos.X, pos.Y + size, zIndex, 1f);
            Vector4 pos3 = new Vector4(pos.X + size, pos.Y + size, zIndex, 1f);

            TexturedVertex[] _vertices =
            {
                new TexturedVertex(pos0, new Vector2(0, 0)),
                new TexturedVertex(pos1, new Vector2(w, 0)),
                new TexturedVertex(pos2, new Vector2(0, h)),
                new TexturedVertex(pos2, new Vector2(0, h)),
                new TexturedVertex(pos1, new Vector2(w, 0)),
                new TexturedVertex(pos3, new Vector2(w, h)),
            };

            return _vertices;
        }

        public static Vertex[] GenerateCube(float sideSize, Color4 color)
        {
            float side = sideSize / 2; // Half side


            return new Vertex[]
            {
                new Vertex(new Vector4(-side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, -side, side, 1.0f), color),
                new Vertex(new Vector4(-side, side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, -side, side, 1.0f), color),
                new Vertex(new Vector4(-side, side, side, 1.0f), color),

                new Vertex(new Vector4(side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(side, side, -side, 1.0f), color),
                new Vertex(new Vector4(side, -side, side, 1.0f), color),
                new Vertex(new Vector4(side, -side, side, 1.0f), color),
                new Vertex(new Vector4(side, side, -side, 1.0f), color),
                new Vertex(new Vector4(side, side, side, 1.0f), color),

                new Vertex(new Vector4(-side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, -side, side, 1.0f), color),
                new Vertex(new Vector4(-side, -side, side, 1.0f), color),
                new Vertex(new Vector4(side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(side, -side, side, 1.0f), color),

                new Vertex(new Vector4(-side, side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, side, side, 1.0f), color),
                new Vertex(new Vector4(side, side, -side, 1.0f), color),
                new Vertex(new Vector4(side, side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, side, side, 1.0f), color),
                new Vertex(new Vector4(side, side, side, 1.0f), color),

                new Vertex(new Vector4(-side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, side, -side, 1.0f), color),
                new Vertex(new Vector4(side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, side, -side, 1.0f), color),
                new Vertex(new Vector4(side, side, -side, 1.0f), color),

                new Vertex(new Vector4(-side, -side, side, 1.0f), color),
                new Vertex(new Vector4(side, -side, side, 1.0f), color),
                new Vertex(new Vector4(-side, side, side, 1.0f), color),
                new Vertex(new Vector4(-side, side, side, 1.0f), color),
                new Vertex(new Vector4(side, -side, side, 1.0f), color),
                new Vertex(new Vector4(side, side, side, 1.0f), color)
            };
        }



        public static TexturedVertex[] GenerateTexturedCube(float sideSize, float textureWidth, float textureHeight)
        {
            float h = textureHeight;
            float w = textureWidth;
            float side = sideSize / 2;

            TexturedVertex[] vertices =
            {
                new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, 0)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(w, 0)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(w, 0)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(w, h)),

                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(0, 0)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, h)),

                new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, 0)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, h)),

                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, 0)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, h)),

                new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, 0)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(0, 0)),

                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, 0)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, 0)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, 0)),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, h)),
            };

            return vertices;
        }
    }
}

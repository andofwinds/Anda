using OpenTK.Mathematics;

using AndaFramework.Graphics.Object;
using AndaFramework.Graphics.Text;

namespace AndaFramework.Graphics
{
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

        public static TexturedVertex[] GenerateCharacter()
        {
            float h = 1;
            float w = AndaText.CharWidth;
            float side = 0.5f;

            TexturedVertex[] vertices =
            {
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, h)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, 0)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, 0)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, h)),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, 0)),
            };

            return vertices;
        }

        public static TexturedVertex[] GenerateTexturedRect(Vector2 pos, float size, Vector2 textureSize, float zIndex)
        {
            //pos = new Vector2(pos.X - (size / 2), pos.Y - (size / 2));
            float w = textureSize.X;
            float h = textureSize.Y;

            Vector4 pos0 = new Vector4(pos.X, pos.Y, zIndex, 1f);
            Vector4 pos1 = new Vector4(pos.X + textureSize.X, pos.Y, zIndex, 1f);
            Vector4 pos2 = new Vector4(pos.X, pos.Y + textureSize.Y, zIndex, 1f);
            Vector4 pos3 = new Vector4(pos.X + textureSize.X, pos.Y + textureSize.Y, zIndex, 1f);

            TexturedVertex[] _vertices =
            {

                new TexturedVertex(pos3, new Vector2(0, 0)),
                new TexturedVertex(pos2, new Vector2(w, 0)),
                new TexturedVertex(pos1, new Vector2(0, h)),

                new TexturedVertex(pos1, new Vector2(0, h)),
                new TexturedVertex(pos2, new Vector2(w, 0)),
                new TexturedVertex(pos0, new Vector2(w, h))
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

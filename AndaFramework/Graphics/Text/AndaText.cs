using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

using AndaFramework.Graphics.Object;
using AndaFramework.Graphics.Drawable;
using AndaFramework.Graphics.Camera;
using AndaFramework.Graphics.Text;

namespace AndaFramework.Graphics.Text
{
    public class AndaText : AndaObject
    {
        public const string Characters = @"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789µ§½!""#¤%&/()=?^*@£€${[]}\~¨'-_.:,;<>|";
        public static float CharWidth;
        public List<AndaCharacter> Text;

        private static Dictionary<char, int> Lookup;
        private Vector4 _color;

        static AndaText()
        {
            Lookup = new Dictionary<char, int>();

            for (int i = 0; i < Characters.Length; i++)
            {
                if (!Lookup.ContainsKey(Characters[i]))
                {
                    Lookup.Add(Characters[i], i);
                }
            }

            CharWidth = 1f / Characters.Length;
        }

        public AndaText(AndaDrawable drawable, Vector4 position,
                Color4 color,
                string value)
            : base(drawable, position, Vector4.Zero, Vector4.Zero, 0)
        {
            _color = new Vector4(color.R, color.G, color.B, color.A);
            Text = new List<AndaCharacter>(value.Length);
            _scale = new Vector3(0.02f);
            SetText(value);
        }

        public void SetText(string value)
        {
            Text.Clear();

            for (int i = 0; i < value.Length; i++)
            {
                int off;
                if (Lookup.TryGetValue(value[i], out off))
                {
                    AndaCharacter c = new AndaCharacter(_drawable,
                            new Vector4(_position.X + (i * 0.015f),
                                _position.Y,
                                _position.Z,
                                _position.W),
                            (off * CharWidth));

                    c.SetScale(_scale);
                    Text.Add(c);
                }
            }
        }

        public override void Render(AndaCamera camera)
        {
            _drawable.Bind();

            GL.VertexAttrib4(3, _color);

            for (int i = 0; i < Text.Count; i++)
            {
                AndaCharacter c = Text[i];
                c.Render(camera);
            }
        }
    }
}

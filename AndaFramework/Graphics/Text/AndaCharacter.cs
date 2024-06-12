using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

using AndaFramework.Graphics.Object;
using AndaFramework.Graphics.Drawable;
using AndaFramework.Graphics.Camera;

namespace AndaFramework.Graphics.Text
{
    public class AndaCharacter : AndaObject
    {
        private float _offset;

        public AndaCharacter(AndaDrawable drawable, Vector4 position, float charOffset)
          : base(drawable, position, Vector4.Zero, Vector4.Zero, 0)
        {
            _offset = charOffset;
            _scale = new Vector3(0.2f);
        }

        public void SetChar(float charOffset)
        {
            _offset = charOffset;
        }

        public override void Render(AndaCamera camera)
        {

            GL.VertexAttrib2(2, new Vector2(_offset, 0));

            Matrix4 t = Matrix4.CreateTranslation(
                    _position.X,
                    _position.Y,
                    _position.Z
                    );

            Matrix4 s = Matrix4.CreateScale(_scale);

            _modelView = s * t * camera.Look;

            GL.UniformMatrix4(21, false, ref _modelView);
            _drawable.Render();
        }
    }
}

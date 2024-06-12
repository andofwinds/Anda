using OpenTK.Mathematics;

using AndaFramework.Graphics.Drawable;

namespace AndaFramework.Graphics.Object
{
    public class TestObject : AndaObject
    {
        public TestObject(AndaDrawable drawable,
            Vector4 position,
            Vector4 direction,
            Vector4 rotation,
            float velocity)
          : base(
              drawable,
              position,
              direction,
              rotation,
              velocity
              )
        {

        }

        /*public override void Update(float time, float deltaTime)
        {
            _direction.X -= 1;
            _velocity += 0.5f * deltaTime;

            base.Update(time, deltaTime);
        }*/

    }
}

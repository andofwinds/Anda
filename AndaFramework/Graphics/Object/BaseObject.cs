using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

using AndaFramework.Graphics.Drawable;
using AndaFramework.Graphics.Camera;

namespace AndaFramework.Graphics.Object
{
    public abstract class AndaObject
    {
        private static int ObjectCounter;

        public AndaDrawable Drawable => _drawable;
        public Vector4 Translation => _position;
        public Vector4 Direction => _direction;
        public Vector3 Scale => _scale;
        public Vector4 Rotation => _rotation;

        public int ObjectId;

        protected Vector4 _position;
        protected Vector4 _direction;
        protected Vector3 _scale;
        protected Vector4 _rotation;

        protected float _velocity;

        protected Matrix4 _modelView;

        protected AndaDrawable _drawable;

        public AndaObject(AndaDrawable drawable,
                          Vector4 position,
                          Vector4 direction,
                          Vector4 rotation,
                          float velocity)
        {
            _drawable = drawable;
            _position = position;
            _direction = direction;
            _rotation = rotation;
            _velocity = velocity;
            _scale = new Vector3(1);
            ObjectId = ObjectCounter++;
        }

        public void Bind()
        {
            _drawable.Bind();
        }

        public virtual void Render(AndaCamera camera)
        {
            Bind();

            Matrix4 t = Matrix4.CreateTranslation(_position.X, _position.Y, _position.Z);
            Matrix4 rx = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(_rotation.X));
            Matrix4 ry = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(_rotation.Y));
            Matrix4 rz = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(_rotation.Z));
            Matrix4 s = Matrix4.CreateScale(_scale);

            _modelView = rx * ry * rz * s * t * camera.Look;
            GL.UniformMatrix4(21, false, ref _modelView);

            _drawable.Render();
        }

        public void SetPosition(Vector4 position)
        {
            _position = position;
        }

        public void SetRotation(Vector4 rotation)
        {
            _rotation = rotation;
        }

        public void SetScale(Vector3 scale)
        {
            _scale = scale;
        }

        public virtual void Update(float time, float deltaTime)
        {
            _position += _direction * (_velocity * deltaTime);
        }
    }
}

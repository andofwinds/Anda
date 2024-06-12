using OpenTK.Mathematics;

namespace AndaFramework.Graphics.Camera
{
    public class StaticCamera : AndaCamera
    {
        public Matrix4 Look { get; }

        public StaticCamera()
        {
            Vector3 position;
            position.X = 0;
            position.Y = 0;
            position.Z = 0;

            Look = Matrix4.LookAt(position, -Vector3.UnitZ, Vector3.UnitY);
        }

        public StaticCamera(Vector3 position, Vector3 target)
        {
            Look = Matrix4.LookAt(position, target, Vector3.UnitY);
        }

        public void Update(float time, float deltaTime)
        { }
    }
}

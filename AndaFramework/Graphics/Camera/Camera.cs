using OpenTK.Mathematics;

namespace AndaFramework.Graphics.Camera
{
    public interface AndaCamera
    {
        Matrix4 Look { get; }

        void Update(float time, float deltaTime);
    }
}

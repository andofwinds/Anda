namespace AndaFramework.Graphics.Samples
{
    public static class SampleRect
    {
        public static float[] vertices =
        {
            0.5f, 0.5f, 0,
            0.5f, -0.5f, 0,
            -0.5f, -0.5f, 0,
            -0.5f, 0.5f, 0
        };
        
        public static uint[] indices =
        {
            0, 1, 3,
            1, 2, 3
        };
    }

    public class SampleTriangle
    {
        public static float[] vertices =
        {
            -0.5f, -0.5f, 0,
            0.5f, -0.5f, 0,
            0f, 0f, 0
        };
        
        public static uint[] indices =
        {
            0, 1, 2
        };
    }
}
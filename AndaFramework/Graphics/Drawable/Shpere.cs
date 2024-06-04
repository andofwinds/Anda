using OpenTK.Mathematics;

namespace AndaFramework.Graphics.Drawable
{
    public class DrawableSphere
    {
        private struct Face
        {
            public Vector3 v1;
            public Vector3 v2;
            public Vector3 v3;

            public Face(Vector3 vec1, Vector3 vec2, Vector3 vec3)
            {
                v1 = vec1;
                v2 = vec2;
                v3 = vec3;
            }
        }

        private List<Vector3> _points;
        private int _index;
        private Dictionary<long, int> _middlePointIndexCache;

        public TexturedVertex[] Create(int recursionLevel)
        {
            _middlePointIndexCache = new Dictionary<long, int>();
            _points = new List<Vector3>();
            List<Face> faces = new List<Face>();
            _index = 0;

            float t = (float)((1 + Math.Sqrt(5)) / 2);
            float s = 1;

            AddPoint(new Vector3(-s, t, 0));
            AddPoint(new Vector3(s, t, 0));
            AddPoint(new Vector3(-s, -t, 0));
            AddPoint(new Vector3(s, -t, 0));

            AddPoint(new Vector3(0, -s, t));
            AddPoint(new Vector3(0, s, t));
            AddPoint(new Vector3(0, -s, -t));
            AddPoint(new Vector3(0, s, -t));

            AddPoint(new Vector3(t, 0, -s));
            AddPoint(new Vector3(t, 0, s));
            AddPoint(new Vector3(-t, 0, -s));
            AddPoint(new Vector3(-t, 0, s));

            // 5 faces around point 0
            faces.Add(new Face(_points[0], _points[11], _points[5]));
            faces.Add(new Face(_points[0], _points[5], _points[1]));
            faces.Add(new Face(_points[0], _points[1], _points[7]));
            faces.Add(new Face(_points[0], _points[7], _points[10]));
            faces.Add(new Face(_points[0], _points[10], _points[11]));

            // 5 adjacent faces 
            faces.Add(new Face(_points[1], _points[5], _points[9]));
            faces.Add(new Face(_points[5], _points[11], _points[4]));
            faces.Add(new Face(_points[11], _points[10], _points[2]));
            faces.Add(new Face(_points[10], _points[7], _points[6]));
            faces.Add(new Face(_points[7], _points[1], _points[8]));

            // 5 faces around point 3
            faces.Add(new Face(_points[3], _points[9], _points[4]));
            faces.Add(new Face(_points[3], _points[4], _points[2]));
            faces.Add(new Face(_points[3], _points[2], _points[6]));
            faces.Add(new Face(_points[3], _points[6], _points[8]));
            faces.Add(new Face(_points[3], _points[8], _points[9]));

            // 5 adjacent faces 
            faces.Add(new Face(_points[4], _points[9], _points[5]));
            faces.Add(new Face(_points[2], _points[4], _points[11]));
            faces.Add(new Face(_points[6], _points[2], _points[10]));
            faces.Add(new Face(_points[8], _points[6], _points[7]));
            faces.Add(new Face(_points[9], _points[8], _points[1]));

            for (int i = 0; i < recursionLevel; i++)
            {
                List<Face> ifaces = new List<Face>();
                foreach (Face face in faces)
                {
                    int a = GetMiddlePoint(face.v1, face.v2);
                    int b = GetMiddlePoint(face.v2, face.v3);
                    int c = GetMiddlePoint(face.v3, face.v1);

                    ifaces.Add(new Face(face.v1, _points[a], _points[c]));
                    ifaces.Add(new Face(face.v2, _points[b], _points[a]));
                    ifaces.Add(new Face(face.v3, _points[c], _points[b]));
                    ifaces.Add(new Face(_points[a], _points[b], _points[c]));
                }

                faces = ifaces;
            }

            List<TexturedVertex> triangles = new List<TexturedVertex>();

            foreach (Face face in faces)
            {
                Vector2 uv1 = GetSphereCoord(face.v1);
                Vector2 uv2 = GetSphereCoord(face.v2);
                Vector2 uv3 = GetSphereCoord(face.v3);

                triangles.Add(new TexturedVertex(new Vector4(face.v1, 2f), uv1));
                triangles.Add(new TexturedVertex(new Vector4(face.v2, 2f), uv2));
                triangles.Add(new TexturedVertex(new Vector4(face.v3, 2f), uv3));
            }

            return triangles.ToArray();
        }

        public static Vector2 GetSphereCoord(Vector3 s)
        {
            float len = s.Length;

            Vector2 uv;
            uv.Y = (float)(Math.Acos(s.Y / len) / Math.PI);
            uv.X = -(float)((Math.Atan2(s.Z, s.X) / Math.PI + 1) * 0.5f);

            return uv;
        }

        private int GetMiddlePoint(Vector3 p1, Vector3 p2)
        {
            long i1 = _points.IndexOf(p1);
            long i2 = _points.IndexOf(p2);

            // FirstIsSmaller but i'm too lazy to type it 8)
            bool fis = i1 < i2;

            long smallerIndex = fis ? i1 : i2;
            long greaterIndex = fis ? i2 : i1;

            long key = (smallerIndex << 32) + greaterIndex;

            int ret;
            if (_middlePointIndexCache.TryGetValue(key, out ret))
            {
                return ret;
            }

            Vector3 middle = new Vector3(
                    (p1.X + p2.X) / 2,
                    (p1.Y + p2.Y) / 2,
                    (p1.Z + p2.Z) / 2
                    );

            int i = AddPoint(middle);

            _middlePointIndexCache.Add(key, i);

            return i;
        }

        private int AddPoint(Vector3 p)
        {
            _points.Add(p.Normalized());
            return _index++;
        }
    }
}

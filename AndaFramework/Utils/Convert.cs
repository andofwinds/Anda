using OpenTK.Mathematics;

namespace AndaFramework.Utils
{
    public static class Convert
    {
        
        /// <summary>
        /// Converts HEX code to float range
        /// </summary>
        /// <param name="HexCode">Hex code</param>
        /// <returns>List of 4 RGBA float ranges</returns>
        public static float[] RgbaToRange(string HexCode)
        {
            float[] _floatValues = new float[4];
                
            // Remove `#` if exists
            if (HexCode[0].Equals('#'))
            {
                HexCode = HexCode.Remove(0, 1);
            }
            for (int i = 0; i < HexCode.Length; i += 2)
            {
                float val = System.Convert.ToInt32(
                    HexCode.Substring(i, 2),
                    16);
                _floatValues[i / 2] = val / 255;
            }

            return _floatValues;
        }
        
        /// <summary>
        /// Converts HEX code to float range in Vector4
        /// </summary>
        /// <param name="HexCode">Hex code</param>
        /// <returns>Vector with 4 RGBA float ranges</returns>
        public static Vector4 RgbaToVectorRange(string HexCode)
        {
            float[] returnedData = RgbaToRange(HexCode);

            return new Vector4( returnedData[0],
                                returnedData[1],
                                returnedData[2],
                                returnedData[3]);
        }

        /// <summary>
        /// Converts HEX code to float range in Color4
        /// </summary>
        /// <param name="HexCode">Hex code</param>
        /// <returns>Color4</returns>
        public static Color4 RgbaToColor4(string HexCode)
        {
            float[] returnedData = RgbaToRange(HexCode);

            return new Color4(
                returnedData[0],
                returnedData[1],
                returnedData[2],
                returnedData[3]);
        }
    }
}
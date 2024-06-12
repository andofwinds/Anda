using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp;
using SixLabors.Fonts;

using AndaFramework.Logging;

namespace AndaFramework.Graphics.Text
{
    public class AndaFont
    {
        public const string Characters =
            @"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789µ§½!""#¤%&/()=?^*@£€${[]}\~¨'-_.:,;<>|";

        private const string ttfPath = "Fonts/Fredoka.ttf";

        public static void GenerateFont()
        {
            int fontSize = 50;

            Logger.Log("FONT", "Processing new font....");
            string fontName = ttfPath.Split('/').Last();
            string resultPath = $"Fonts/{fontName}.png";
            FontCollection collection = new FontCollection();
            FontFamily fontFamily = collection.Add(ttfPath);
            Font font = fontFamily.CreateFont(fontSize, FontStyle.Italic);

            RichTextOptions options = new RichTextOptions(font)
            {
                Origin = new PointF(0, 0),
                TabWidth = 0,
                WrappingLength = 0,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            int w = Characters.Length * fontSize;
            int h = fontSize;

            Logger.Log("FONT", $"Generating font image {w}x{h}.");

            using (var image = new Image<Rgba32>(w, h))
            {
                int spacing;
                int offset = 0;
                for (int i = 0; i < Characters.Length; i++)
                {
                    spacing = (int)TextMeasurer.MeasureSize(Characters[i].ToString(), options).Width;

                    //offset += spacing / 2;
                    image.Mutate(x => x.DrawText(
                                Characters[i].ToString(),
                                font,
                                new Color(Rgba32.ParseHex("#FF0000")),
                                new PointF(0 + offset, 0)
                                ));

                    offset += (int)TextMeasurer.MeasureSize(Characters[i].ToString(), options).Width + spacing;
                    //offset += spacing;
                    //offset += fontSize;
                }

                image.SaveAsPng(resultPath);
            }
        }
    }
}

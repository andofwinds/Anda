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
        private const string Characters =
            @"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789µ§½!""#¤%&/()=?^*@£€${[]}\~¨'-_.:,;<>|";

        private const string ttfPath = "/home/andofwinds/Desktop/Anda/Anda/Fonts/Fredoka.ttf";

        public static void GenerateFont()
        {
            Logger.Log("FONT", "Processing new font....");
            string destPath = "/home/andofwinds/Desktop/Anda/Anda/OUTPUT.png";
            string fontName = ttfPath.Split('/').Last();
            string dirPath = $"Fonts/collection_{fontName}";
            FontCollection collection = new FontCollection();
            FontFamily fontFamily = collection.Add(ttfPath);
            Font font = fontFamily.CreateFont(50, FontStyle.Italic);

            RichTextOptions options = new RichTextOptions(font)
            {
                Origin = new PointF(0, 0),
                TabWidth = 0,
                WrappingLength = 0,
                HorizontalAlignment = HorizontalAlignment.Right
            };



            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            foreach (char c in Characters)
            {
                FontRectangle rect = TextMeasurer.MeasureSize(c.ToString(), options);
                Image image = new Image<Rgba32>((int)rect.Width, 50);

                image.Mutate(x => x.DrawText(
                            c.ToString(),
                            font,
                            new Color(Rgba32.ParseHex("#000000")),
                            new PointF(image.Width - rect.Width,
                                image.Height - rect.Height)
                            ));

                image.SaveAsPng($"{dirPath}/{c}.png");
            }


            Logger.Log("FONT", $"New font created from source `{fontName}`!");
        }
    }
}

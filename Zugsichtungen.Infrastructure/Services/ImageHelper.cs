using SkiaSharp;

namespace Zugsichtungen.Infrastructure.Services
{
    public static class ImageHelper
    {
        public static byte[] CreateThumbnail(byte[] originalImage, int maxWidth = 150)
        {
            if (originalImage.Length == 0)
            { 
                throw new ArgumentException("Bilddaten sind leer.", nameof(originalImage));
            }

            using var src = SKImage.FromEncodedData(originalImage);

            // Zielgröße (Seitenverhältnis beibehalten)
            var scale = Math.Min((float)maxWidth / src.Width, (float)maxWidth / src.Height);
            var w = Math.Max(1, (int)Math.Round(src.Width * scale));
            var h = Math.Max(1, (int)Math.Round(src.Height * scale));

            var info = new SKImageInfo(w, h, SKColorType.Rgba8888, SKAlphaType.Premul);
            using var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            // Für Downsizing: Linear + Mipmaps → glattere Ergebnisse
            var sampling = new SKSamplingOptions(SKFilterMode.Linear, SKMipmapMode.Linear);

            // Zeichnen skaliert ins Ziel
            canvas.DrawImage(src, SKRect.Create(w, h), sampling);
            canvas.Flush();

            using var resized = surface.Snapshot();
            using var data = resized.Encode(SKEncodedImageFormat.Jpeg, 80);

            return data.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CameraControl;

namespace FakeCameraControl
{
    public class FakeCamera : Camera
    {
        public FakeCamera(string model, string port)
        {
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
            this.Port = port ?? throw new ArgumentNullException(nameof(port));
        }

        public override string Model { get; }
        public override string Port { get; }

        public override Task<string> CaptureImage(string path, string filename)
        {
            const int width = 255;
            const int height = 255;

            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"{path} not found");

            using var image = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(image);
            using var pen = new Pen(Brushes.Black);

            var triangle = RandomPoints(width, height).Take(3).ToList();

            graphics.Clear(Color.White);
            graphics.DrawLine(pen, triangle[0], triangle[1]);
            graphics.DrawLine(pen, triangle[0], triangle[2]);
            graphics.DrawLine(pen, triangle[1], triangle[2]);

            var fullPath = $"{path}/{filename}.png";
            image.Save(fullPath, ImageFormat.Png);

            return Task.FromResult(fullPath);
        }

        private IEnumerable<Point> RandomPoints(int maxX, int maxY)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            while (true)
            {
                yield return new Point(random.Next(0, maxX), random.Next(0, maxY));
            }
        }
    }
}

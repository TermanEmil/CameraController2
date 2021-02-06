using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace FakeCameraControl.Commands.DrawRandomFigure
{
    public class DrawRandomFigureCommandHandler : IRequestHandler<DrawRandomFigureCommand, Bitmap>
    {
        private const int PenWidth = 2;

        public Task<Bitmap> Handle(DrawRandomFigureCommand request, CancellationToken cancellationToken)
        {
            Bitmap image = null;

            try
            {
                image = new Bitmap(request.Width, request.Height);
                using var graphics = Graphics.FromImage(image);
                using var pen = new Pen(Brushes.Black, PenWidth);

                using var framePen = new Pen(Brushes.Aquamarine, 4);
                var frame = new Rectangle(0, 0, request.Width, request.Height);

                var points = this
                    .RandomPoints(request.Width, request.Height)
                    .Take(request.Points)
                    .ToList();

                graphics.Clear(Color.White);
                graphics.DrawRectangle(framePen, frame);

                graphics.DrawLine(pen, points.First(), points.Last());
                for (var i = 0; i < points.Count - 1; i++)
                    graphics.DrawLine(pen, points[i], points[i + 1]);
            }
            catch
            {
                image?.Dispose();
                throw;
            }

            return Task.FromResult(image);
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

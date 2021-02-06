using System.Drawing;
using MediatR;

namespace FakeCameraControl.Commands.DrawRandomFigure
{
    public class DrawRandomFigureCommand : IRequest<Bitmap>
    {
        public DrawRandomFigureCommand(int width, int height, int points)
        {
            this.Width = width;
            this.Height = height;
            this.Points = points;
        }

        public int Width { get; }
        public int Height { get; }
        public int Points { get; }
    }
}

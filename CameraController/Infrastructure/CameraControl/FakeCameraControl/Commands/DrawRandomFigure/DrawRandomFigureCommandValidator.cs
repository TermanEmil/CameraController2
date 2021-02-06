using FluentValidation;

namespace FakeCameraControl.Commands.DrawRandomFigure
{
    public class DrawRandomFigureCommandValidator : AbstractValidator<DrawRandomFigureCommand>
    {
        public DrawRandomFigureCommandValidator()
        {
            this.RuleFor(x => x.Width).GreaterThan(0);
            this.RuleFor(x => x.Height).GreaterThan(0);
            this.RuleFor(x => x.Points).GreaterThan(0);
        }
    }
}

using FluentValidation;

namespace CameraControl.Commands.CaptureImage
{
    public class CaptureImageCommandValidator : AbstractValidator<CaptureImageCommand>
    {
        public CaptureImageCommandValidator()
        {
            this.RuleFor(x => x.Port).NotEmpty();
            this.RuleFor(x => x.Path).NotEmpty();
            this.RuleFor(x => x.Filename).NotEmpty();
        }
    }
}

using FluentValidation;

namespace GphotoCameraControl.Commands.CaptureImage
{
    public class CaptureImageCommandValidator : AbstractValidator<CaptureImageCommand>
    {
        public CaptureImageCommandValidator()
        {
            this.RuleFor(x => x.Filename).NotEmpty();
            this.RuleFor(x => x.Port).NotEmpty();
        }
    }
}

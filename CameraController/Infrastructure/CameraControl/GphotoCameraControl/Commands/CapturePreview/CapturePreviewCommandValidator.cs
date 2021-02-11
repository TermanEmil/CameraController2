using FluentValidation;

namespace GphotoCameraControl.Commands.CapturePreview
{
    public class CapturePreviewCommandValidator : AbstractValidator<CapturePreviewCommand>
    {
        public CapturePreviewCommandValidator()
        {
            RuleFor(x => x.Port).NotEmpty();
        }
    }
}

using FluentValidation;

namespace CameraControl.Commands.CaptureImageAndDownload
{
    public class CaptureImageAndDownloadCommandValidator : AbstractValidator<CaptureImageAndDownloadCommand>
    {
        public CaptureImageAndDownloadCommandValidator()
        {
            RuleFor(x => x.Port).NotEmpty();
        }
    }
}

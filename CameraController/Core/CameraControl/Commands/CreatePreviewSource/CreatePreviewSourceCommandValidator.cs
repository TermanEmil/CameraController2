using FluentValidation;

namespace CameraControl.Commands.CreatePreviewSource
{
    public class CreatePreviewSourceCommandValidator : AbstractValidator<CreatePreviewSourceCommand>
    {
        public CreatePreviewSourceCommandValidator()
        {
            this.RuleFor(x => x.Port);
        }
    }
}

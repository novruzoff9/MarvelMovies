using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation_Rules
{
    public class AnimationValidator : AbstractValidator<Animation>
    {
        public AnimationValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Film Adını yazın");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Şəkil daxil edin");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Film Adı 4 simvoldan az olammaz");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Film Adı 20 simvoldan cox olammaz");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Haqqında yazın");
            RuleFor(x => x.ReleaseDate).NotEmpty().WithMessage("Tarixi yazın");
        }
    }
}

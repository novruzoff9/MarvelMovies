using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation_Rules
{
    public class AdminMessageValidator : AbstractValidator<AdminMessages>
    {
        public AdminMessageValidator()
        {
            RuleFor(x => x.UserMail).NotEmpty().WithMessage("Mail adresi boş buraxılmamalıdır");
            RuleFor(x => x.Header).NotEmpty().WithMessage("Başlıq boş buraxılmamalıdır");
            RuleFor(x => x.Header).MaximumLength(50).WithMessage("Başlıq 50 simvoldan çox olmamalıdır");
        }
    }
}

using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation_Rules
{
    public class MessageValidator : AbstractValidator<UserMessages>
    {
        public MessageValidator()
        {
            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Göndəriləcək ünvan boş buraxılmamalıdır");
            RuleFor(x => x.MessageHeader).NotEmpty().WithMessage("Başlıq boş buraxılmamalıdır");
            RuleFor(x => x.MessageHeader).MaximumLength(50).WithMessage("Başlıq 50 simvoldan çox olmamalıdır");
        }
    }
}

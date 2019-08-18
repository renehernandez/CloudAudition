using FluentValidation;
using CloudAuditionApi.Models;

namespace CloudAuditionApi.Validators 
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator() {
            RuleFor(m => m.Id).GreaterThan(0);
            RuleFor(m => m.Content).NotNull().NotEmpty();
        }
    }
}
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace CloudAuditionApi.Models
{
    public class Message
    {
        public long Id { get; set; }

        public string Content { get; set; }
    }

    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator() {
            RuleFor(m => m.Id).GreaterThan(0);
            RuleFor(m => m.Content).NotNull().NotEmpty();
        }
    }
}
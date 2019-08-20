using CloudAuditionApi.Palindrome;
using FluentValidation;

namespace CloudAuditionApi.Models
{
    public class Message
    {
        public long Id { get; set; }

        public string Content { get; set; }

        public bool IsPalindrome { 
            get { return PalindromeEvaluatorFactory.GetEvaluator().IsPalindrome(Content); }
        }
    }

    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator() {
            RuleFor(m => m.Content).NotNull().NotEmpty();
        }
    }
}
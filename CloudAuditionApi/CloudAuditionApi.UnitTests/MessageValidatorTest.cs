using NUnit.Framework;
using CloudAuditionApi.Models;

namespace CloudAuditionApi.UnitTests
{
    public class ContentPropertyTest
    {
        MessageValidator validator;
        
        [SetUp]
        public void Setup()
        {
            validator = new MessageValidator();
        }

        [Test]
        public void FailsIfContentIsNull()
        {
            var message = new Message { };

            var result = validator.Validate(message);
            
            Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Count, Is.EqualTo(2));
            });
        }

        [Test]
        public void FailsIfContentIsEmpty()
        {
            var message = new Message { Content = "" };

            var result = validator.Validate(message);
            
            Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public void PassWithNonEmptyContent() 
        {
            var message = new Message { Content = "Hello World" };

            var result = validator.Validate(message);

            Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.True);
                Assert.That(result.Errors.Count, Is.EqualTo(0));
            });
        }
    }
}
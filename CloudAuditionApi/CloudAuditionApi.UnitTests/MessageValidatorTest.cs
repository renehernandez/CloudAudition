using NUnit.Framework;
using CloudAuditionApi.Models;

namespace CloudAuditionApi.UnitTests
{
    public class IdPropertyTest
    {
        MessageValidator validator;
        
        [SetUp]
        public void Setup()
        {
            validator = new MessageValidator();
        }

        [Test]
        public void FailsIfIdIsNull()
        {
            var message = new Message { Id = 0, Content = "Hello World" };

            var result = validator.Validate(message);
            
            Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Count, Is.EqualTo(1));
            });
        }

        public void PassWithIdGreaterThanZero() 
        {
            var message = new Message { Id = 3, Content = "Hello World" };

            var result = validator.Validate(message);

            Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.True);
                Assert.That(result.Errors.Count, Is.EqualTo(0));
            });
        }
    }

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
            var message = new Message { Id = 5 };

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
            var message = new Message { Id = 5, Content = "" };

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
            var message = new Message { Id = 3, Content = "Hello World" };

            var result = validator.Validate(message);

            Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.True);
                Assert.That(result.Errors.Count, Is.EqualTo(0));
            });
        }
    }
}
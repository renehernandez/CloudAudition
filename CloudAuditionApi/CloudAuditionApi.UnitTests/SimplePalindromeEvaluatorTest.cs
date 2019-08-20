using CloudAuditionApi.Palindrome;
using NUnit.Framework;

namespace CloudAuditionApi.UnitTests
{
    public class SimplePalindromeEvaluatorTest
    {
        SimplePalindromeEvaluator evaluator;

        [SetUp]
        public void Setup()
        {
            evaluator = new SimplePalindromeEvaluator();
        }

        [Test]
        public void ReturnsFalseForNonPalindrome() 
        {
            Assert.That(evaluator.IsPalindrome("Not a palindrome"), Is.False);
        }

        [Test]
        public void ReturnsTrueForSingleLetterPalindrome() 
        {
            Assert.That(evaluator.IsPalindrome("a"), Is.True);
        }

        [Test]
        public void ReturnsTrueForAnOddLengthPalindrome() 
        {
            Assert.That(evaluator.IsPalindrome("No lemon,nomel oN"), Is.True);
        }

        [Test]
        public void ReturnsTrueForAnEvenLengthPalindrome() 
        {
            Assert.That(evaluator.IsPalindrome("redder"), Is.True);
        }
    }
}

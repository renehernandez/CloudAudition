namespace CloudAuditionApi.Palindrome
{
    public static class PalindromeEvaluatorFactory
    {
        public static IPalindromeEvaluator GetEvaluator()
        {
            return new SimplePalindromeEvaluator();
        }
    }
}
namespace CloudAuditionApi.Palindrome
{
    public class SimplePalindromeEvaluator : IPalindromeEvaluator
    {
        public bool IsPalindrome(string text)
        {
            for (int i = 0, j = text.Length - 1; i < j; i++, j--) 
            {   
                if (text[i] != text[j])
                    return false;
            }

            return true;
        }
    }
}
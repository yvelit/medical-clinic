namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string @string)
        {
            return string.IsNullOrEmpty(@string) || string.IsNullOrWhiteSpace(@string);
        }
    }
}

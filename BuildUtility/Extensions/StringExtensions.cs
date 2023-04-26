namespace BuildUtility.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveLastChar(this string source)
        {
            return source.Substring(0, source.Length - 1);
        }

        public static string RemoveTab(this string source)
        {
            return source.Replace("\t", "");
        }

        public static string RemoveSpace(this string source)
        {
            return source.Replace(" ", "");
        }
    }
}

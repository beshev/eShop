namespace EShop.Web.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string TrimIfLongerThan(this string content, int length)
            => content.Length > length ? content[length..] : content;
    }
}

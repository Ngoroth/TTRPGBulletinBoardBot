namespace TTRPGBulletinBoardBot.Core.Extentions
{
    public static class StringExtentions
    {
        public static string EscapeMarkdownV2(this string text)
        {
            return text
                .Replace("_", @"\_")
                .Replace("*", @"\*")
                .Replace("[", @"\[")
                .Replace("]", @"\]")
                .Replace("(", @"\(")
                .Replace(")", @"\)")
                .Replace("~", @"\~")
                .Replace("`", @"\`")
                .Replace(">", @"\>")
                .Replace("#", @"\#")
                .Replace("+", @"\+")
                .Replace("-", @"\-")
                .Replace("=", @"\=")
                .Replace("|", @"\|")
                .Replace("{", @"\{")
                .Replace("}", @"\}")
                .Replace(".", @"\.")
                .Replace("!", @"\!");
        }
    }
}
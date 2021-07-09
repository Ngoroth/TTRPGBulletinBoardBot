using System.Text.RegularExpressions;

namespace TTRPGBulletinBoardBot.Core.Extensions
{
    public static class MarkdownV2StringExtensions
    {
        public static string EscapeMarkdownV2(this string text)
        {
            var escapeRegex = new Regex(@"[_\*\[\]\(\)~`>#\+-=\|\{}\.!]");
            return escapeRegex.Replace(text, match => $@"\{match.Value}");
        }
        
        public static string ConvertToTag(this string text)
        {
            var whitespacesRegex = new Regex(@"\s+");
            var punctuationRegex = new Regex(@"[\p{P}\p{S}]");
            var underscoreTailRegex = new Regex(@"_+$");

            //Пробелы превращаем в _
            //Приводим к верхнему регистру
            //Амперсанд превращаем в n
            //Знаки пунктуации превращаем в _
            //Отрезаем лишний хвост из __
            return "#" + 
                   underscoreTailRegex.Replace(
                punctuationRegex.Replace(
                    whitespacesRegex.Replace(text, "_")
                        .ToUpper()
                        .Replace("&", "n"), "_"), "");
        }

        public static string MakeBold(this string text)
        {
            return $"*{text}*";
        }
    }
}
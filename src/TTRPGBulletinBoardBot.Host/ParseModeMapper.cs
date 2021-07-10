using Telegram.Bot.Types.Enums;
using TTRPGBulletinBoardBot.Core;

namespace TTRPGBulletinBoardBot.Host
{
    public static class ParseModeMapper
    {
        public static ParseMode ToParseMode(this TextFormat textFormat)
        {
            return textFormat switch
            {
                TextFormat.MarkdownV2 => ParseMode.MarkdownV2,
                _ => ParseMode.Default
            };
        }
    }
}
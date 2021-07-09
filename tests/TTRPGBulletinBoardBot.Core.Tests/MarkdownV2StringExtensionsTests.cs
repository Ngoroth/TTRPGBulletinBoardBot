using TTRPGBulletinBoardBot.Core.Extensions;
using Xunit;

namespace TTRPGBulletinBoardBot.Core.Tests
{
    public class MarkdownV2StringExtensionsTests
    {
        [Theory]
        [InlineData("D&D", "#DnD")]
        [InlineData("test test", "#TEST_TEST")]
        [InlineData("test", "#TEST")]
        [InlineData("a_", "#A")]
        [InlineData("double  whitespace", "#DOUBLE_WHITESPACE")]
        [InlineData("a!a\"a#a$a%a'a(a)a*a+a,a\\a-a.a/a:a;a<a=a>a?a@a[a]a^a_a` { | }",
            "#A_A_A_A_A_A_A_A_A_A_A_A_A_A_A_A_A_A_A_A_A_A_A_A_A_A_A")]
        public void ConvertToTag(string text, string result)
        {
            Assert.Equal(result, text.ConvertToTag());
        }

        [Theory]
        [InlineData("_*[]()~`>#+-=|{}.!", @"\_\*\[\]\(\)\~\`\>\#\+\-\=\|\{\}\.\!")]
        public void EscapeMarkdownV2(string text, string result)
        {
            Assert.Equal(result, text.EscapeMarkdownV2());
        }
    }
}
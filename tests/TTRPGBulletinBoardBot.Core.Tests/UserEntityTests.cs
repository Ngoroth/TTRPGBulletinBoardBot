using System;
using TTRPGBulletinBoardBot.Core.Entities;
using Xunit;

namespace TTRPGBulletinBoardBot.Core.Tests
{
    public class UserEntityTests
    {
        [Fact]
        public void Equality()
        {
            var first = new UserEntity(1, Stage.Publication);
            var second = new UserEntity(1, Stage.Start);
            Assert.Equal(first, second);

            second = null;
            Assert.NotEqual(first, second);
        }
    }
}
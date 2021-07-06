using System.Collections.Generic;

namespace TTRPGBulletinBoardBot.Core.Entities
{
    public sealed record UserEntity(long Id, Stage Stage)
    {
        public Dictionary<Stage, string> Answers { get; set; } = new()
        {
            {Stage.AskGameName, ""},
            {Stage.AskDescription, ""},
            {Stage.AskSystem, ""},
            {Stage.AskExpectations, ""},
            {Stage.AskDateTime, ""}
        };
        public bool Equals(UserEntity? other)
        {
            if (other is null)
                return false;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
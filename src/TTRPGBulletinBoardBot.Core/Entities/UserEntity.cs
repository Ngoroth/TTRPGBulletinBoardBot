using System.Collections.Generic;

namespace TTRPGBulletinBoardBot.Core.Entities
{
    public sealed record UserEntity(long Id, Stage Stage)
    {
        public Dictionary<Stage, string> Answers { get; } = new()
        {
            {Stage.AskSystem, ""},
            {Stage.AskMaster, ""},
            {Stage.AskExpectations, ""},
            {Stage.AskDateTime, ""},
            {Stage.AskLocation, ""},
            {Stage.AskGameName, ""},
            {Stage.AskDescription, ""}
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
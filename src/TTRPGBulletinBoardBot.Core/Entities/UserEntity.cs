namespace TTRPGBulletinBoardBot.Core.Entities
{
    public record UserEntity(long Id, Stage Stage)
    {
        public virtual bool Equals(UserEntity? other)
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
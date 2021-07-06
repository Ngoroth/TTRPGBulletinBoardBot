using System.Collections.Generic;
using System.Linq;
using TTRPGBulletinBoardBot.Core.Entities;

namespace TTRPGBulletinBoardBot.Core.Repositories
{
    public class UsersRepository
    {
        private readonly HashSet<UserEntity> _entities = new();

        public void Add(UserEntity entity)
        {
            _entities.Add(entity);
        }

        public UserEntity? FindOne(long userId)
        {
            return _entities.FirstOrDefault(entity => entity.Id == userId);
        }

        public void Update(UserEntity userEntity)
        {
            _entities.Remove(userEntity);
            _entities.Add(userEntity);
        }
    }
}
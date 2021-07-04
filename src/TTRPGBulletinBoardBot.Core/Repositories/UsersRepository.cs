using System.Collections.Generic;
using System.Linq;
using TTRPGBulletinBoardBot.Core.Entities;

namespace TTRPGBulletinBoardBot.Core.Repositories
{
    public class UsersRepository
    {
        private readonly HashSet<UserEntity> _entities = new HashSet<UserEntity>();

        public void Add(UserEntity entity)
        {
            _entities.Add(entity);
        }

        public UserEntity? FindOne(long id)
        {
            return _entities.FirstOrDefault(entity => entity.Id == id);
        }
    }
}
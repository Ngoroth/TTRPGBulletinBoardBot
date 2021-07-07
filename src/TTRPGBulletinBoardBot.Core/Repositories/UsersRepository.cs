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

        public UserEntity GetOne(long userId)
        {
            var userEntity = _entities.FirstOrDefault(entity => entity.Id == userId);
            if (userEntity is not null) return userEntity;
            userEntity = new UserEntity(userId, Stage.Start);
            Add(userEntity);
            return userEntity;
        }

        public void Update(UserEntity userEntity)
        {
            updateEntity(userEntity);
        }

        private void updateEntity(UserEntity? userEntity)
        {
            _entities.Remove(userEntity);
            _entities.Add(userEntity);
        }
    }
}
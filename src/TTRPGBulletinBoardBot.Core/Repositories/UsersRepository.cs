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
            updateEntity(userEntity);
        }
        
        public void SetUserStage(long userId, Stage stage)
        {
            var userEntity = _entities.Single(entity => entity.Id == userId) with {Stage = stage};
            updateEntity(userEntity);
        }

        public void SetAnswer(long userId, Stage stage, string answer)
        {
            var userEntity = _entities.Single(entity => entity.Id == userId);
            userEntity.Answers[stage] = answer;
            updateEntity(userEntity);
        }

        private void updateEntity(UserEntity? userEntity)
        {
            _entities.Remove(userEntity);
            _entities.Add(userEntity);
        }
    }
}
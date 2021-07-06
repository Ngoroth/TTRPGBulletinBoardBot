using System;
using TTRPGBulletinBoardBot.Core.Entities;
using TTRPGBulletinBoardBot.Core.Repositories;

namespace TTRPGBulletinBoardBot.Core.Services
{
    public class StageService
    {
        private readonly UsersRepository _usersRepository;

        public StageService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Stage GetUserStage(long userId)
        {
            var userEntity = _usersRepository.FindOne(userId);
            if (userEntity is not null) 
                return userEntity.Stage;
            
            userEntity = new UserEntity(userId, Stage.Start);
            _usersRepository.Add(userEntity);
            return userEntity.Stage;
        }

        public void SetNextStage(long userId, Stage currentStage)
        {
            var userEntity = _usersRepository.FindOne(userId);
            userEntity = userEntity! with {Stage = currentStage + 1};
            _usersRepository.Update(userEntity);
        }
    }
}
using System;
using TTRPGBulletinBoardBot.Core.Repositories;

namespace TTRPGBulletinBoardBot.Core.Services
{
    public class ActionService
    {
        private readonly UsersRepository _usersRepository;

        public ActionService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Stage GetUserStage(long userId)
        {
            var userEntity = _usersRepository.FindOne(userId);

            return userEntity?.Stage ?? Stage.Start;
        }
    }
}
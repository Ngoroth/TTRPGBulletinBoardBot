using System;
using TTRPGBulletinBoardBot.Core.Repositories;

namespace TTRPGBulletinBoardBot.Core.Services
{
    public class BotService
    {
        private readonly StageService _stageService;
        private readonly PhraseService _phraseService;
        private readonly UsersRepository _usersRepository;

        public BotService(StageService stageService, PhraseService phraseService, UsersRepository usersRepository)
        {
            _stageService = stageService;
            _phraseService = phraseService;
            _usersRepository = usersRepository;
        }

        public (BotAction BotAction, string BotMessage) Process(long userId, string userMessage)
        {
            var userStage = _stageService.GetUserStage(userId);

            switch (userStage)
            {
                case Stage.AskGameName:
                    
            }
            
        }
        
        public BotAction DefineBotAction(long userId, string message)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
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

        public IEnumerable<(BotAction BotAction, string BotMessage)> Process(long userId, string userMessage)
        {
            var userStage = _stageService.GetUserStage(userId);

            switch (userStage)
            {
                case Stage.Start:
                    if (!userMessage.StartsWith("/find_players"))
                        return new[] {(BotAction.SendMessageToUser, _phraseService.GetPhrase(Stage.Start))};
                    _stageService.SetNextStage(userId, userStage);
                    return new[] {(BotAction.SendMessageToUser, _phraseService.GetPhrase(Stage.AskGameName))};

                case Stage.AskGameName:
                case Stage.AskDescription:
                case Stage.AskExpectations:
                case Stage.AskSystem:
                    _stageService.SetNextStage(userId, userStage);
                    _usersRepository.SetAnswer(userId, userStage, userMessage);
                    return new[] {(BotAction.SendMessageToUser, _phraseService.GetPhrase(userStage + 1))};
                case Stage.AskDateTime:
                    _stageService.SetNextStage(userId, userStage);
                    _usersRepository.SetAnswer(userId, userStage, userMessage);
                    return new[]
                    {
                        (BotAction.SendMessageToUser, _phraseService.GetPhrase(userStage + 1)),
                        (BotAction.MakePublicationInChannel, _phraseService.GetPhrase(userStage))
                    };
                case Stage.Publication:
                    return new[] {(BotAction.MakePublicationInChannel, _phraseService.GetPhrase(userStage))};
                default:
                    return new[] {(BotAction.SendMessageToUser, _phraseService.GetPhrase(Stage.Start))};
            }

        }
        
        public BotAction DefineBotAction(long userId, string message)
        {
            throw new NotImplementedException();
        }
    }
}
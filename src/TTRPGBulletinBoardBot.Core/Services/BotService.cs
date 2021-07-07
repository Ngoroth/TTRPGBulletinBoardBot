using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TTRPGBulletinBoardBot.Core.Entities;
using TTRPGBulletinBoardBot.Core.Repositories;

namespace TTRPGBulletinBoardBot.Core.Services
{
    public class BotService
    {
        private readonly PhraseService _phraseService;
        private readonly UsersRepository _usersRepository;
        private readonly PublicationService _publicationService;

        public BotService(PhraseService phraseService, UsersRepository usersRepository,
            PublicationService publicationService)
        {
            _phraseService = phraseService;
            _usersRepository = usersRepository;
            _publicationService = publicationService;
        }

        public IEnumerable<(BotAction BotAction, string BotMessage)> Process(long userId, string userMessage)
        {
            var userEntity = _usersRepository.GetOne(userId);

            switch (userEntity.Stage)
            {
                case Stage.Publication:
                case Stage.Start:
                    if (!userMessage.StartsWith("/find_players"))
                        return new[] {(BotAction.SendMessageToUser, _phraseService.GetPhrase(Stage.Start))};
                    _usersRepository.Update(userEntity with {Stage = Stage.AskGameName});
                    return new[] {(BotAction.SendMessageToUser, _phraseService.GetPhrase(Stage.AskGameName))};

                case Stage.AskGameName:
                case Stage.AskDescription:
                case Stage.AskExpectations:
                case Stage.AskSystem:
                    return processAskStage(userEntity, userMessage);
                case Stage.AskDateTime:
                    return processAskStage(userEntity, userMessage)
                        .Concat(new[]
                        {
                            (BotAction.MakePublicationInChannel, _publicationService.MakePublicationText(userId))
                        });
                default:
                    return new[] {(BotAction.SendMessageToUser, _phraseService.GetPhrase(Stage.Start))};
            }
        }

        private IEnumerable<(BotAction BotAction, string BotMessage)> processAskStage(UserEntity userEntity, string answer)
        {
            var updatedUserEntity = userEntity with {Stage = userEntity.Stage + 1};
            updatedUserEntity.Answers[userEntity.Stage] = answer;
            _usersRepository.Update(updatedUserEntity);
            return new[] {(BotAction.SendMessageToUser, _phraseService.GetPhrase(updatedUserEntity.Stage))};
        }
    }
}
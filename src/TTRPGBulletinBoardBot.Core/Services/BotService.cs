using System.Collections.Generic;
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

        public IEnumerable<(BotAction BotAction, TextFormat TextFormat, string BotMessage)> Process(long userId,
            string userMessage)
        {
            var userEntity = _usersRepository.GetOne(userId);

            if (userMessage.StartsWith("/find_players"))
                return resetToStage(userEntity, Stage.AskSystem);

            UserEntity updatedUserEntity;
            switch (userEntity.Stage)
            {
                case Stage.Start:
                case Stage.Publication:
                    return new[]
                        {(BotAction.SendMessageToUser, TextFormat.Text, _phraseService.GetPhrase(Stage.Start))};
                case Stage.AskSystem:
                case Stage.AskMaster:
                case Stage.AskExpectations:
                case Stage.AskDateTime:
                case Stage.AskLocation:
                case Stage.AskGameName:
                    updatedUserEntity = setAnswer(userEntity, userMessage);
                    return new[]
                    {
                        (BotAction.SendMessageToUser, TextFormat.Text,
                            _phraseService.GetPhrase(updatedUserEntity.Stage))
                    };
                case Stage.AskDescription:
                    updatedUserEntity = setAnswer(userEntity, userMessage);
                    return new[]
                    {
                        (BotAction.SendMessageToUser, TextFormat.MarkdownV2,
                            _publicationService.MakePublicationText(updatedUserEntity)),
                        (BotAction.SendMessageToUser, TextFormat.Text,
                            _phraseService.GetPhrase(updatedUserEntity.Stage))
                    };
                case Stage.Preview:
                    if (!userMessage.ToLower().StartsWith("да"))
                        return resetToStage(userEntity, Stage.Start);

                    updatedUserEntity = setAnswer(userEntity, userMessage);
                    return new[]
                    {
                        (BotAction.SendMessageToUser, TextFormat.Text,
                            _phraseService.GetPhrase(updatedUserEntity.Stage)),
                        (BotAction.MakePublicationInChannel, TextFormat.MarkdownV2,
                            _publicationService.MakePublicationText(updatedUserEntity))
                    };

                default:
                    return new[]
                        {(BotAction.SendMessageToUser, TextFormat.Text, _phraseService.GetPhrase(Stage.Start))};
            }
        }

        private UserEntity setAnswer(UserEntity userEntity, string answer)
        {
            var updatedUserEntity = userEntity with {Stage = userEntity.Stage + 1};
            updatedUserEntity.Answers[userEntity.Stage] = answer;
            _usersRepository.Update(updatedUserEntity);
            return updatedUserEntity;
        }

        private IEnumerable<(BotAction BotAction, TextFormat TextFormat, string BotMessage)> resetToStage(
            UserEntity userEntity, Stage stage)
        {
            _usersRepository.Update(userEntity with {Stage = stage});
            return new[] {(BotAction.SendMessageToUser, TextFormat.Text, _phraseService.GetPhrase(stage))};
        }
    }
}
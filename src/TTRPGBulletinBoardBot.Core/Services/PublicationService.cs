using System;
using System.Text;
using TTRPGBulletinBoardBot.Core.Repositories;

namespace TTRPGBulletinBoardBot.Core.Services
{
    public class PublicationService
    {
        private UsersRepository _usersRepository;

        public PublicationService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public string MakePublicationText(long userId)
        {
            var userEntity = _usersRepository.FindOne(userId);
            var publicationMessage = new StringBuilder("#Поиск_Игроков");
            publicationMessage.AppendLine();
            publicationMessage.AppendLine(userEntity!.Answers[Stage.AskGameName]);
            publicationMessage.AppendLine(userEntity.Answers[Stage.AskDescription]);
            publicationMessage.AppendLine("Система:");
            publicationMessage.AppendLine(convertToTag(userEntity.Answers[Stage.AskSystem]));
            publicationMessage.AppendLine("От игроков ожидается:");
            publicationMessage.AppendLine(userEntity.Answers[Stage.AskExpectations]);
            publicationMessage.AppendLine("Время и дата игры:");
            publicationMessage.AppendLine(userEntity.Answers[Stage.AskDateTime]);
            return publicationMessage.ToString();
        }

        private string convertToTag(string text)
        {
            return $"#{text.Replace(" ", "_")}";
        }
    }
}
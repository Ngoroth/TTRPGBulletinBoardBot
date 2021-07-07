using System;
using System.Text;
using TTRPGBulletinBoardBot.Core.Extentions;
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
            var userEntity = _usersRepository.GetOne(userId);
            var publicationMessage = new StringBuilder("#Поиск_Игроков".EscapeMarkdownV2());
            publicationMessage.AppendLine();
            publicationMessage.AppendLine(makeBold(userEntity!.Answers[Stage.AskGameName].EscapeMarkdownV2()));
            publicationMessage.AppendLine(userEntity.Answers[Stage.AskDescription].EscapeMarkdownV2());
            publicationMessage.AppendLine("Система:".EscapeMarkdownV2());
            publicationMessage.AppendLine(convertToTag(userEntity.Answers[Stage.AskSystem]).EscapeMarkdownV2());
            publicationMessage.AppendLine("От игроков ожидается:".EscapeMarkdownV2());
            publicationMessage.AppendLine(userEntity.Answers[Stage.AskExpectations].EscapeMarkdownV2());
            publicationMessage.AppendLine("Время и дата игры:".EscapeMarkdownV2());
            publicationMessage.AppendLine(userEntity.Answers[Stage.AskDateTime].EscapeMarkdownV2());
            return publicationMessage.ToString();
        }

        private string convertToTag(string text)
        {
            return $"#{text.Replace(" ", "_")}";
        }

        private string makeBold(string text)
        {
            return $"*{text}*";
        }
    }
}
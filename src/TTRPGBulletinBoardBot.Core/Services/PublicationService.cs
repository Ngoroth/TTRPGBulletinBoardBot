﻿using System.Text;
using TTRPGBulletinBoardBot.Core.Entities;
using TTRPGBulletinBoardBot.Core.Extensions;

namespace TTRPGBulletinBoardBot.Core.Services
{
    public class PublicationService
    {
        public string MakePublicationText(UserEntity userEntity)
        {
            var publicationMessage = new StringBuilder("#Поиск_Игроков".EscapeMarkdownV2());
            publicationMessage.AppendLine();
            publicationMessage.AppendLine(userEntity!.Answers[Stage.AskGameName].EscapeMarkdownV2().MakeBold());
            publicationMessage.AppendLine(userEntity.Answers[Stage.AskDescription].EscapeMarkdownV2());
            publicationMessage.AppendLine("Система:".EscapeMarkdownV2());
            publicationMessage.AppendLine(userEntity.Answers[Stage.AskSystem].ConvertToTag().EscapeMarkdownV2());
            publicationMessage.AppendLine("От игроков ожидается:".EscapeMarkdownV2());
            publicationMessage.AppendLine(userEntity.Answers[Stage.AskExpectations].EscapeMarkdownV2());
            publicationMessage.AppendLine("Время и дата игры:".EscapeMarkdownV2());
            publicationMessage.AppendLine(userEntity.Answers[Stage.AskDateTime].EscapeMarkdownV2());
            return publicationMessage.ToString();
        }
    }
}
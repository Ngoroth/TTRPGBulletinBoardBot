using System;
using System.Collections.Generic;

namespace TTRPGBulletinBoardBot.Core.Services
{
    public class PhraseService
    {
        private readonly Dictionary<Stage, string> _phrases = new()
        {
            {Stage.Start, "Привет. используй команду /find_players для создания объявления о наборе игроков."},
            {Stage.AskGameName, "Какое название у игры?"},
            {Stage.AskDescription, "Пришли мне описание игры."},
            {Stage.AskSystem, "Укажи систему по которой будет проводиться игра."},
            {Stage.AskDateTime, "Когда и во сколько будет проводиться игра?"},
            {Stage.AskExpectations, "Что тебе хотелось бы видеть в игроках?"},
            {Stage.Publication, "Объявление о игре опубликовано в канале https://t.me/TTRPG_Bulletin_board"}
        };
        public string GetPhrase(Stage stage)
        {
            return _phrases[stage];
        }
    }
}
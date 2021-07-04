using System;
using System.Collections.Generic;

namespace TTRPGBulletinBoardBot.Core.Services
{
    public class PhraseService
    {
        private readonly Dictionary<Stage, string> _phrases = new()
        {
            {Stage.Start, "Привет. используй команду /find_players для создания объявления о наборе игроков."},
        };
        public string GetPhrase(Stage stage)
        {
            return _phrases[Stage.Start];
        }
    }
}
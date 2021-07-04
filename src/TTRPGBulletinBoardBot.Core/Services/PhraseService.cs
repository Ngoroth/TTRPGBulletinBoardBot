using System;
using System.Collections.Generic;

namespace TTRPGBulletinBoardBot.Core.Services
{
    public class PhraseService
    {
        private readonly Dictionary<Stage, string> _phrases = new()
        {
            {Stage.Start, ""}
        };
        public string GetPhrase(Stage stage)
        {
            throw new NotImplementedException();
        }
    }
}
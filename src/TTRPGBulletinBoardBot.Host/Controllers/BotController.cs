using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TTRPGBulletinBoardBot.Core;
using TTRPGBulletinBoardBot.Core.Services;

namespace TTRPGBulletinBoardBot.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : ControllerBase
    {
        private readonly ITelegramBotClient _botClient;
        private readonly BotService _botService;

        public BotController(ILogger<BotController> logger, BotService botService, ITelegramBotClient botClient)
        {
            _botService = botService;
            _botClient = botClient;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Update update)
        {
            var actions = _botService.Process(update.Message.Chat.Id, update.Message.Text);

            foreach (var (action, text) in actions)
            {
                switch (action)
                {
                    case BotAction.SendMessageToUser:
                        await _botClient.SendTextMessageAsync(update.Message.Chat.Id, text);
                        break;
                    case BotAction.MakePublicationInChannel:
                        await _botClient.SendTextMessageAsync(-1001364811368, text, ParseMode.MarkdownV2);
                        break;
                }
            }

            return new OkResult();
        }
    }
}
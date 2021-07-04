using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using TTRPGBulletinBoardBot.Core.Services;

namespace TTRPGBulletinBoardBot.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : ControllerBase
    {
        private readonly ILogger<BotController> _logger;
        private readonly ITelegramBotClient _botClient;
        private readonly StageService _stageService;
        private readonly PhraseService _phraseService;

        public BotController(ILogger<BotController> logger, StageService stageService, PhraseService phraseService)
        {
            _logger = logger;
            _stageService = stageService;
            _phraseService = phraseService;
            _botClient = new TelegramBotClient("");
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Update update)
        {
            //-1001364811368
            var userStage = _stageService.GetUserStage(update.Message.Chat.Id);
            await _botClient.SendTextMessageAsync(update.Message.Chat.Id, _phraseService.GetPhrase(userStage));
            _stageService.SetNextStage(update.Message.Chat.Id, userStage);
            return new OkResult();
        }
    }
}
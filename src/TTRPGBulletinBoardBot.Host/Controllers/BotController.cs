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
        private readonly ActionService _actionService;

        public BotController(ILogger<BotController> logger, ActionService actionService)
        {
            _logger = logger;
            _actionService = actionService;
            _botClient = new TelegramBotClient("");
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Update update)
        {
            var userStage = _actionService.GetUserStage(update.Message.Chat.Id);
            await _botClient.SendTextMessageAsync(-1001364811368, update.Message.Text);
            return new OkResult();
        }
    }
}
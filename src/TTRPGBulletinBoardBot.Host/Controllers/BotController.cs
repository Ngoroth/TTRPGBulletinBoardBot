using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TTRPGBulletinBoardBot.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : ControllerBase
    {
        private readonly ILogger<BotController> _logger;
        private readonly ITelegramBotClient _botClient;

        public BotController(ILogger<BotController> logger)
        {
            _logger = logger;
            _botClient = new TelegramBotClient("");
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Update update)
        {
            await _botClient.SendTextMessageAsync(-1001364811368, update.Message.Text);
            return new OkResult();
        }
    }
}
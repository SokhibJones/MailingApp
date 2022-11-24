using MailingApp.Models;
using MailingApp.Models.Entities;
using MailingApp.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MailingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string userName)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.FindUserByNameAsync(userName);
                TempData["success"] = $"Welcome {userName}";

                return RedirectToAction(actionName: nameof(MessageBoard), new { userName = result.Name });
            }

            return View();
        }

        public async Task<IActionResult> MessageBoard(string userName)
        {
            var user = await _userRepository.FindUserByNameAsync(userName);
            var userMessageData = new MessageViewModel
            {
                Sender = user,
                Users = await _userRepository.FindAllUsersAsync(),
                InboxMessages = await _userRepository.InboxAsync(user),
                SentMessages = await _userRepository.SentMessagesAsync(user)
            };            

            return View(userMessageData);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(Guid fromUserId, Guid toUserId, string subject, string messageBody)
        {
            User sender = await _userRepository.FindUserByIdAsync(fromUserId);
            User recipient = await _userRepository.FindUserByIdAsync(toUserId);

            try
            {
                var message = new Message
                {
                    From = sender,
                    To = recipient,
                    Subject = subject,
                    Body = messageBody
                };

                await _messageRepository.AddMessageAsync(message);
                TempData["success"] = "Message sent successfully";
            }
            catch
            {
                TempData["error"] = "Could not send the message. Error occured";
            }
            
            return RedirectToAction(nameof(MessageBoard), new { userName = sender.Name });
        }

        [HttpGet]
        public async Task<IActionResult> MessageDetails(Guid messageId, string messageType)
        {
            Message message = await _messageRepository.FindMessageByIdAsync(messageId);

            if (message is null)
            {
                return NotFound();
            }

            ViewData["messageHeader"] = messageType == "inbox" ? $"From: {message.From.Name}" : $"To: {message.To.Name}";

            return View(message);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using ChatChallange.Hubs;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Chat_Challange.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string UserName { get; set; }

        public void OnGet()
        {
            if (User != default)
            {
                UserName = User.Identity.Name;
            }

        }
    }
}

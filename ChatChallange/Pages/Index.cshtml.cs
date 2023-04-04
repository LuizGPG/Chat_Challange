using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chat_Challange.Pages
{
    public class IndexModel : PageModel
    {
        public string UserName { get; set; }
        public IndexModel()
        {
        }

        public void OnGet()
        {
            if (User != default)
            {
                var name = User.Identity.Name;
                UserName = name.Substring(0, name.IndexOf("@"));
            }

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenSocials.App_Code;

namespace OpenSocials.Pages
{
    public class NewsModel : PageModel
    {
        private readonly DataContext _context;

        public NewsModel(DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<News> NewsDB { get; set; }

        public void OnGet()
        {

            var newsList = _context.News.ToList();

            if (newsList != null)
            {
                NewsDB = newsList;
            }
        }
    }
}

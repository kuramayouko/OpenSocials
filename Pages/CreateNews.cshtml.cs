using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using OpenSocials.App_Code;

namespace OpenSocials.Pages
{
    public class CreateNewsModel : PageModel
    {

		private readonly DataContext _context;
		
		public CreateNewsModel(DataContext context)
		{
			_context = context;
		}

		[BindProperty]
		public News NewsBD { get; set; }
	
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        { 
            NewsBD.Is_Approved = 0;
            NewsBD.Date_Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString("yyyy-MM-ddTHH:mm:sszz00");

            // Salvando no BD
            _context.News.Add(NewsBD);
            _context.SaveChanges();

            return RedirectToPage("/Index");
        }

    }
}

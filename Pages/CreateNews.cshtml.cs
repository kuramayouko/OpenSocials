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
		
		[BindProperty]
		public NewsMedia NewsMediaBD { get; set; }
		
		public String UrlString;
	
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        { 
            NewsBD.Is_Approved = 0;
            NewsBD.Date_Created = DateTimeOffset.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffzz00");
            NewsBD.Media_Id = 0;

            // Salvando no BD
            _context.News.Add(NewsBD);
            _context.SaveChanges();

            int generatedId = NewsBD.Id;

            // Update News.MediaId with the same value as Id

            _context.News.Update(NewsBD);
            _context.SaveChanges();

            if (NewsMediaBD.Base64 != "0")
			{
                NewsMediaBD.Id = generatedId;
                _context.NewsMedia.Add(NewsMediaBD);
				_context.SaveChanges();
			}
			
            return RedirectToPage("/News");
        }

    }
}

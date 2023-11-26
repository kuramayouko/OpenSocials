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


            // Salvando no BD
            _context.News.Add(NewsBD);
            _context.SaveChanges();
            
            // Salvando Anexo falta configurar junto com o upload
			
			if(NewsMediaBD.Base64 != "0")
			{
				NewsMediaBD.Id = NewsBD.Id;
				_context.NewsMedia.Add(NewsMediaBD);
				_context.SaveChanges();
			}
			
			

            return RedirectToPage("/Index");
        }

    }
}

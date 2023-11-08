using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenSocials.Pages
{
    public class FacebookAuthModel : PageModel
    {
		private readonly IConfiguration _configuration;

        public FacebookAuthModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void OnGet()
        {
			// Pegar os valores do BD n usar no appsettings.json 
            string appId;
            string appSecret;
            string redirectUri; // RedirectURL

            string facebookLoginUrl = $"https://www.facebook.com/v18.0/dialog/oauth?client_id={appId}&redirect_uri={Uri.EscapeDataString(redirectUri)}&response_type=token";

            Response.Redirect(facebookLoginUrl);
        }
        
        public async Task OnGetCallback(string access_token)
        {
            if (!string.IsNullOrEmpty(access_token))
            {
                string longLivedToken = await ExchangeForLongLivedToken(access_token);

                var userDetails = await GetFacebookUserDetails(longLivedToken);

                // Pegar as resposta do JSON
                // Ex;
                // var userId = userDetails["id"];
                // var userName = userDetails["name"];
            }
        }
        
        private protected async Task<string> ExchangeForLongLivedToken(string shortLivedToken)
        {
            string appId = _configuration["Facebook:AppId"];
            string appSecret = _configuration["Facebook:AppSecret"];

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id={appId}&client_secret={appSecret}&fb_exchange_token={shortLivedToken}");
                response.EnsureSuccessStatusCode();

                dynamic data = await response.Content.ReadAsAsync<dynamic>();
                return data.access_token;
            }
        }
        
        private protected async Task<dynamic> GetFacebookUserDetails(string accessToken)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://graph.facebook.com/me?fields=id,name,email&access_token={accessToken}");
                response.EnsureSuccessStatusCode();

                dynamic data = await response.Content.ReadAsAsync<dynamic>();
                return data;
            }
        }

    }
}

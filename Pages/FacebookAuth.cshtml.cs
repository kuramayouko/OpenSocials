using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Json;
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
            //Pegar os valores do BD
            string appId = "";
            string redirectUri = "";

            string facebookLoginUrl = $"https://www.facebook.com/v18.0/dialog/oauth?client_id={appId}&redirect_uri={Uri.EscapeDataString(redirectUri)}&response_type=token";

            Redirect(facebookLoginUrl);
        }

        public async Task<IActionResult> OnGetCallback(string access_token)
        {
            if (!string.IsNullOrEmpty(access_token))
            {
                string longLivedToken = await ExchangeForLongLivedToken(access_token);

                var userDetails = await GetFacebookUserDetails(longLivedToken);

                // Resposta JSON implementar
                // Ex:
                // var userId = userDetails["id"];
                // var userName = userDetails["name"];
            }
            return Page();
        }

        private async Task<string> ExchangeForLongLivedToken(string shortLivedToken)
        {
            string appId = "";
            string appSecret = "";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id={appId}&client_secret={appSecret}&fb_exchange_token={shortLivedToken}");
                response.EnsureSuccessStatusCode();

                dynamic data = "";

                //data = await response.Content.ReadAsAsync();

                return data.access_token;
            }
        }

        private async Task<dynamic> GetFacebookUserDetails(string accessToken)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://graph.facebook.com/me?fields=id,name,email&access_token={accessToken}");
                response.EnsureSuccessStatusCode();

                dynamic data = "";

                //data = await response.Content.ReadAsAsync();

                return data;
                
            }
        }
    }
}

namespace OpenSocials.App_Code
{
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class Facebook
    {
        private String appId { get; set; }
        private String appSecret { get; set; }
        private String redirectUri { get; set; }
        private String accessToken { get; set; }
        private String authorizeUrl { get; set; }

        public string GenerateCodeURL()
        {
            this.authorizeUrl = $"https://www.facebook.com/v12.0/dialog/oauth?client_id={this.appId}&redirect_uri={this.redirectUri}";
            return this.authorizeUrl;
        }

        public async Task<string> GenerateTokenAsync(String authorizationCode)
        {
            var httpClient = new HttpClient();
            var tokenUrl = $"https://graph.facebook.com/v12.0/oauth/access_token?client_id={this.appId}&redirect_uri={this.redirectUri}&client_secret={this.appSecret}&code={authorizationCode}";
            var response = await httpClient.GetAsync(tokenUrl);
            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<dynamic>(content);
            this.accessToken = tokenResponse.access_token;

            return this.accessToken;
        }
    }
}

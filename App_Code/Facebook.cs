namespace OpenSocials.App_Code
{

    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class Post
    {
        public string createdTime { get; set; }
        public string message { get; set; }
        public string pagePostId { get; set; }
        public List<string> attachments { get; set; }
    }

    public class Comment
    {

        public string id { get; set; }
        public string message { get; set; }
        public Commenter from { get; set; }
    }

    public class Commenter
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class CommentResponse
    {
        public List<Comment> comments { get; set; }
    }

    public class Facebook
    {
        private String appId { get; set; }
        private String appSecret { get; set; }
        private String redirectUri { get; set; }
        private String userId { get; set; }
        private String pageId { get; set; }
        private String accessToken {set;}

        private String authorizeUrl { get; set; }

        ///<summary>Abre pagina de login no facebook para autenticacao, necessario setar APPID e RedirectUri antes!</summary>
        ///<return>Retorna uma URL que o usuario usara para autenticar</return>
        public string GenerateCodeURL()
        {
            this.authorizeUrl = $"https://www.facebook.com/v18.0/dialog/oauth?client_id="+this.appId+"&redirect_uri="+this.redirectUri;
            return this.authorizeUrl;
        }

        ///<summary>Gera um token permanente de acesso do usuario e da pagina, necessario autenticar primeiro![GenerateCodeURL]</summary>
        ///<param name="authorizationCode">Codigo de autorizacao gerado pelo methodo [GenerateCodeURL]</param>
        ///<return>Retorna uma URL que o usuario usara para autenticar</return>
        public async Task<string> GenerateTokenAsync(String authorizationCode)
        {
            var httpClient = new HttpClient();

            // O metodo de obter o token sera mudado para mais simple!!!

            //Token usuario temporario 1 hora
            var tokenUser = $"https://graph.facebook.com/v18.0/oauth/access_token?client_id="+this.appId+"&redirect_uri="+this.redirectUri+"&client_secret="+this.appSecret+"&code="+authorizationCode;
            var response = await httpClient.GetAsync(tokenUser);
            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<dynamic>(content);
            this.accessToken = tokenResponse.access_token;

            //Token usuario de termo longo
            var tokenUserPerm = $"https://graph.facebook.com/v18/oauth/access_token?grant_type=fb_exchange_token&client_id="+this.appId+"&client_secret="+this.appSecret+"&fb_exchange_token="+this.accessToken;
            var response = await httpClient.GetAsync(tokenPage);
            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<dynamic>(content);
            this.accessToken = tokenResponse.access_token;

            //Token de pagina permanente
            var tokenPagePerm = $"https://graph.facebook.com/v18/"+this.facebookUserId+"/accounts?access_token="+this.accessToken
            var response = await httpClient.GetAsync(tokenPage);
            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<dynamic>(content);
            this.accessToken = tokenResponse.access_token;

            return this.accessToken;
        }


        //PageGetFeed needs redone
        public async Task<List<Post>> PageGetFeed()
        {
            using (HttpClient client = new HttpClient())
            {
                List<Post> posts = new List<Post>();

                try
                {
                    string requestUrl = $"https://graph.facebook.com/v18.0/" + this.pageId + "/feed?access_token=" + this.accessToken + "";

                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<JObject>(responseContent);

                        if (data != null && data["data"] != null)
                        {
                            foreach (var item in data["data"])
                            {
                                Post post = new Post
                                {
                                    createdTime = item["created_time"].ToString(),
                                    message = item["message"] != null ? item["message"].ToString() : "",
                                    pagePostId = item["id"].ToString()
                                };

                                if (item["attachments"] == null)
                                {
                                    if (attachmentsData != null && attachmentsData["data"] != null)
                                    {
                                        foreach (var attachment in attachmentsData["data"])
                                        {
                                            if (attachment["subattachments"] != null)
                                            {
                                                foreach (var subAttachment in attachment["subattachments"]["data"])
                                                {
                                                    if (subAttachment["media"] != null && subAttachment["media"]["image"] != null)
                                                    {
                                                    post.Attachments.Add(subAttachment["media"]["image"]["src"].ToString());
                                                    }
                                                }
                                            }
                                            else if (attachment["media"] != null && attachment["media"]["image"] != null)
                                            {
                                                post.Attachments.Add(attachment["media"]["image"]["src"].ToString());
                                            }
                                        }
                                    }
                                }      
                                
                            }
                            posts.Add(post);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Falha ao obter posts. Erro: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
            return posts;
        }

        public async Task PagePostMessage(string message)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("message", message),
                        new KeyValuePair<string, string>("access_token", this.accessToken)
                    });

                    var response = await client.PostAsync($"https://graph.facebook.com/v18.0/"+ this.pageId +"/feed", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Postagem realizada com sucesso!!!");
                    }
                    else
                    {
                        Console.WriteLine($"Erro ao postar... Erro: {response.StatusCode}");
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Resposta do servidor: " + responseContent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
        }

        public async Task PageEditPostMessage(string updatedMessage, string postId, string accessToken)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("message", updatedMessage),
                        new KeyValuePair<string, string>("access_token", accessToken)
                    });

                    var response = await client.PostAsync($"https://graph.facebook.com/v18.0/"+this.postId, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Post atualizado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine($"Erro ao atualizar post. Erro: {response.StatusCode}");
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Resposta do servidor: " + responseContent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
        }

        public async Task PagePostMessageMedia(string message, List<Media> mediaList)
        {
            using (HttpClient client = new HttpClient())
            { 

                try
                {
                    var multipartContent = new MultipartFormDataContent();

                    multipartContent.Add(new StringContent(message), "message");
                    multipartContent.Add(new StringContent(this.accessToken), "access_token");

                    for (int i = 0; i < mediaList.Count; i++)
                    {
                        string mediaType = mediaList[i].mediaType;
                        string base64Data = mediaList[i].ConvertToBase64();
                        multipartContent.Add(new StringContent(base64Data), $"source_data[{i}]");

                        // Opcional verifica se ha titulo ou descricao da foto
                        if (!string.IsNullOrEmpty(mediaList[i].mediaTitle))
                        {
                            multipartContent.Add(new StringContent(mediaList[i].mediaTitle), $"title[{i}]");
                        }

                        if (!string.IsNullOrEmpty(mediaList[i].MediaDescription))
                        {
                            multipartContent.Add(new StringContent(mediaList[i].mediaDescription), $"description[{i}]");
                        }
                    }

                    if (mediaList[0].mediaType == "photo")
                    {
                        HttpResponseMessage response = await client.PostAsync($"https://graph.facebook.com/v18.0/"+this.pageId+"/photos", multipartContent);
                    }
                    else
                    {
                        HttpResponseMessage response = await client.PostAsync($"https://graph.facebook.com/v18.0/"+this.pageId+"/videos", multipartContent);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Postagem realizada com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine($"Falha ao postar... Erro: {response.StatusCode}");
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Resposta do servidor: " + responseContent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
        }
        
        public async Task PagePostDeleteMedia(string postId, List<string> mediaIds, string accessToken)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    foreach (var mediaId in mediaIds)
                    {
                        string requestUrl = $"https://graph.facebook.com/v18.0/"+postId+"_"+mediaId+"?access_token={accessToken}";

                        HttpResponseMessage response = await client.DeleteAsync(requestUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"Midia deletada com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine($"Falha ao deletar midia. Erro: {response.StatusCode}");
                            string responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine("Resposta do servidor: " + responseContent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
        }

        public async Task<List<Comment>> GetPostComments(string postId, string accessToken)
        {
            using (HttpClient client = new HttpClient())
            {
                List<Comment> comments = null;

                try
                {
                    string requestUrl = $"https://graph.facebook.com/v18.0/{postId}/comments?access_token={accessToken}";

                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<CommentResponse>(responseContent);

                        if (data != null)
                        {
                            comments = data.Comments;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Falha ao obter comentarios da postagem. Erro: {response.StatusCode}");
                    }
                }   
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }

                return comments;
            }
        }

        public async Task ReplyToComment(string commentId, string message, string accessToken, string mediaPath = null)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var multipartContent = new MultipartFormDataContent();

                    multipartContent.Add(new StringContent(message), "message");
                    multipartContent.Add(new StringContent(accessToken), "access_token");

                    if (!string.IsNullOrEmpty(mediaPath))
                    {
                        byte[] fileBytes = System.IO.File.ReadAllBytes(mediaPath);
                        multipartContent.Add(new ByteArrayContent(fileBytes), "source", System.IO.Path.GetFileName(mediaPath));
                    }

                    string requestUrl = $"https://graph.facebook.com/v18.0/{commentId}/comments";

                    HttpResponseMessage response;

                    if (string.IsNullOrEmpty(mediaPath))
                    {
                        // Responde sem midia
                        response = await client.PostAsync(requestUrl, multipartContent);
                    }
                    else
                    {
                        // Responde com midia
                        response = await client.PostAsync(requestUrl, multipartContent);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Comentario respondido com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine($"Falha ao responder comentario. Erro: {response.StatusCode}");
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Resposta do servidor: " + responseContent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
        }

        public async Task SchedulePost(string message, string scheduledTime, string accessToken, List<Media> mediaPaths = null)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var multipartContent = new MultipartFormDataContent();
                    multipartContent.Add(new StringContent(message), "message");
                    multipartContent.Add(new StringContent(scheduledTime), "published");
                    multipartContent.Add(new StringContent(accessToken), "access_token");

                    if (mediaPaths != null)
                    {
                        foreach (var mediaPath in mediaPaths)
                        {
                            byte[] fileBytes = System.IO.File.ReadAllBytes(mediaPath);
                            multipartContent.Add(new ByteArrayContent(fileBytes), "source", System.IO.Path.GetFileName(mediaPath));
                        }
                    }

                    HttpResponseMessage response = await client.PostAsync("https://graph.facebook.com/v18.0/me/feed", multipartContent);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Post scheduled successfully!");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to schedule post. Status Code: {response.StatusCode}");
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Server Response: " + responseContent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

    }
}

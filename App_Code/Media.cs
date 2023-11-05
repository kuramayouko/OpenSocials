namespace OpenSocials.App_Code
{
    using System;
    using System.IO;

    public class Media
    {
        private string mediaTitle { get; set; }
        private string mediaDescription { get; set; }
        private string mediaType { get; set; }
        private string mediaLocalUrl { get; set; }
        private string base64 { get; set; }

        public Media(string title, string description, string localUrl)
        {
            this.mediaTitle = title;
            this.mediaDescription = description;
            this.mediaLocalUrl = localUrl;

            // Determina o tipo de arquivo (foto ou video)
            string fileExtension = Path.GetExtension(localUrl);
            if (fileExtension != null)
            {
                fileExtension = fileExtension.ToLower();
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                {
                    mediaType = "photo";
                    this.ConvertToBase64();
                }
                else if (fileExtension == ".mp4" || fileExtension == ".avi" || fileExtension == ".mov")
                {
                    mediaType = "video";
                    this.ConvertToBase64();
                }
                else
                {
                    throw new ArgumentException("Formato de midia nao reconhecido!");
                }
            }
            else
            {
                throw new ArgumentException("Localizacao de arquivo invalida!");
            }
        }

        private void ConvertToBase64()
        {
            using (FileStream fileStream = File.OpenRead(mediaLocalUrl))
            {
                byte[] fileBytes = new byte[fileStream.Length];
                fileStream.Read(fileBytes, 0, (int)fileStream.Length);
                this.base64 = Convert.ToBase64String(fileBytes);
            }
        }
    }
}

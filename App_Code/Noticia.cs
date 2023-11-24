namespace OpenSocials.App_Code
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.EntityFrameworkCore.Sqlite;
    using System.Collections.Generic;

    public class Noticia
    {
        public int id { get; set; }
        public string tittle { get; set; }
        public string text { get; set; }
        public int media_id { get; set; }
        public int is_approved { get; set; }
        public int fb_postid { get; set; }
        public int insta_postid { get; set; }
        public string date_created { get; set; }
        public string date_posted { get; set; }
        public string date_schedule { get; set; }
    }
    public DbSet<AppConfig> AppConfigs { get; set; }
}

namespace OpenSocials.App_Code
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.EntityFrameworkCore.Sqlite;
    using System.Collections.Generic;
    public class MidiaNoticia
    {
        public int id { get; set; }
        public int base64 { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string type{ get; set; }
    }
    public DbSet<AppConfig> AppConfigs { get; set; }
}

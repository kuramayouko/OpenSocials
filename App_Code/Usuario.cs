namespace OpenSocials.App_Code
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.EntityFrameworkCore.Sqlite;
    using System.Collections.Generic;
    public class Usuario
    {
        public int id { get; set; }
        public string username { get; set; }
        public string nickname { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public int attempts { get; set; }
        public int is_admin { get; set; }
        public int is_commenter { get; set; }
        public int is_reviewer { get; set; }
    }
    public DbSet<AppConfig> AppConfigs { get; set; }
}

namespace OpenSocials.App_Code
{    
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.EntityFrameworkCore.Sqlite;

	public class AppConfig
	{
		public int Id { get; set; }
		public string FbUserToken { get; set; }
		public string FbPageToken { get; set; }
		public string InstaUserToken { get; set; }
		public string InstaPageToken { get; set; }
		public string AppId { get; set; }
		public string AppSecret { get; set; }
	}
	
	public class DataContext : DbContext
	{
		protected readonly IConfiguration Configuration;

		public DataContext(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlite(Configuration.GetConnectionString("SQLDatabase"));
		}
		
		// Add todas classes que representam dados do BD
		public DbSet<AppConfig> AppConfigs { get; set; }
	}
}

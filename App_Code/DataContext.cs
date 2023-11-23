namespace OpenSocials.App_Code
{    
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.EntityFrameworkCore.Sqlite;
    using System.ComponentModel.DataAnnotations;

    public class AppConfig
	{
		[Key]
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
		//Movido para o Startup.cs
		//protected override void OnConfiguring(DbContextOptionsBuilder options)
		//{
			//options.UseSqlite(Configuration.GetConnectionString("SQLDatabase"));
		//}
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Config com foreign key aqui
		}
		
		// Add DbSet de todas classes que representam dados do BD
		public DbSet<AppConfig> AppConfigs { get; set; }
	}
}

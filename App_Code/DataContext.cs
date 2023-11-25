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
	
	public class NewsMedia
    {
        public int Id { get; set; }
        public int Base64 { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type{ get; set; }
    }
    
    public class News
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Text { get; set; }
        public int Media_Id { get; set; }
        public int Is_Approved { get; set; }
        public int Fb_PostId { get; set; }
        public int Insta_PostId { get; set; }
        public string Date_Created { get; set; }
        public string Date_Posted { get; set; }
        public string Date_Schedule { get; set; }
    }
    
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Attempts { get; set; }
        public int Is_Admin { get; set; }
        public int Is_Commenter { get; set; }
        public int Is_Reviewer { get; set; }
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
		public DbSet<News> Newss { get; set; }
		public DbSet<NewsMedia> NewsMedias { get; set; }
		public DbSet<User> Users { get; set; }
	}
}

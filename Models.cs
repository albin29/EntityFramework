namespace EFIntro;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class User
{
    [Key]
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int? PostId { get; set; }
    public Post Post { get; set; }
}
public class Post
{
    [Key]
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? PublishedOn { get; set; }
    public int? BlogId { get; set; }
    [ForeignKey("BlogId")]
    public Blog Blog { get; set; }
    public int? UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
}
public class Blog
{
    [Key]
    public int? Id { get; set; }
    public int BlogId { get; set; }
    public string? Url { get; set; }
    public string? Name { get; set; }
    public int? PostId { get; set; }
    public Post Post { get; set; }
}
public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
    public string Dbpath { get; }
    public BloggingContext()
    {
        var folder = Environment.SpecialFolder.ApplicationData;
        string path = AppDomain.CurrentDomain.BaseDirectory;
        Dbpath = System.IO.Path.Join(path, "cs.forts-albin-duraku.db");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"Data Source = {Dbpath}");
}
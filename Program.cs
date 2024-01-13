using EFIntro;
using System;
using System.IO;
using System.Linq;
class Program
{
    static void Main()
    {
        Console.WriteLine("Hello");
        string[] linesBlogs = File.ReadAllLines("D:/cSharp/EFIntro/Blogs.csv");
        string[] linesUsers = File.ReadAllLines("D:/cSharp/EFIntro/Users.csv");
        string[] linesPosts = File.ReadAllLines("D:/cSharp/EFIntro/Posts.csv");
        using (var db = new BloggingContext())
        {
            foreach (string line in linesBlogs)
            {
                string[] blogLine = line.Split(',');
                int blogId = int.Parse(blogLine[0]);
                string url = blogLine[1];
                string name = blogLine[2];
                int postid = int.Parse(blogLine[3]);
                db.Blogs.Add(new Blog { BlogId = blogId, Url = url, Name = name, PostId = postid });
            }
            foreach (string line in linesUsers)
            {
                string[] userLine = line.Split(',');
                int userId = int.Parse(userLine[0]);
                string username = userLine[1];
                string password = userLine[2];
                int postId = int.Parse(userLine[3]);
                db.Users.Add(new User { Id = userId, Username = username, Password = password, PostId = postId });
            }
            foreach (string line in linesPosts)
            {
                string[] postLine = line.Split(',');
                int postId = int.Parse(postLine[0]);
                string title = postLine[1];
                string content = postLine[2];
                string published = postLine[3];
                int blogId = int.Parse(postLine[4]);
                int userId = int.Parse(postLine[5]);
                db.Posts.Add(new Post { Id = postId, Title = title, Content = content, PublishedOn = published, BlogId = blogId, UserId = userId });
            }
            db.SaveChanges();
            var blogs = db.Blogs.ToList();
            Console.WriteLine("Blogs:");
            foreach (var blog in blogs)
            {
                Console.WriteLine($"BlogId: {blog.Id}, Url: {blog.Url}, Name: {blog.Name}");
            }
            var users = db.Users.ToList();
            Console.WriteLine("\nUsers:");
            foreach (var user in users)
            {
                Console.WriteLine($"UserId: {user.Id}, Username: {user.Username}, Password: {user.Password}, PostId: {user.PostId}");
            }
            var posts = db.Posts.ToList();
            Console.WriteLine("\nPosts:");
            foreach (var post in posts)
            {
                Console.WriteLine($"PostId: {post.Id}, Title: {post.Title}, Content: {post.Content}, PublishedOn: {post.PublishedOn}, BlogId: {post.BlogId}, UserId: {post.UserId}");
            }
        }
    }
}

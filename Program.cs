using EFIntro;
using System;
using System.IO;
using System.Linq;
class Program
{
    static void Main()
    {
        Console.WriteLine("Hello");
        string[] linesBlogs = File.ReadAllLines("../../../Blogs.csv");

        string[] linesUsers = File.ReadAllLines("../../../Users.csv");
        string[] linesPosts = File.ReadAllLines("../../../Posts.csv");
        using (var db = new BloggingContext())
        {
            List<int> Blogid1 = new List<int>();
            List<int> Blogid2 = new List<int>();

            foreach (string line in linesBlogs)
            {
                string[] blogLine = line.Split(',');
                int Posts = int.Parse(blogLine[3]);
                int blogid = int.Parse(blogLine[0]);
                if (blogid == 1)
                {
                    Blogid1.Add(Posts);
                }
                if (blogid == 2)
                {
                    Blogid2.Add(Posts);
                }
            }

            string test = string.Empty;
            int count = 0;
            foreach (string line in linesBlogs)
            {
                string[] blogLine = line.Split(',');
                string url = blogLine[1];
                string name = blogLine[2];
                int postid = int.Parse(blogLine[3]);

                if (test != url)
                {

                    if (count == 0)
                    {

                        db.Blogs.Add(new Blog { Url = url, Name = name, Posts = Blogid1 });
                        count++;
                    }

                    else
                    {
                        db.Blogs.Add(new Blog { Url = url, Name = name, Posts = Blogid2 });
                        break;

                    }
                }
                test = url;
            }

            List<int> Userid1 = new List<int>();
            List<int> Userid2 = new List<int>();

            foreach (string line in linesUsers)
            {
                string[] userLine = line.Split(',');
                int Posts = int.Parse(userLine[3]);
                int userid = int.Parse(userLine[0]);
                if (userid == 1)
                {
                    Userid1.Add(Posts);
                }
                if (userid == 2)
                {
                    Userid2.Add(Posts);
                }

            }
            string test1 = string.Empty;
            int count1 = 0;
            foreach (string line in linesUsers)
            {
                string[] userLine = line.Split(',');
                int userId = int.Parse(userLine[0]);
                string username = userLine[1];
                string password = userLine[2];
                int postId = int.Parse(userLine[3]);
                if (test1 != username)
                {
                    if (count1 == 0)
                    {
                        db.Users.Add(new User { Id = userId, Username = username, Password = password, Posts = Userid1 });
                        count1++;
                    }
                    else
                    {
                        db.Users.Add(new User { Id = userId, Username = username, Password = password, Posts = Userid2 });
                        break;
                    }
                }
                test1 = username;
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
                Console.WriteLine($"UserId: {user.Id}, Username: {user.Username}, Password: {user.Password}");
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

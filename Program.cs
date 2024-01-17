using EFIntro;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Net;
using var db = new BloggingContext();
string[] usersfile = File.ReadAllLines("../../../Users.csv");
string[] blogsfile = File.ReadAllLines("../../../Blogs.csv");
string[] postsfile = File.ReadAllLines("../../../Posts.csv");

Console.WriteLine($"SQLite DB Located at: {db.DbPath}\n");

foreach (var line in usersfile)
{
    string[] userTable = line.Split(",");
    int userId = int.Parse(userTable[0]);
    string username = userTable[1];
    string password = userTable[2];
    User? userDb = db.Users?.Find(userId);
    if (userDb != null)
    {
        continue;
    }
    db.Add(new User { Id= userId, Username = username, Password = password });
}
foreach (var line in blogsfile)
{
    string[] blogTable = line.Split(",");
    int blogId = int.Parse(blogTable[0]);
    string url = blogTable[1];
    string name = blogTable[2];
    Blog? blogDb = db.Blogs?.Find(blogId);
    if (blogDb != null)
    {
        continue;
    }
    db.Add(new Blog { Id = blogId, Url = url, Name = name });
}

foreach (var line in postsfile)
{
    string[] postTable = line.Split(",");
    int id = int.Parse(postTable[0]);
    string title = postTable[1];
    string content = postTable[2];
    DateOnly date = DateOnly.Parse(postTable[3]);
    int BlogId = int.Parse(postTable[4]);
    int UserId = int.Parse(postTable[5]);
    db.Add(new Post { Title = title, Content = content, PublishedOn = date, BlogId = BlogId, UserId = UserId });
}

db.SaveChanges();

Console.WriteLine("DB:");
Console.WriteLine("├─ Users");
foreach (var user in db.Users)
{
    Console.WriteLine($"│  └─ User ID: {user.Id}, Username: {user.Username}, Password: {user.Password}");
    foreach (var post in user.Posts)
    {
        Console.WriteLine($"│     └─ Post ID: {post.Id}, Title: {post.Title}, Content: {post.Content}, Published On: {post.PublishedOn}");
    }
}
Console.WriteLine("├─ Blogs");
foreach (var blog in db.Blogs)
{
    Console.WriteLine($"│  └─ Blog ID: {blog.Id}, URL: {blog.Url}, Name: {blog.Name}");
    foreach (var post in blog.Posts)
    {
        Console.WriteLine($"│     └─ Post ID: {post.Id}, Title: {post.Title}, Content: {post.Content}, Published On: {post.PublishedOn}");
    }
}
Console.WriteLine("├─ Posts");
foreach (var post in db.Posts)
{
    Console.WriteLine($"│  └─ Post ID: {post.Id}, Title: {post.Title}, Content: {post.Content}, Published On: {post.PublishedOn}");
}

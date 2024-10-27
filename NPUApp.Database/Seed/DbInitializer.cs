using NPUApp.Database.Context;
using NPUApp.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.Database.Seed
{
    public static class DbInitializer
    {
        public static void Initialize(NpuAppDbContext context) 
        {
            context.Database.EnsureCreated();

            var parts = new Part[]
            {
                new() { PartNumber = 26047, FriendlyName = "Sus?" },
                new() { PartNumber = 78329, FriendlyName = "1x5 plate"},
            };

            context.Parts.AddRange(parts);

            var users = new User[]
            {
                new() {UserName = "User1"},
                new() {UserName = "User2"}
            };

            context.Users.AddRange(users);

            var posts = new NpuPost[]
            {
                new() { Parts = new List<Part>{ parts[0], parts[1] }, PictureUrl = "https://picsum.photos/200", User = users[0], CreatedOn = DateTime.UtcNow, Title = "My first post!"  },
                new() { Parts = new List<Part>{ parts[0] }, PictureUrl = "https://picsum.photos/200", User = users[1], CreatedOn = DateTime.UtcNow, Title = "Hej!"  }
            };

            context.NpuPosts.AddRange(posts);

            var ratings = new Rating[]
            {
                new() {Post = posts[0], User = users[1], UniquenessScore =3, CreativityScore = 4},
                new() {Post = posts[1], User = users[1], UniquenessScore =5, CreativityScore = 5},
                new() {Post = posts[0], User = users[0], UniquenessScore =5, CreativityScore = 5},
                new() {Post = posts[1], User = users[0], UniquenessScore =4, CreativityScore = 2},
            };

            context.Ratings.AddRange(ratings);

            context.SaveChanges();
        }
    }
}

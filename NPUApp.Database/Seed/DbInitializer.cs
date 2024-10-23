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
                new() {Username = "User1"},
                new() {Username = "User2"}
            };

            context.Users.AddRange(users);

            var pictures = new Picture[]
            {
                new() { Identifier = Guid.NewGuid()},
                new() { Identifier = Guid.NewGuid()}
            };

            context.Pictures.AddRange(pictures);

            var posts = new NpuPost[]
            {
                new() { Parts = new List<Part>{ parts[0], parts[1] }, Picture = pictures[0], User = users[0]  },
                new() { Parts = new List<Part>{ parts[0], parts[1] }, Picture = pictures[1], User = users[1]  }
            };

            context.NpuPosts.AddRange(posts);

            context.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlChanger.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //bool exists = (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();
            //if(exists)
            //{
            //    Database.EnsureDeleted();
            //}
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(new User { Login = "pupkinV", Password = "CustomPass1", Id = 1 },
                new User { Login = "PetrovK", Password = "CustomPass2", Id = 2 },
                new User { Login = "PavelM", Password = "CustomPass3", Id = 3 },
                new User { Login = "KonradA", Password = "CustomPass4", Id = 4 });

            builder.Entity<Url>().HasData(new Url {UrlOriginal = new Uri("https://www.songsterr.com/a/wsa/pt-adamczyk-olga-jankowska-cyberpunk-2077-never-fade-away-samurai-cover-guitar-solo-tab-s476473"), UrlOriginalHost = "", UrlOriginalPath = "", UrlModded = "https://localhost:44367/test1", UrlModdedPath = "test1", Id = 1, UserId = 1 });
        }
    }
}

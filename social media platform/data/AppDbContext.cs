using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using social_media_platform.models;

namespace social_media_platform.data
{
    internal class AppDbContext : DbContext
    {
        public string Connection { get; }
        public DbSet<Post> posts { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<ReactLog> reactLogs { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<FollowedUser> followedUsers { get; set; }
        public DbSet<React> Reacts { get; set; }


        public AppDbContext()
        {
            Connection = "Server=localhost;Database=SocialMediaDb;Integrated Security=true;TrustServerCertificate=True;";
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=SocialMediaDb;Integrated Security=true;TrustServerCertificate=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasMany(p => p.Comments)
                .WithOne(c => c.Post).HasForeignKey(c => c.PostId);
            modelBuilder.Entity<Post>().HasMany(p => p.ReactLogs)
                .WithOne(r => r.Post).HasForeignKey(r => r.PostId);
            modelBuilder.Entity<React>().HasMany(r => r.ReactLogs)
                .WithOne(rl => rl.React).HasForeignKey(rl => rl.ReactId);
            modelBuilder.Entity<FollowedUser>()
                .HasKey(fu => new { fu.FollowerId, fu.FollowedId });
            modelBuilder.Entity<FollowedUser>()
                .HasOne(fu => fu.Follower)
                .WithMany(u => u.FollowedUsers)
                .HasForeignKey(fu => fu.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FollowedUser>()
                .HasOne(fu => fu.Followed)
                .WithMany(u => u.Followers)
                .HasForeignKey(fu => fu.FollowedId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().Property(u=>u.UserId).ValueGeneratedOnAdd();

        }
        
    }
}

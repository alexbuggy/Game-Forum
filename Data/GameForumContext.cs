using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GameForum.Models;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;


namespace GameForum.Data
{
    public class GameForumContext : IdentityDbContext<User>
    {
        public GameForumContext(DbContextOptions<GameForumContext> options)
            : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<FavoriteGame> FavoriteGames { get; set; }
        public DbSet<GameRequest> GameRequests { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<GameGameCategory> GameGameCategories { get; set; }
        public DbSet<GameRequestCategory> GameRequestCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FavoriteGame>()
                .HasKey(fg => new { fg.UserId, fg.GameId });

            builder.Entity<FavoriteGame>()
                .HasOne(fg => fg.User)
                .WithMany(u => u.FavoriteGames)
                .HasForeignKey(fg => fg.UserId);

            builder.Entity<FavoriteGame>()
                .HasOne(fg => fg.Game)
                .WithMany(g => g.FavoritedBy)
                .HasForeignKey(fg => fg.GameId);

            builder.Entity<Reply>()
                .HasOne(r => r.ParentPost)
                .WithMany(p => p.Replies)
                .HasForeignKey(r => r.ParentPostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Post>()
                .HasDiscriminator<string>("PostType")
                .HasValue<Post>("Post")
                .HasValue<Discussion>("Discussion")
                .HasValue<Reply>("Reply")
                .HasValue<Review>("Review");

            builder.Entity<Vote>()
                .HasOne(v => v.Post)
                .WithMany(p => p.Votes)
                .HasForeignKey(v => v.PostId)
                .IsRequired() 
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vote>()
                .HasOne(v => v.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Review>()
                .HasOne(p => p.Game)
                .WithMany(g => g.Reviews) 
                .HasForeignKey(p => p.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Discussion>()
                .HasOne(d => d.Game)
                .WithMany(g => g.Discussions)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<GameRequest>()
                .HasOne(gr => gr.RequestedByUser)
                .WithMany(u => u.GameRequests)
                .HasForeignKey(gr => gr.RequestedByUserId)
                .OnDelete(DeleteBehavior.Restrict);




            builder.Entity<IdentityUserToken<string>>(b =>
            {
                b.Property(t => t.Name).HasMaxLength(128); // Default max length for Identity tokens
            });

            builder.Entity<GameGameCategory>()
                .HasKey(gc => new { gc.GameId, gc.Category });

            builder.Entity<GameGameCategory>()
                .HasOne(gc => gc.Game)
                .WithMany(g => g.GameCategories)
                .HasForeignKey(gc => gc.GameId);

            builder.Entity<GameRequestCategory>()
                .HasKey(gc => new { gc.GameRequestId, gc.Category });

            builder.Entity<GameRequestCategory>()
                .HasOne(gc => gc.GameRequest)
                .WithMany(gr => gr.GameCategories)
                .HasForeignKey(gc => gc.GameRequestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
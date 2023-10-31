using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Contexts;

public class ThinkContext:IdentityDbContext
{
    public ThinkContext()
    {
        
    }
    public ThinkContext(DbContextOptions options):base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=localhost,1433;Database=Think;User Id=SA;Password=Password1!;TrustServerCertificate=True");
        optionsBuilder.EnableSensitiveDataLogging();
    }

    public DbSet<User> Users { get; set; }
    
    public DbSet<Post> Posts { get; set; }
    
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Friend> Friends { get; set; }
    
    public DbSet<UserConnectionId> UserConnectionIds { get; set; }
    
    public DbSet<FriendRequest> FriendRequests { get; set; }
    public DbSet<Comment> Comments { get; set; }
    
    public DbSet<Message> Messages { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<PostLikedUser> PostLikedUsers { get; set; }
    public DbSet<CommentLikedUser> CommentLikedUsers { get; set; }
    public DbSet<CommentOfPost> CommentOfPost { get; set; }
}
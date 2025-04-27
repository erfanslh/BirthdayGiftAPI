using BirthdayApp.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BirthdayApp
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
                
        }
        public DbSet<WishList> Wishlists { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            // WishList Table
            model.Entity<WishList>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.wishLists)
                .HasForeignKey(x => x.OwnerId);


            // Friendship Table
            model.Entity<Friendship>()
                .HasOne(x => x.Requester)
                .WithMany(x => x.SentFriendRequests)
                .HasForeignKey(x => x.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            model.Entity<Friendship>()
                .HasOne(x => x.Receiver)
                .WithMany(x => x.ReceivedFriendRequests)
                .HasForeignKey(x => x.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

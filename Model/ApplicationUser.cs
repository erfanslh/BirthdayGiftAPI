using Microsoft.AspNetCore.Identity;

namespace BirthdayApp.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        public ICollection<WishList> wishLists = new List<WishList>();

        public ICollection<Friendship> SentFriendRequests { get; set; } = new List<Friendship>();
        public ICollection<Friendship> ReceivedFriendRequests { get; set; } = new List<Friendship>();

    }
}

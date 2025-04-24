using Microsoft.AspNetCore.Identity;

namespace BirthdayApp.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        public ICollection<WishList> wishLists = new List<WishList>();
    }
}

using BirthdayApp.Model;

namespace BirthdayApp.DTO
{
    public class WishListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Link { get; set; }
        public bool IsBooked { get; set; }
        public string BookedByUser { get; set; } = null!;
        public string OwnerId { get; set; } = null!;
    }
}

namespace BirthdayApp.DTO
{
    public class UpdateWishListDTO
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Link { get; set; }
        public bool IsBooked { get; set; }
    }
}

using BirthdayApp.Model;

namespace BirthdayApp.DTO
{
    public class FriendshipDTO
    {
        public int Id { get; set; }

        public string RequesterId { get; set; } = null!;
        public string? RequesterName { get; set; }

        public string ReceiverId { get; set; } = null!;
        public string? ReceiverName { get; set; }

        public FriendshipStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

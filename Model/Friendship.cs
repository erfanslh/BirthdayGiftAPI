namespace BirthdayApp.Model
{
    public class Friendship
    {
        // id of the FriendRequest
        public int Id { get; set; }

        // id of the user who sent the request
        public string RequesterId { get; set; } = null!;
        public ApplicationUser Requester { get; set; } = null!;

        // id of the user who received the request
        public string ReceiverId { get; set; } = null!;
        public ApplicationUser Receiver { get; set; } = null!;

        // status of the request
        public FriendshipStatus Status { get; set; } 

        // date of FriendRequest
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }

    public enum FriendshipStatus
    {
        Pending,
        Accepted,
        Rejected,
        Blocked
    }
}

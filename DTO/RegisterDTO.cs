namespace BirthdayApp.DTO
{
    public class RegisterDTO
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public required DateTime BirthDate { get; set; } 
    }
}

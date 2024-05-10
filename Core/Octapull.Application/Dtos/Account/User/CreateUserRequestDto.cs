namespace Octapull.Application.Dtos.Account.User
{
    public class CreateUserRequestDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageId { get; set; }
    }
}

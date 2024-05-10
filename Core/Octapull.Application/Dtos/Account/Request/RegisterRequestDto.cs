using Microsoft.AspNetCore.Http;

namespace Octapull.Application.Dtos.Account.Request
{
    // ad, soyad, email, telefon, şifre girerek kayıt olmalarını ve profil resmi yüklemelerini sağlayacak bir sayfa
    public class RegisterRequestDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile Image { get; set; }
    }
}

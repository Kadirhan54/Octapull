using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octapull.Application.Dtos
{
    // ad, soyad, email, telefon, şifre girerek kayıt olmalarını ve profil resmi yüklemelerini sağlayacak bir sayfa
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }
}

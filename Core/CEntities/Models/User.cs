using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CEntities.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string UserTypeId { get; set; } // A -> Advisor --- M -> Management --- T -> Teacher --- P -> Parent
        public int SchoolId { get; set; } // her okul kendi sitesinden giriş yapar (app.xanaokulu.com) 
        public int Position { get; set; } // ünvan
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; } // sisteme giriş için kullanılır
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; } // işten ayrılınca yada öğrenci devam etmiyorsa false olur
        public DateTime RegistrationDate { get; set; } // auto
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;

namespace OnlineShop.Db.Models
{
    public class User : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
    }
}

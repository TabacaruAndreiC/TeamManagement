using System.ComponentModel.DataAnnotations;
using TeamManagement.Data.Enums;

namespace TeamManagement.Services.Dtos
{
        public class UserRegisterDto
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            public Role Role { get; set; }
        }
}

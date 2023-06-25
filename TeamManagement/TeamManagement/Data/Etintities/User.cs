using System.Data;
using TeamManagement.Data.Enums;

namespace TeamManagement.Data.Etintities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Role? Role { get; set; } = null;
    }
}

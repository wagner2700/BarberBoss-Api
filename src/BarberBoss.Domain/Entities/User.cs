using BarberBoss.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace BarberBoss.Domain.Entities
{
    [Table("User")]
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid UserIdentifier { get; set; }
        public Roles Role { get; set; } =  Roles.TEAM_MEMBER;
       
    }
}

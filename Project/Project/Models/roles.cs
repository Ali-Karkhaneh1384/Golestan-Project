using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public enum Roles
    {
        Admin , Student , Instructor
    }
    public class roles
    {
        [Key]
        public int Id { get; set; }
        public Roles name { get; set; }

        public ICollection<user_roles> user_roles { get; set; }

    }
}

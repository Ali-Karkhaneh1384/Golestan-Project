using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Project.Models
{
    [PrimaryKey(nameof(UserId), nameof(RoleId))]
    public class user_roles
    {
        //public int user_id { get; set; }
        //public int role_id { get; set; }

        
        public int UserId { get; set; }
        
        public int RoleId { get; set; }

        [ForeignKey("UserId")]
        public virtual users users { get; set; }

        [ForeignKey("RoleId")]
        public virtual roles roles { get; set; }


    }
}

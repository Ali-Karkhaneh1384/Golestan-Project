using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created_at {  get; set; }
        [Required(ErrorMessage ="نام نمیتواند خالی باشد")]
        [Column(TypeName = "varchar (50)")]
        public string First_Name { get; set; }
        [Required(ErrorMessage = "نام خانوادگی نمیتواند خالی باشد")]
        [Column(TypeName = "varchar (50)")]
        public string Last_Name { get; set; }
        [Required(ErrorMessage = "ایمیل نمیتواند خالی باشد")]
        [Column(TypeName = "varchar (50)")]
        public string Email {  get; set; }
        [Required(ErrorMessage = "گذرواژه نمیتواند خالی باشد")]
        [DataType(DataType.Password)]
        public string Hashed_Password {  get; set; }
        

        public ICollection<user_roles>? user_roles { get; set; }
        public ICollection<instructors>? Instructors { get; set; }
        public ICollection<students>? students { get; set; }

    }
}

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
        [Required(ErrorMessage ="وارد کردن نام الزامی است")]
        [Column(TypeName = "nvarchar (50)")]
        public string First_Name { get; set; }
        [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامی است")]
        [Column(TypeName = "nvarchar (50)")]
        public string Last_Name { get; set; }
        [Required(ErrorMessage = "وارد کردن ایمیل دانشجو الزامی است")]
        [Column(TypeName = "nvarchar (50)")]
        public string Email {  get; set; }
        [Required(ErrorMessage = "وارد کردن گذرواژه الزامی است")]
        [DataType(DataType.Password)]
        public string Hashed_Password {  get; set; }
        

        public ICollection<user_roles>? user_roles { get; set; }
        public ICollection<instructors>? Instructors { get; set; }
        public ICollection<students>? students { get; set; }

    }
}

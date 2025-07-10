using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class students
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int student_id { get; set; }
        public int user_id { get; set; }
        [Required(ErrorMessage ="تاریخ ثبت نام نمیتواند خالی باشد")]
        public DateTime enrollment_date { get; set; }
        [ForeignKey("user_id")]
        public users users { get; set; }
        public ICollection<takes> takes { get; set; }
    }
}

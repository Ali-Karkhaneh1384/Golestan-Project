using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Project.Models
{
    public class instructors
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int instructor_id {  get; set; }
        public int user_id {  get; set; }
        [Column(TypeName ="decimal(10,2)")]
        [Required(ErrorMessage ="حقوق نمیتواند خالی باشد ") ]
        public decimal salary {  get; set; }
        [Required(ErrorMessage = "تاریخ استخدام نمیتواند خالی باشد ")]
        public DateTime hire_date { get; set; }



        [ForeignKey("user_id")]
        public users? user { get; set; }
        public ICollection<teach>? teaches { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class classrooms
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "ساختمان نمبتواند خالی باشد")]
        [Column(TypeName = "varchar(225)")]
        public string building { get; set; }
        [Required(ErrorMessage = "شماره کلاس نمبتواند خالی باشد")]
        public int room_number { get; set; }
        [Required(ErrorMessage = "ظرفیت نمبتواند خالی باشد")]
        public int capacity { get; set; }
        public ICollection<sections> sections { get; set; }
    }
}

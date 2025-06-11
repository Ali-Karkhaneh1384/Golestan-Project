using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class time_slots
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "روز نمیتواند خالی باشد")]
        [Column(TypeName ="varchar(225)")]
        public string day { get; set; }
        [Required(ErrorMessage = "زمان شروع نمیتواند خالی باشد")]
        public DateTime start_time { get; set; }
        [Required(ErrorMessage = "زمان پایان نمیتواند خالی باشد")]
        public DateTime end_time { get; set; }
        public ICollection<sections> sections { get; set; }
    }
}

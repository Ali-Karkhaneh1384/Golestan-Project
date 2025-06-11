using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class courses
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان نمیتواند خالی باشد")]
        [Column(TypeName = "varchar(225)")]
        public string Title { get; set; }
        [Required(ErrorMessage = "کد نمیتواند خالی باشد")]
        [Column(TypeName = "varchar(225)")]
        public string code {  get; set; }
        [Required(ErrorMessage = "واحد نمیتواند خالی باشد")]
        [Column(TypeName = "varchar(225)")]
        public string unit {  get; set; }
        [Required(ErrorMessage = "توضیحات نمیتواند خالی باشد")]
        [Column(TypeName = "varchar(225)")]
        public string description { get; set; }
        [Required(ErrorMessage = "تاریخ امتحان نمیتواند خالی باشد")]
        
        public DateTime final_exam_date {  get; set; }
        public sections sections { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class courses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "وارد کردن عنوان درس الزامی است")]
        [Column(TypeName = "varchar(225)")]
        public string Title { get; set; }
        [Required(ErrorMessage = "وارد کردن کد درس الزامی است")]
        [Column(TypeName = "varchar(225)")]
        public string code {  get; set; }
        [Required(ErrorMessage = "وارد کردن تعداد واحد درس الزامی است")]
        [Range(0, 4, ErrorMessage ="تعداد واحد فقط باید بین 0 تا 4 باشد")]
        public int unit {  get; set; }
        [Required(ErrorMessage = "وارد کردن توضیحات الزامی است")]
        [Column(TypeName = "varchar(225)")]
        public string description { get; set; }
        public ICollection<sections>? sections { get; set; }

    }
}

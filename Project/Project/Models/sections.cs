using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class sections
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int course_id {  get; set; }
        [Required(ErrorMessage ="وارد کردن نیمسال تحصیلی الزامی است")]
        public int semester {  get; set; }
        [Required(ErrorMessage ="وارد کردن سال تحصیلی الزامی است")]
        public int year {  get; set; }
        public DateTime final_exam_date { get; set; }
        public int classroom_id {  get; set; }
        [NotMapped]
        [Required(ErrorMessage = "وارد کردن زمان کلاس ها الزامی است")]
        public List<int> TimeSlotIds { get; set; } = new List<int>();
        [ForeignKey("classroom_id")]
        public classrooms? classroom { get; set; }
        public ICollection<section_time>? section_Times { get; set; }
        [ForeignKey("course_id")]
        public courses? course {  get; set; }
        public teach? teach { get; set; }
        public ICollection<takes>? takes { get; set; }
    }
}

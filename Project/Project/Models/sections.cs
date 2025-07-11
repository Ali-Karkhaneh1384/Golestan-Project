using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class sections
    {
        [Key]
        public int Id { get; set; }
        public int course_id {  get; set; }
        [Required(ErrorMessage ="نیمسال نمیتواند خالی باشد")]
        public int semester {  get; set; }
        [Required(ErrorMessage ="سال تحصیلی نمیتواند خالی باشد")]
        public int year {  get; set; }
        public int classroom_id {  get; set; }
        public int time_slot_id {  get; set; }
        [ForeignKey("classroom_id")]
        public classrooms classroom { get; set; }
        [ForeignKey("time_slot_id")]
        public time_slots time_slot { get; set; }
        [ForeignKey("course_id")]
        public courses course {  get; set; }
        public teach teach { get; set; }
        public ICollection<takes> takes { get; set; }
    }
}

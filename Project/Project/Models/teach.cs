using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [PrimaryKey(nameof(instructor_id) , nameof(section_id))]
    public class teach
    {
        [Required (ErrorMessage ="استاد باید انتخاب شود")]
        public int instructor_id {  get; set; }
        [Required(ErrorMessage = "کلاس باید انتخاب شود")]
        public int section_id {  get; set; }

        [ForeignKey("instructor_id")]
        public instructors? instructor { get; set; }
        [ForeignKey("section_id")]
        public sections? section {  get; set; }
        


    }
}

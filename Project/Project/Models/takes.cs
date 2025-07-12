using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Project.Models
{
    [PrimaryKey(nameof(student_id), nameof(section_id))]
    public class takes
    {
        public int student_id { get; set; }
        public int section_id { get; set; }
        [Required(ErrorMessage ="نمره نمیتواند خالی باشد")]
        public int grade {  get; set; }
        [ForeignKey("student_id")]
        public students students { get; set; }
        [ForeignKey("section_id")]
        public sections sections { get; set; }


    }
}

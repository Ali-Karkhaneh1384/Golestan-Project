using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [PrimaryKey(nameof(section_id), nameof(time_slot_id))]
    public class section_time
    {
        public int section_id { get; set; }
        public int time_slot_id { get; set; }

        [ForeignKey("section_id")]
        [InverseProperty("section_Times")]
        public sections section { get; set; }

        [ForeignKey("time_slot_id")]
        [InverseProperty("section_Times")]
        public time_slots time_slot { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ShowtimeIntervalViewModel
    {
        public int Id { get; set; }
        public int ShowtimeId { get; set; }
        
        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        
        [Required]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }
        
        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}

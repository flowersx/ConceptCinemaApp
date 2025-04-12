using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ShowtimeViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Movie")]
        public int MovieId { get; set; }
        
        [Required]
        [Display(Name = "Cinema Hall")]
        public int CinemaHallId { get; set; }
        
        [Required]
        [Display(Name = "Duration (minutes)")]
        [Range(10, 360, ErrorMessage = "Duration must be between 10 and 360 minutes")]
        public int DurationMinutes { get; set; }
        
        [Required]
        [Display(Name = "Base Price")]
        [DataType(DataType.Currency)]
        [Range(0.01, 1000, ErrorMessage = "Price must be between 0.01 and 1000")]
        public decimal BasePrice { get; set; }
        
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        // Days of week for recurring showtimes
        [Display(Name = "Monday")]
        public bool IsMonday { get; set; }
        
        [Display(Name = "Tuesday")]
        public bool IsTuesday { get; set; }
        
        [Display(Name = "Wednesday")]
        public bool IsWednesday { get; set; }
        
        [Display(Name = "Thursday")]
        public bool IsThursday { get; set; }
        
        [Display(Name = "Friday")]
        public bool IsFriday { get; set; }
        
        [Display(Name = "Saturday")]
        public bool IsSaturday { get; set; }
        
        [Display(Name = "Sunday")]
        public bool IsSunday { get; set; }
        
        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
        
        // Navigation properties (for display purposes)
        public string MovieTitle { get; set; }
        public string CinemaHallName { get; set; }
        
        // Time slots for this showtime
        public List<ShowtimeIntervalViewModel> Intervals { get; set; } = new List<ShowtimeIntervalViewModel>();
    }
}

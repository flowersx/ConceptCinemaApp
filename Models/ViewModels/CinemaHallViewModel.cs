using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CinemaHallViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "The name cannot exceed 100 characters.")]
        public string Name { get; set; }
        public int TotalSeats { get; set; }
        
        [Required]
        [Range(1, 20, ErrorMessage = "Rows must be between 1 and 20.")]
        public int Rows { get; set; }
        [Required]
        [Range(1, 20, ErrorMessage = "Rows must be between 1 and 20.")]
        public int SeatsPerRow { get; set; }

        public List<SeatViewModel> Seats { get; set; } = new();
    }
}

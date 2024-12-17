using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CinemaHallViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Display-only
        public int TotalSeats { get; set; }

        // Fields for dynamic seat generation
        public int Rows { get; set; }
        public int SeatsPerRow { get; set; }

        public List<SeatViewModel> Seats { get; set; } = new();
    }
}

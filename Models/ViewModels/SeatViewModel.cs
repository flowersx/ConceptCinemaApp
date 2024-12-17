using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class SeatViewModel
    {
        public string Row { get; set; }
        public int Number { get; set; }
        public bool IsAvailable { get; set; }
    }
}

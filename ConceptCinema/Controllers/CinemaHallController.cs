namespace WebApplication1.Controllers
{
    using AutoMapper;
    using DataAccess;
    using global::Models.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Services.IServices;

    public class CinemaHallController : Controller
    {
        private readonly ICinemaHallService _cinemaHallService;
        private readonly IMapper _mapper;

        public CinemaHallController(ICinemaHallService cinemaHallService, IMapper mapper)
        {
            _cinemaHallService = cinemaHallService;
            _mapper = mapper;
        }

        // 1. INDEX ACTION - Display all cinema halls
        public async Task<IActionResult> Index()
        {
            var cinemaHalls = await _cinemaHallService.GetAllAsync();
            var viewModel = _mapper.Map<List<CinemaHallViewModel>>(cinemaHalls);

            return View(viewModel);
        }

        // 2. CREATE ACTION (GET) - Return form for creation
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CinemaHallViewModel());
        }

        // 3. CREATE ACTION (POST) - Handle form submission
        [HttpPost]
        public async Task<IActionResult> Create(CinemaHallViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            // Map ViewModel to Domain Model
            var cinemaHall = _mapper.Map<CinemaHall>(viewModel);

            // Dynamically create seats based on rows and seats per row
            var seats = new List<Seat>();
            for (int i = 0; i < viewModel.Rows; i++)
            {
                for (int j = 0; j < viewModel.SeatsPerRow; j++)
                {
                    seats.Add(new Seat
                    {
                        Row = ((char)('A' + i)).ToString(),
                        Number = j + 1,              
                        IsAvailable = true
                    });
                }
            }

            cinemaHall.Seats = seats;
            cinemaHall.TotalSeats = seats.Count;
            var cinema = _mapper.Map<CinemaHallViewModel>(cinemaHall);
            await _cinemaHallService.AddAsync(cinema);

            return RedirectToAction("Index");
        }

        // 4. EDIT ACTION (GET) - Return form with existing data
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaHall = await _cinemaHallService.GetByIdAsync(id);
            if (cinemaHall == null)
                return NotFound();

            var viewModel = _mapper.Map<CinemaHallViewModel>(cinemaHall);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CinemaHallViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var cinemaHall = await _cinemaHallService.GetByIdAsync(viewModel.Id);
            if (cinemaHall == null)
                return NotFound();

            // Update fields
            cinemaHall.Name = viewModel.Name;

            // Optionally regenerate seats if rows or seats per row are updated
            var newSeats = new List<Seat>();
            for (int i = 0; i < viewModel.Rows; i++)
            {
                for (int j = 0; j < viewModel.SeatsPerRow; j++)
                {
                    newSeats.Add(new Seat
                    {
                        Row = ((char)('A' + i)).ToString(),
                        Number = j + 1,
                        IsAvailable = true
                    });
                }
            }

            cinemaHall.Seats = _mapper.Map<List<SeatViewModel>>(newSeats);
            cinemaHall.TotalSeats = newSeats.Count;

            await _cinemaHallService.UpdateAsync(cinemaHall);

            return RedirectToAction("Index");
        }
    }
}


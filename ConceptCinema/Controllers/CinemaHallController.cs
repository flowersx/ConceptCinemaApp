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
        
        public async Task<IActionResult> Index()
        {
            var cinemaHalls = await _cinemaHallService.GetAllAsync();
            var viewModel = _mapper.Map<List<CinemaHallViewModel>>(cinemaHalls);

            return View(viewModel);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CinemaHallViewModel());
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CinemaHallViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            
            var cinemaHall = _mapper.Map<CinemaHall>(viewModel);
            
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
            
            cinemaHall.Name = viewModel.Name;

            await _cinemaHallService.UpdateAsync(cinemaHall);

            return RedirectToAction("Index");
        }
    }
}


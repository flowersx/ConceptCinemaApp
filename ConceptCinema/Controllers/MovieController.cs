using AutoMapper;
using Constants.Authentification;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using Services.IServices;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            var viewModel = _mapper.Map<List<MovieViewModel>>(movies);
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create()
        {
            return View(new MovieViewModel());
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Create(MovieViewModel viewModel, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    viewModel.ImageBase64 = Convert.ToBase64String(fileBytes);
                    ModelState.Remove("ImageBase64");
                }
            }
            else
            {
                viewModel.ImageBase64 = string.Empty;
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var movie = _mapper.Map<Movie>(viewModel);
            await _movieService.AddMovieAsync(movie);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
                return NotFound();

            var viewModel = _mapper.Map<MovieViewModel>(movie);
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> ToggleShowInMainPage([FromBody] ToggleShowInMainPageRequest request)
        {
            var movie = await _movieService.GetMovieByIdAsync(request.MovieId);
            if (movie == null)
            {
                return NotFound();
            }

            movie.ShowInMainPage = request.ShowInMainPage;
            await _movieService.UpdateMovieAsync(movie);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(MovieViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var movie = await _movieService.GetMovieByIdAsync(viewModel.Id);
            if (movie == null)
                return NotFound();

            movie.Title = viewModel.Title;
            movie.Description = viewModel.Description;
            movie.ReleaseDate = viewModel.ReleaseDate;
            movie.ImageBase64 = viewModel.ImageBase64;

            await _movieService.UpdateMovieAsync(movie);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
                return NotFound();

            var viewModel = _mapper.Map<MovieViewModel>(movie);
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
                return NotFound();

            await _movieService.DeleteMovieAsync(id);
            return RedirectToAction("Index");
        }
    }
}
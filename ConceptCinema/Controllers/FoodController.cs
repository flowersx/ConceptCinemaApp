using AutoMapper;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using Services.IServices;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly IMapper _mapper;

        public FoodController(IFoodService foodService, IMapper mapper)
        {
            _foodService = foodService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var foodItems = await _foodService.GetAllFoodAsync();
            var viewModel = _mapper.Map<List<FoodViewModel>>(foodItems);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new FoodViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(FoodViewModel viewModel, IFormFile imageFile)
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

            var food = _mapper.Map<FoodAndBeverage>(viewModel);
            await _foodService.AddFoodAsync(food);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var food = await _foodService.GetFoodByIdAsync(id);
            if (food == null)
                return NotFound();

            var viewModel = _mapper.Map<FoodViewModel>(food);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FoodViewModel viewModel, IFormFile imageFile)
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

            if (!ModelState.IsValid)
                return View(viewModel);

            var food = await _foodService.GetFoodByIdAsync(viewModel.Id);
            if (food == null)
                return NotFound();

            food.Name = viewModel.Name;
            food.Price = viewModel.Price;
            
            // Only update image if a new one was uploaded
            if (!string.IsNullOrEmpty(viewModel.ImageBase64))
            {
                food.ImageBase64 = viewModel.ImageBase64;
            }

            await _foodService.UpdateFoodAsync(food);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var food = await _foodService.GetFoodByIdAsync(id);
            if (food == null)
                return NotFound();

            await _foodService.DeleteFoodAsync(id);
            return RedirectToAction("Index");
        }
    }
}

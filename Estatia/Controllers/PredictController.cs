using Microsoft.AspNetCore.Mvc;
using Estatia.Models.ViewModels;
using Microsoft.ML.OnnxRuntime;         
using Microsoft.ML.OnnxRuntime.Tensors; 
using System.IO;                        
using Estatia.Services;

namespace Estatia.Controllers
{
    public class PredictController : Controller
    {
        private readonly PricePredictionService _predictionService;

        public PredictController(PricePredictionService predictionService)
        {
            _predictionService = predictionService;
        }


        public IActionResult Index()
        {
            return View(new PredictionViewModel());
        }

        [HttpPost]
        public IActionResult Index(PredictionViewModel model)
        {
            if (ModelState.IsValid)
            {
                float price = _predictionService.PredictPrice(model.SizeSqFt, model.CityCode, model.TypeCode);
                model.PredictedPrice = price;
            }
            return View(model);
        }
    }
}
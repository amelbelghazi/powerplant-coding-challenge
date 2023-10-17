using Microsoft.AspNetCore.Mvc;
using ProductionPlanAPI.Dtos;
using ProductionPlanAPI.Dtos.Request;

namespace ProductionPlanAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductionPlanController : ControllerBase
    {
        private readonly ILogger<ProductionPlanController> _logger;

        public ProductionPlanController(ILogger<ProductionPlanController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public CreateProductionPlanResponse Post(CreateProductionPlanRequest productionDto)
        {
            
        }
    }
}
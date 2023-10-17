using Microsoft.AspNetCore.Mvc;
using ProductionPlanAPI.Dtos;
using ProductionPlanAPI.Dtos.Request;
using ProductionPlanAPI.Services;

namespace ProductionPlanAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductionPlanController : ControllerBase
    {
        private readonly ILogger<ProductionPlanController> logger;
        private readonly IProductionPlanService productionPlanService;

        public ProductionPlanController(ILogger<ProductionPlanController> logger, IProductionPlanService productionPlanService)
        {
            this.logger = logger;
            this.productionPlanService = productionPlanService;
        }

        [HttpPost]
        public CreateProductionPlanResponse Post(CreateProductionPlanRequest productionDto)
        {
            
        }
    }
}
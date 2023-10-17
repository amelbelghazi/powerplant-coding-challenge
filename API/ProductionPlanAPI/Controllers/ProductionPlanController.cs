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
        public ActionResult<CreateProductionPlanResponse> Post(CreateProductionPlanRequest productionDto)
        {
            if(!ModelState.IsValid)
            {
                string errors = string.Join("\n", ModelState.Values);
                logger.LogError($"A validation error happened. More details below \n {errors}");
                throw new InvalidDataException(errors);
            }

            this.productionPlanService.SetupData(productionDto);
            CreateProductionPlanResponse response = this.productionPlanService.GenerateProductionPlan();
            return this.Ok(response);
        }
    }
}
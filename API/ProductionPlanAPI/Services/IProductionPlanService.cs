using ProductionPlanAPI.Dtos;
using ProductionPlanAPI.Dtos.Request;

namespace ProductionPlanAPI.Services
{
    public interface IProductionPlanService
    {
        CreateProductionPlanResponse GenerateProductionPlan();
        bool SetupData(CreateProductionPlanRequest productionDto);
    }
}

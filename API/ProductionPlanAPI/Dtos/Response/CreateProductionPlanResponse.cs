namespace ProductionPlanAPI.Dtos
{
    public partial class CreateProductionPlanResponse
    {
        public ICollection<PowerplantProduction>? PowerplantProductions { get; set; }
    }
}

namespace ProductionPlanAPI.Dtos.Request;

public partial class CreateProductionPlanRequest
{
    public int Load { get; set; }
    public FuelsDto? Fuels { get; set; }
    public List<PowerplantDto>? Powerplants { get; set; }
}

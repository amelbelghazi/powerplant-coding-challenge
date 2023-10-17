using Newtonsoft.Json;

namespace ProductionPlanAPI.Dtos
{
    public partial class CreateProductionPlanResponse
    {
        public class PowerplantProduction
        {
            public string Name { get; set; }

            [JsonProperty("p")]
            public double Production { get; set; }
        }
    }
}

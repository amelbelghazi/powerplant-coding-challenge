namespace ProductionPlanAPI.Dtos.Request;

using Newtonsoft.Json;

public class FuelsDto
{
    [JsonProperty("gas(euro/MWh)")]
    public double GasPrice { get; set; }

    [JsonProperty("kerosine(euro/MWh)")]
    public double KerosinePrice { get; set; }

    [JsonProperty("co2(euro/ton)")]
    public int Co2Price { get; set; }

    [JsonProperty("wind(%)")]
    public int Wind { get; set; }
}

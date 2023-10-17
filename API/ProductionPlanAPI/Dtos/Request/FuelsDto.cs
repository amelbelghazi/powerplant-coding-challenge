namespace ProductionPlanAPI.Dtos.Request;

using System.Text.Json.Serialization;

public class FuelsDto
{
    [JsonPropertyName("gas(euro/MWh)")]
    public double GasPrice { get; set; }

    [JsonPropertyName("kerosine(euro/MWh)")]
    public double KerosinePrice { get; set; }

    [JsonPropertyName("co2(euro/ton)")]
    public int Co2Price { get; set; }

    [JsonPropertyName("wind(%)")]
    public int Wind { get; set; }
}

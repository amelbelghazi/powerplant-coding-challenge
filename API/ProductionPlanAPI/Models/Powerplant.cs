using System.Security.AccessControl;

namespace ProductionPlanAPI.Models;

public abstract class Powerplant
{
    public string Name { get; init; }
    public Source Source { get; init; }
    public double MaxProduction { get; init; }

    public Powerplant(string name, Source source, double maxPoduction)
    {
        this.Name = name;
        this.Source = source;
        this.MaxProduction = maxPoduction;
    }
}

public class FossilePowerPlant : Powerplant
{
    public FossilePowerPlant(string name, Source source, double maxPoduction, double efficiency) 
        : base(name, source, maxPoduction)
    {
        this.Efficiency = efficiency;
    }

    public double Efficiency { get; init; }

    public double GetProductionCost(double sourcePrice)
    {
        return this.MaxProduction / this.Efficiency * sourcePrice;
    }

    public double GetProductionCost(double production, double sourcePrice)
    {
        return production / this.Efficiency * sourcePrice;
    }
}

public class GreenPowerplant : Powerplant
{
    public GreenPowerplant(string name, Source source, double maxPoduction)
        : base(name, source, maxPoduction)
    {
    }

    public double GetMaxProduction(double sourceScale)
    {
        return this.MaxProduction * sourceScale;
    }
}

public class PowerPlantWithEmission : FossilePowerPlant
{
    public PowerPlantWithEmission(string name, Source source, double maxPoduction, double efficiency, double minProduction, double co2Emission)
        : base(name, source, maxPoduction, efficiency)
    {
        this.MinProduction = minProduction;
        this.CO2Emission = co2Emission;
    }

    public double MinProduction { get; init; }
    public double CO2Emission { get; init; }

    public double GetProductionEmission(double emissionPrice)
    {
        return this.MaxProduction * CO2Emission * emissionPrice;
    }

    public double GetProductionEmission(double production, double emissionPrice)
    {
        return production * CO2Emission * emissionPrice;
    }
}

public abstract class Source
{
    public string Name { get; init; }
    public SourceType Type { get; init; }
    public enum SourceType
    {
        Green,
        Fossile
    }

    public Source(string name, SourceType type)
    {
        this.Name = name;
        this.Type = type;
    }
}

public class Fuel : Source
{
    public Fuel(string name, double cost) : base(name, SourceType.Fossile)
    {
        this.Cost = cost;
    }

    public double Cost { get; set; }
}

public class Flow : Source
{
    public Flow(string name, int scale) : base(name, SourceType.Green)
    {
        this.Scale = scale;
    }

    public int Scale { get; set; }
}
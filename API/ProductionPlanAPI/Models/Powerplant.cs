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

    public double GetProductionPrice(double sourcePrice)
    {
        return this.MaxProduction / this.Efficiency * sourcePrice;
    }

    public double GetProductionPrice(double production, double sourcePrice)
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
        return this.MaxProduction * sourceScale / 100;
    }
}

public class PowerPlantWithEmission : FossilePowerPlant
{
    public PowerPlantWithEmission(string name, Source source, double maxPoduction, double efficiency, double minProduction, double co2Emission, Emission emission)
        : base(name, source, maxPoduction, efficiency)
    {
        this.MinProduction = minProduction;
        this.EmissionQuantity = co2Emission;
        this.Emission = emission;
    }

    public Emission Emission { get; set; }
    public double MinProduction { get; init; }
    public double EmissionQuantity { get; init; }

    public double GetProductionEmission(double emissionPrice)
    {
        return this.MaxProduction * EmissionQuantity * emissionPrice;
    }

    public double GetProductionEmission(double production, double emissionPrice)
    {
        return production * EmissionQuantity * emissionPrice;
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
    public Fuel(string name, double price) : base(name, SourceType.Fossile)
    {
        this.Price = price;
    }

    public Fuel(string name) : base(name, SourceType.Fossile)
    {
    }

    public double Price { get; set; }
}

public class Flow : Source
{
    public Flow(string name) : base(name, SourceType.Green)
    {
    }

    public Flow(string name, double scale) : base(name, SourceType.Green)
    {
        this.Scale = scale;
    }

    public double Scale { get; set; }
}

public class Emission
{
    public Emission(string name)
    {
        this.Name = name;
    }
    public Emission(string name, double price)
    {
        this.Name = name;
        this.Price = price;
    }
    public string Name { get; set; }
    public double Price { get; set; }
}
namespace ProductionPlanAPI.Services;

using ProductionPlanAPI.Dtos;
using ProductionPlanAPI.Dtos.Request;
using ProductionPlanAPI.Helpers;
using ProductionPlanAPI.Models;
using static ProductionPlanAPI.Dtos.CreateProductionPlanResponse;

public class ProductionPlanService : IProductionPlanService
{
    PowerplantCreator PowerplantCreator { get; set; }
    ICollection<Source> Sources { get; set; } = new List<Source>();
    ICollection<Powerplant> Powerplants { get; set; } = new List<Powerplant>();
    ICollection<Emission> Emissions { get; set; } = new List<Emission>();
    public double Load { get; set; }

    public ProductionPlanService(PowerplantCreator powerplantCreator)
    {
        this.PowerplantCreator = powerplantCreator;
    }
    public CreateProductionPlanResponse GenerateProductionPlan()
    {
        var powerplantProduction = new List<PowerplantProduction>();
        //get list of powerplant with green energy 
        var greenPowerplants = this.Powerplants.Where(p => p.Source is Flow);

        //sort powerplants by their production capacity
        greenPowerplants = greenPowerplants.OrderByDescending(gp => (gp as GreenPowerplant).GetMaxProduction((this.Sources.FirstOrDefault(s => s.Name == gp.Source.Name) as Flow).Scale));

        //Generate load
        (var remainingLod, var greenPowerplantProduction) = GetGreenProductionLoad(greenPowerplants);
        powerplantProduction.AddRange(greenPowerplantProduction);

        //Check if remaining load is 0 or supperior
        if (remainingLod > 0)
        {
            //If more energy needed get list of powerplants with fossile energy
            var fossilePowerplants = this.Powerplants.Where(p => p.Source is Fuel);

            //Sort powerplants by production capacity 
            fossilePowerplants = fossilePowerplants
                .OrderByDescending(gp => (gp as FossilePowerPlant).GetProductionPrice((this.Sources.FirstOrDefault(s => s.Name == gp.Source.Name) as Fuel).Price)
                + (gp is PowerPlantWithEmission ? (gp as PowerPlantWithEmission).GetProductionEmission(this.Emissions.FirstOrDefault(x => x.Name == (gp as PowerPlantWithEmission).Emission.Name).Price) : 0));

            
            /* fossilePowerplants = fossilePowerplants
                .OrderByDescending(gp => (gp as FossilePowerPlant).GetProductionPrice((this.Sources.FirstOrDefault(s => s.Name == gp.Source.Name) as Fuel).Price));*/
             

            //Get load from powerplants 
            (var remainingLoad, var fossilePowerplantProduction) = GetFossilProductionLoad(fossilePowerplants, remainingLod);
            powerplantProduction.AddRange(fossilePowerplantProduction);
        }


        //return production plan
        return new CreateProductionPlanResponse { PowerplantProductions = powerplantProduction};
    }

    private (double, List<PowerplantProduction>) GetGreenProductionLoad(IEnumerable<Powerplant> greenPowerplants)
    {
        var powerplantProduction = new List<PowerplantProduction>();
        var remainingLoad = this.Load;
        
        foreach(GreenPowerplant powerplant in greenPowerplants)
        {
            if ((this.Sources.FirstOrDefault(s => s.Name == powerplant.Source.Name) as Flow).Scale == 0 || remainingLoad == 0)
            {
                powerplantProduction.Add(new PowerplantProduction { Name = powerplant.Name, Production = 0 });
                continue;
            }

            var powerplantLoad = powerplant.GetMaxProduction((this.Sources.FirstOrDefault(s => s.Name == powerplant.Source.Name) as Flow).Scale);

            if (remainingLoad >= powerplantLoad)
            {
                remainingLoad -= powerplantLoad;
                powerplantProduction.Add(new PowerplantProduction { Name = powerplant.Name, Production = powerplantLoad });
            }
            else
            {
                powerplantProduction.Add(new PowerplantProduction { Name = powerplant.Name, Production = remainingLoad });
                remainingLoad = 0;
            }
        }

        return (remainingLoad, powerplantProduction);
    }

    private (double, List<PowerplantProduction>) GetFossilProductionLoad(IEnumerable<Powerplant> fossilePowerplants, double load)
    {
        var powerplantProduction = new List<PowerplantProduction>();
        var remainingLoad = load;

        foreach (FossilePowerPlant powerplant in fossilePowerplants)
        {
            if (remainingLoad == 0)
            {
                powerplantProduction.Add(new PowerplantProduction { Name = powerplant.Name, Production = 0 });
                continue;
            }

            var powerplantLoad = powerplant.MaxProduction;

            if (remainingLoad >= powerplantLoad)
            {
                remainingLoad -= powerplantLoad;
                powerplantProduction.Add(new PowerplantProduction { Name = powerplant.Name, Production = powerplantLoad });
            }
            else
            {
                //check if I can get another source with less cost
                if (powerplant is PowerPlantWithEmission && (powerplant as PowerPlantWithEmission).MinProduction > remainingLoad)
                {
                    powerplantProduction.Add(new PowerplantProduction { Name = powerplant.Name, Production = (powerplant as PowerPlantWithEmission).MinProduction });
                }
                else
                {
                    powerplantProduction.Add(new PowerplantProduction { Name = powerplant.Name, Production = remainingLoad });
                }
                remainingLoad = 0;
            }
        }

        return (remainingLoad, powerplantProduction);
    }

    public bool SetupData(CreateProductionPlanRequest productionDto)
    {
        this.Load = productionDto.Load;

        // set Sources
        this.Sources.Add(new Fuel("Gas", productionDto.Fuels!.GasPrice));
        this.Sources.Add(new Fuel("Kerosine", productionDto.Fuels!.KerosinePrice));
        this.Sources.Add(new Flow("Air", productionDto.Fuels.Wind));

        // set Powerplants 
        foreach (var powerplantDto in productionDto.Powerplants!)
        {
            this.Powerplants.Add(this.PowerplantCreator.CreatePowerplant(powerplantDto));
        }

        //set Emissions
        this.Emissions.Add(new Emission("Co2", productionDto.Fuels.Co2Price));

        return true;
    }
}

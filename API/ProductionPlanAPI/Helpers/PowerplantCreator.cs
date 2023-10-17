using ProductionPlanAPI.Dtos;
using ProductionPlanAPI.Exceptions;
using ProductionPlanAPI.Models;

namespace ProductionPlanAPI.Helpers
{
    public class PowerplantCreator : PowerplantFactory
    {
        public override Powerplant CreatePowerplant(PowerplantDto powerplantDto)
        {
            switch (powerplantDto.Type)
            {
                case "windturbine":
                    return new GreenPowerplant(powerplantDto.Name!, new Flow("Air"), powerplantDto.Pmax);
                case "turbojet":
                    return new FossilePowerPlant(powerplantDto.Name!, new Fuel("Kerosine"), powerplantDto.Pmax, powerplantDto.Efficiency);
                case "gasfired":
                    return new PowerPlantWithEmission(powerplantDto.Name!, new Fuel("Gas"), powerplantDto.Pmax, powerplantDto.Efficiency, powerplantDto.Pmin, 0.3, new Emission("Co2"));
                default:
                    throw new UnknownPowerplantType($"The type {powerplantDto.Type} is not supported");
            };
        }
    }
}

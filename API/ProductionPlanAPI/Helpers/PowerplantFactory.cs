using ProductionPlanAPI.Dtos;
using ProductionPlanAPI.Models;

namespace ProductionPlanAPI.Helpers;

public abstract class PowerplantFactory
{
    public abstract Powerplant CreatePowerplant(PowerplantDto powerplantDto);
}

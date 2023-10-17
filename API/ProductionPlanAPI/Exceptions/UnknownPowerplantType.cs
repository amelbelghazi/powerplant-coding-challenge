namespace ProductionPlanAPI.Exceptions
{
    public class UnknownPowerplantType : BaseException
    {
        public UnknownPowerplantType(string message) : base(message)
        {
        }
    }
}

namespace ProductionPlanAPI.Exceptions
{
    public class InvalidDataException : BaseException
    {
        public InvalidDataException(string message) : base(message)
        {
        }
    }
}

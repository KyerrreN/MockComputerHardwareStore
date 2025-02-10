namespace Entities.Exceptions
{
    public sealed class MaxFpsRangeBadRequestException : BadRequestException
    {
        public MaxFpsRangeBadRequestException()
            : base ("Max FPS cannot be less than min FPS")
        {
            
        }
    }
}

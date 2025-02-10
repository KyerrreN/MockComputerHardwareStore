namespace Entities.Exceptions
{
    public sealed class MaxFpsOverflowBadRequestException : BadRequestException
    {
        public MaxFpsOverflowBadRequestException()
            : base("Max FPS cannot be more than 999.9")
        {
            
        }
    }
}

namespace Entities.Exceptions
{
    public sealed class MinFpsNegativeBadRequestException : BadRequestException
    {
        public MinFpsNegativeBadRequestException()
            : base("Minimum FPS cannot be negative")
        {
            
        }
    }
}

namespace Entities.Exceptions
{
    public sealed class GraphicsCardNotFoundException : NotFoundException
    {
        public GraphicsCardNotFoundException(Guid graphicsCardId) 
            : base($"The graphics card with id: {graphicsCardId} doesn't exist in the database")
        {
        }
    }
}

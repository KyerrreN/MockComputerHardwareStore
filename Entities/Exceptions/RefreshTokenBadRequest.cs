namespace Entities.Exceptions
{
    public class RefreshTokenBadRequest : BadRequestException
    {
        public RefreshTokenBadRequest() 
            : base("Invalid client request. TokenDto has some invalid values, or expired")
        {
        }
    }
}

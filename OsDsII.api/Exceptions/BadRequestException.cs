using OsDsII.api.Exceptions.constraints;
using System.Net;

namespace OsDsII.api.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) :
        base
            (
                ErrorCode.BAD_REQUEST,
                message,
                HttpStatusCode.NotFound,
                StatusCodes.Status400BadRequest,
                null,
                DateTimeOffset.UtcNow,
                null
            )
        { }
    }
}
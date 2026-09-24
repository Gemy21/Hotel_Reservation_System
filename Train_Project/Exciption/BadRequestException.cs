using Microsoft.AspNetCore.Http;

namespace Train_Project.Exciption;
public class BadRequestException : BaseException
{
    public BadRequestException(string message)
        : base(message, StatusCodes.Status400BadRequest, "BAD_REQUEST") 
    {
    }

    public BadRequestException(string message, Exception innerException)
        : base(message, StatusCodes.Status400BadRequest, "BAD_REQUEST", innerException) 
    {
    }
}

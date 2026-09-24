using Microsoft.AspNetCore.Http;

namespace Train_Project.Exciption;

public class NotFoundException : BaseException
{
    public NotFoundException(string message)
        : base(message, StatusCodes.Status404NotFound, "NOT_FOUND")
    {
    }

    public NotFoundException(string resourceName, object key)
        : base($"{resourceName} with key '{key}' was not found.", StatusCodes.Status404NotFound, "NOT_FOUND")
    {
    }
}

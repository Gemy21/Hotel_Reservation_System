using Microsoft.AspNetCore.Http;

namespace Train_Project.Exciption;

public class UnauthorizedException(string message = "Unauthorized access.")
    : BaseException(message, StatusCodes.Status401Unauthorized, "UNAUTHORIZED");

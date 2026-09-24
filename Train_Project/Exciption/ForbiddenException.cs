using Microsoft.AspNetCore.Http;

namespace Train_Project.Exciption;

public class ForbiddenException(string message = "Access forbidden.")
    : BaseException(message, StatusCodes.Status403Forbidden, "FORBIDDEN");

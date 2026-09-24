using Microsoft.AspNetCore.Http;

namespace Train_Project.Exciption;
public class ConflictException(string message) : BaseException(message, StatusCodes.Status409Conflict, "CONFLICT");

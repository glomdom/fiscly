namespace Fiscly.Api.Dto;

public sealed record CurrentUserResponse(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email
);
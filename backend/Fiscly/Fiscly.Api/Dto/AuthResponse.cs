namespace Fiscly.Api.Dto;

public sealed record AuthResponse(
    string Token,
    Guid UserId,
    string FirstName,
    string LastName,
    string Email
);
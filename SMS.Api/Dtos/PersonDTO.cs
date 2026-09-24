namespace SMS.Api.Dtos;

public record PersonDto(
    int Id, string FirstName, string LastName,
    string Email, string JobTitle, DateOnly HireDate, bool IsActive);

public record CreatePersonDto(
    string FirstName, string LastName,
    string Email, string JobTitle, DateOnly HireDate);
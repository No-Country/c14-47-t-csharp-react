using System.ComponentModel.DataAnnotations;

namespace OrganicFreshAPI.Entities.Dtos.Requests;


public record LoginRequest(
    string Email,
    string Password
);

public record RegisterRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}


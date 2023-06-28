using SalesSystem.Core.DTOs.Authentication;
using SalesSystem.Core.Entities;

namespace SalesSystem.Services.Concretes;
public interface IUserService
{
    public bool IsAuthenticated { get; }
    public User CurrentUser { get; }
    Task<bool> CheckPasswordAsync(string? email, string? password);
    string GetCurrentUserFullName();
    Task<string> SignInAsync(int id, string? role = null);
    Task<string> SignInAsync(string? email, string? password, string? role = null);
    Task CreateAsync(RegisterDto dto);
    Task RegisterAsync(RegisterDto dto);

}
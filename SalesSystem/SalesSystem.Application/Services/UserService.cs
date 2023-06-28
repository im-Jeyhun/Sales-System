using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SalesSystem.Services.Concretes;
using SalesSystem.Core.Contracts.Identity;
using SalesSystem.Core.DTOs.Authentication;
using SalesSystem.Core.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using SalesSystem.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SalesSystem.Application.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private User _currentUser;
    private readonly IUserRepository _userRepository;
    public UserService(
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        IMapper mapper,
        IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public User CurrentUser
    {
        get
        {
            if (_currentUser is not null)
            {
                return _currentUser;
            }

            var idClaim = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(C => C.Type == CustomClaimNames.ID);
            if (idClaim is null)
                throw new Exception("Identity cookie not found");

            var sql = "SELECT * FROM Users WHERE Id = @Id";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                _currentUser = connection.Query<User>(sql, new { Id = int.Parse(idClaim.Value) })
                    .First();
            }

            return _currentUser;
        }
    }

    public bool IsAuthenticated
    {
        get => _httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated;
    }

    public async Task<bool> CheckPasswordAsync(string? email, string? password)
    {
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(password);
        var user = await _userRepository.GetByEmailAsync(email);

        if (user is null) return false;

        return BCrypt.Net.BCrypt.Verify(password, user!.Password);
    }


    public string GetCurrentUserFullName()
    {
        return $"{CurrentUser.Name} {CurrentUser.Surname}";
    }

    public async Task<string> SignInAsync(int id, string? role = null)
    {
        var claims = new List<Claim>
        {
                new Claim(CustomClaimNames.ID, id.ToString())
        };

        if (role is not null) claims.Add(new Claim(ClaimTypes.Role, role));

        var issuer = _configuration["JwtOptinos:Issuer"];
        var audience = _configuration["JwtOptinos:Audience"];
        var key = _configuration["JwtOptinos:Key"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var expireDate = DateTime.Now.AddMinutes(double.Parse(_configuration["JwtOptinos:ExperationMinute"]));

        var tokenConfigurations = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: expireDate,
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(tokenConfigurations);
    }

    public async Task<string> SignInAsync(string? email, string? password, string? role = null)
    {
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(password);

        var user = await _userRepository.GetByEmailAsync(email);

        if (BCrypt.Net.BCrypt.Verify(password, user!.Password))
        {
            if (user.Role != null && user.Role == RoleNames.ADMIN)
            {
                return await SignInAsync(user.Id, user.Role);

            }
            else
            {
                return await SignInAsync(user.Id, role);
            }
        }
        return null;

    }


    public async Task CreateAsync(RegisterDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        var user = _mapper.Map<User>(dto);
        await _userRepository.Add(user);
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);

        if (user is not null) throw new ValidationException("Email already exists");

        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        dto.Password = hash;
        await CreateAsync(dto);
    }

}
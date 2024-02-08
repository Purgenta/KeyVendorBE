using System.Security.Claims;
using AspNetCore.Identity.MongoDbCore.Models;
using KeyVendor.Application.Common.Interfaces;
using KeyVendor.Domain.Entities;
using KeyVendor.Infrastructure.Exceptions;

namespace KeyVendor.Infrastructure.Auth;

public class UserService : IUserService
{
    private readonly ApplicationUserManager _userManager;

    public UserService(ApplicationUserManager userManager)
    {
        _userManager = userManager;
    }

    public async Task CreateUserAsync(string emailAddress, List<string> roles, double money)
    {
        var alreadyExist = await _userManager.FindByEmailAsync(emailAddress);

        if (alreadyExist != null)
            return;

        var user = new User
        {
            Email = emailAddress,
            UserName = emailAddress
        };

        try
        {
            user.Claims.Add(new MongoClaim { Type = ClaimTypes.Email, Value = emailAddress });
            user.Money = money;
            user.Claims.AddRange(roles.Select(userRole => new MongoClaim
            {
                Type = ClaimTypes.Role, Value = userRole
            }));

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                throw new AuthException("Could not create a new user",
                    new { Errors = result.Errors.ToList() });
            }

            var rolesResult = await _userManager.AddToRolesAsync(user,
                roles.Select(nr => nr.ToUpper()));

            if (!rolesResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);

                throw new AuthException("Could not add roles to user",
                    new { Errors = rolesResult.Errors.ToList() });
            }
        }
        catch (Exception e)
        {
            await _userManager.DeleteAsync(user);

            throw new AuthException("Could not create a new user",
                e);
        }
    }

    public async Task UpdateUserMoneyAsync(string emailAdress, double money)
    {
        var user = await _userManager.FindByEmailAsync(emailAdress);
        if (user == null)
            throw new AuthException("User not found");

        user.Money = money;
        await _userManager.UpdateAsync(user);
    }

    public Task<User?> GetUserAsync(string id) => _userManager.FindByIdAsync(id);
    public Task<User?> GetUserByEmailAsync(string id) => _userManager.FindByEmailAsync(id);

    public Task<bool> IsInRoleAsync(User user, string roleName) => _userManager.IsInRoleAsync(user,
        roleName);
}
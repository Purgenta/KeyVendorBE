namespace KeyVendor.Application.Common.Interfaces;

using KeyVendor.Domain.Entities;

public interface IUserService
{
    Task CreateUserAsync(string emailAddress, List<string> roles, double money);
    Task<User?> GetUserAsync(string id);
    Task<User?> GetUserByEmailAsync(string id);
    Task<bool> IsInRoleAsync(User user, string roleName);
    Task UpdateUserMoneyAsync(string emailAdress, double money);
}
using Microsoft.AspNetCore.Identity;
using POIT2025.Entities;

namespace POIT2025.Services;

public class AccountService : IAccountService
{
    private UserManager<UniversityUser> userManager;

    public AccountService(UserManager<UniversityUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<UniversityUser?> Login(
        string userName,
        string password)
    {
        var user = await userManager.FindByNameAsync(userName) 
            ?? throw new ArgumentException();
        
        var isPasswordCorrect = await userManager.CheckPasswordAsync(
            user,
            password);

        if (!isPasswordCorrect) 
        {
            throw new ArgumentException();
        }

        return user;
    }

    // Task stores a Completion flag - Completed, InProgress
    // Task stores Result - UniversityUser
    public async Task<UniversityUser?> Register(
        string userName,
        string password)
    {
        // check userName uniqueness
        // check password is not too easy
        // check if password is hashed

        var user = new UniversityUser()
        {
            UserName = userName,
            // add more fields
        };
        var result = await userManager.CreateAsync(user, password); 
        // external call - DB
        // not wait -- release thread
        // and will continue after get result

        if (result.Succeeded)
        {
            return user;
        }

        var errors = result.Errors;
        var errorMessage = string.Join(",",
            result.Errors.Select(x => x.Description));
        throw new ArgumentException(errorMessage);
    }
}

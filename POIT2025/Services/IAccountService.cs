using POIT2025.Entities;

namespace POIT2025.Services;

public interface IAccountService
{
    Task<UniversityUser?> Register(string userName, string password);

    Task<UniversityUser?> Login(string userName, string password);
}

using POIT2025.Entities;

namespace POIT2025.Services;

public interface ITokenService
{
    string CreateToken(UniversityUser user);
}

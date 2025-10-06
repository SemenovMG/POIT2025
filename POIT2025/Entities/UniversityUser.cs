using Microsoft.AspNetCore.Identity;

namespace POIT2025.Entities;

public class UniversityUser : IdentityUser
{
    public int? StudentId { get; set; }
}

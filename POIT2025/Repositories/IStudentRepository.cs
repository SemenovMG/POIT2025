using POIT2025.Dtos.Requests;
using POIT2025.Entities;

namespace POIT2025.Repositories;

public interface IStudentRepository
{
    List<Student> GetAll(string? group);
    Student? GetById(int id);
    Student Add(AddStudentRequest req);
    Student? Update(int id, AddStudentRequest req);
    Student? Delete(int id);
}

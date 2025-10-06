using POIT2025.Data;
using POIT2025.Dtos.Requests;
using POIT2025.Entities;

namespace POIT2025.Repositories;

public class StudentRepository : IStudentRepository
{
    private ScheduleContext _db;

    public StudentRepository(ScheduleContext db)
    {
        _db = db;
    }

    public List<Student> GetAll(string? group)
    {
        if (group is null)
        {
            return _db.Students.ToList();
        }

        return _db.Students//.Where(s => s.Group == group)
            .ToList();
    }

    public Student? GetById(int id)
    {
        var result = _db.Students.FirstOrDefault(s => s.Id == id);

        return result;
    }

    public Student Add(AddStudentRequest req)
    {
        var newStud = new Student()
        {
            Name = req.Name,
            //Group = req.Group,
        };

        _db.Students.Add(newStud);

        _db.SaveChanges();

        return newStud;
    }

    public Student? Update(int id, AddStudentRequest req)
    {
        var stud = GetById(id);
        if (stud is null)
            return null;
        stud.Name = req.Name;
        //stud.Group = req.Group;
        _db.SaveChanges();
        return stud;
    }

    public Student? Delete(int id)
    {
        var stud = GetById(id);
        if (stud is null)
            return null;

        _db.Students.Remove(stud);
        _db.SaveChanges();

        return stud;
    }
}

using POIT2025.Dtos.Requests;
using POIT2025.Entities;

namespace POIT2025.Repositories;

public class StudentLocalRepository : IStudentRepository
{
    private static List<Student> _students = [
            new() {
                Id = 1,
                Name = "Alex",
            },
            new() {
                Id = 2,
                Name = "Andrey",
            },
            new() {
                Id = 3,
                Name = "Viktor",
            },
        ];

    private static int _nextId = 4;

    public List<Student> GetAll(string? group)
    {
        if (group is null)
        {
            return _students;
        }

        return _students//.Where(s => s.Group == group)
            .ToList();
    }

    public Student? GetById(int id)
    {
        var result = _students.FirstOrDefault(s => s.Id == id);

        return result;
    }

    public Student Add(AddStudentRequest req)
    {
        var newStud = new Student()
        {
            Id = _nextId,
            Name = req.Name,
            //Group = req.Group,
        };
        _nextId++;

        _students.Add(newStud);

        return newStud;
    }

    public Student? Update(int id, AddStudentRequest req)
    {
        var stud = GetById(id);
        if (stud is null)
            return null;
        stud.Name = req.Name;
        //stud.Group = req.Group;
        return stud;
    }

    public Student? Delete(int id)
    {
        var stud = GetById(id);
        if (stud is null)
            return null;
        _students = _students.Where(_student => _student.Id != id)
            .ToList();

        return stud;
    }
}

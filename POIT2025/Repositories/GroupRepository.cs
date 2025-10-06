using Microsoft.EntityFrameworkCore;
using POIT2025.Data;
using POIT2025.Entities;

namespace POIT2025.Repositories;

public class GroupRepository : IGroupRepository
{
    private ScheduleContext _db;

    public GroupRepository(ScheduleContext db)
    {
        _db = db;
    }

    public Group? Add(string name, int capacity)
    {
        var group = new Group { Name = name, Capacity = capacity };
        _db.Groups.Add(group);
        _db.SaveChanges();
        return group;
    }

    public bool CheckUniqueName(string name)
    {
        return !_db.Groups.Any(g => g.Name == name);
    }

    public List<Group> GetAll()
    {
        var result = _db.Groups
            .Include(g => g.Students)
            //.Include(g => g.Faculty)
            //    .ThenInclude(f => f.Dean)
            .ToList();
        return result;
    }

    public Group? GetById(int id)
    {
        var result = _db.Groups
            .Include(g => g.Students)
            .FirstOrDefault(g => g.Id == id);

        return result;
    }
}

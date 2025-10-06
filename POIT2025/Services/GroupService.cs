using POIT2025.Constants;
using POIT2025.Entities;
using POIT2025.Repositories;

namespace POIT2025.Services;

public class GroupService : IGroupService
{
    private IGroupRepository _repo;

    public GroupService(IGroupRepository repo)
    {
        _repo = repo;
    }

    public Group? Add(string name, int capacity)
    {
        if (capacity > GroupConstants.MaxGroupCapacity)
        {
            throw new Exception($"Capacity should be less or equal to {GroupConstants.MaxGroupCapacity}");
        }

        var isNameUnique = _repo.CheckUniqueName(name);
        if (!isNameUnique)
        {
            throw new Exception($"Name should be unique");
        }

        return _repo.Add(name, capacity);
    }

    public List<Group> GetAll()
    {
        return _repo.GetAll();
    }

    public Group? GetById(int id)
    {
        return _repo.GetById(id);
    }
}

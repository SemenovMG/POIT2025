using POIT2025.Entities;

namespace POIT2025.Repositories;

public interface IGroupRepository
{
    List<Group> GetAll();
    Group? GetById(int id);
    Group? Add(string name, int capacity);
    bool CheckUniqueName(string name);
}

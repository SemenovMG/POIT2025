using POIT2025.Entities;

namespace POIT2025.Services;


// Controller -> Service -> Repo

public interface IGroupService
{
    List<Group> GetAll();
    Group? GetById(int id);
    Group? Add(string name, int capacity);
}

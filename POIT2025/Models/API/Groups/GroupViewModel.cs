namespace POIT2025.Models.API.Groups;

public class GroupViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<GroupStudentViewModel> Students { get; set; }
}

public class GroupStudentViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
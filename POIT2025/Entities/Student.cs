using System.ComponentModel.DataAnnotations;

namespace POIT2025.Entities;

public class Student
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Group? Group { get; set; }

    // public int? GroupId { get; set; }
    // public DateTime DoB {  get; set; }
    // public string Group { get; set; }
}

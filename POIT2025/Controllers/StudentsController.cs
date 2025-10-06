using Microsoft.AspNetCore.Mvc;
using POIT2025.Dtos.Requests;
using POIT2025.Entities;
using POIT2025.Repositories;

namespace POIT2025.Controllers;

// {protocol+domen}/students/42/details?filter=3
// {domen}/
// /api/students
[Route("[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{

    private IStudentRepository _repo;

    public StudentsController(IStudentRepository repo)
    {
        _repo = repo;
    }

    // GET /students
    [HttpGet]
    public ActionResult<List<Student>> GetAll(
        [FromQuery]string? group)
    {
        return Ok(_repo.GetAll(group));
    }

    // GET /students/42
    [HttpGet("{id}")]
    public ActionResult<Student> GetById(int id)
    {
        var result = _repo.GetById(id);
        if (result is null)
        {
            return NotFound($"Student with id = {id} is not found");
        }

        return Ok(result);
    }

    // POST /students
    [HttpPost()]
    public ActionResult<Student> Add([FromBody] AddStudentRequest req)
    {
        var newStud = _repo.Add(req);
        return Ok(newStud);
    }

    //PUT /students/42
    [HttpPut("{id}")]
    public ActionResult<Student> Update(
        int id, 
        [FromBody] AddStudentRequest req)
    {
        var newStud = _repo.Update(id, req);
        if (newStud is null)
        {
            return NotFound();
        }
        return Ok(newStud);
    }

    //Delete /students/42
    [HttpDelete("{id}")]
    public ActionResult<Student> Delete(
    int id)
    {
        var newStud = _repo.Delete(id);
        if (newStud is null)
        {
            return NotFound();
        }
        return Ok(newStud);
    }
}

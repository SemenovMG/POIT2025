using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POIT2025.Entities;
using POIT2025.Models.API.Groups;
using POIT2025.Repositories;
using POIT2025.Services;

namespace POIT2025.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController : ControllerBase
{
    private IGroupService _service;

    public GroupController(IGroupService service)
    {
        _service = service;
    }

    // GET /groups
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet]
    public ActionResult<List<GroupViewModel>> GetAll()
    {
        var user = User;
        var data = _service.GetAll();
        // AutoMappers
        var result = data.Select(groupEntity => new GroupViewModel
        {
            Id = groupEntity.Id,
            Name = groupEntity.Name,
            Students = groupEntity.Students
                .Select(studentEntity => new GroupStudentViewModel
                {
                    Id = studentEntity.Id,
                    Name = studentEntity.Name,
                }).ToList()
        }).ToList();
        return Ok(result);
    }

    // GET /groups/42
    [HttpGet("{id}")]
    public ActionResult<Student> GetById(int id)
    {
        var result = _service.GetById(id);
        if (result is null)
        {
            return NotFound($"Group with id = {id} is not found");
        }

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Group> Add(string name, int capacity) // TODO: move to model
    {
        var group = _service.Add(name, capacity);
        if (group is null)
        {
            return BadRequest("Can not create group");
        }
        return group;
    }
}

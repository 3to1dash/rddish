using Microsoft.AspNetCore.Mvc;
using rddish.api.Data;
using rddish.Application;
using rddish.Radius;
using rddish.Radius.Data;

namespace rddish.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RadiusController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public RadiusController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost("AddNas")]
    public IActionResult AddNas([FromBody] NasDto input)
    {
        throw new NotImplementedException();
    }

    [HttpPost("AddUser")]
    public IActionResult AddUserToGroup(AddUserDto input)
    {
        throw new NotImplementedException();
    }

    [HttpPost("AddGroup")]
    public IActionResult AddGroup(AddGroupDto input)
    {
        throw new NotImplementedException();
    }

    [HttpPost("AddUserToGroup")]
    public IActionResult AddUserToGroup(AddUserToGroupDto input)
    {
        throw new NotImplementedException();
    }

    [HttpPost("AddUserLimit")]
    public IActionResult AddUserLimit()
    {
        throw new NotImplementedException();
    }

    [HttpPost("AddSim")]
    public async Task<IActionResult> AddSim(string userName, int allowedSessions)
    {
        var response = new Result<int>();
        try
        {
            var result = await _unitOfWork.AddSimultaneous(userName, allowedSessions);
            return StatusCode(StatusCodes.Status201Created, response.Success(result));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, response.Fail(e));
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using rddish.API.Data.RequestDtos;
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

    [HttpPost("AddUserStaticIp")]
    public async Task<IActionResult> AddUserStaticIp(string userName, string staticIp)
    {
        var response = new Result<int>();
        try
        {
            var result = await _unitOfWork.AddStaticIp(userName, staticIp);
            return StatusCode(StatusCodes.Status201Created, response.Success(result));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, response.Fail(e));
        }
    }

    [HttpPost("AddUserExpirationDate")]
    public async Task<IActionResult> AddUserExpirationDate(string userName, DateTime expirationDate)
    {
        var response = new Result<int>();
        try
        {
            var result = await _unitOfWork.AddUserExpirationDate(userName, expirationDate);
            return StatusCode(StatusCodes.Status201Created, response.Success(result));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, response.Fail(e));
        }
    }

    [HttpPost("AddUserRateLimit")]
    public async Task<IActionResult> AddUserRateLimit(string userName, string uploadLimit, string downloadLimit)
    {
        var response = new Result<int>();
        try
        {
            var result = await _unitOfWork.AddUserRateLimit(userName, uploadLimit, downloadLimit);
            return StatusCode(StatusCodes.Status201Created, response.Success(result));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, response.Fail(e));
        }
    }
}
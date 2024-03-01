using Microsoft.AspNetCore.Mvc;
using rddish.API.Data.RequestDtos;
using rddish.Application;
using rddish.Mikrotik.Services;

namespace rddish.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MikrorikController : ControllerBase
{
    private readonly IMikrotikService _mikrotikService;

    public MikrorikController(IMikrotikService mikrotikService)
    {
        _mikrotikService = mikrotikService;
    }

    [HttpPost("AddRadiusClient")]
    public IActionResult AddRadiusClient([FromBody] RadiusClient input)
    {
        var response = new Result<object>();
        try
        {
            _mikrotikService.AddRadiusClient(input.Services, input.Address, input.Secret);
            return StatusCode(StatusCodes.Status201Created, response.Success());
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, response.Fail(e));
        }
    }

    [HttpPost("AddIpPool")]
    public IActionResult AddIpPool([FromBody] IpPool input)
    {
        var response = new Result<object>();
        try
        {
            _mikrotikService.AddIpPool(input.Name, input.FromIp, input.ToIp);
            return StatusCode(StatusCodes.Status201Created, response.Success());
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, response.Fail(e));
        }
    }
}
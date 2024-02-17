using Microsoft.AspNetCore.Mvc;
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
}

public class RadiusClient
{
    public string Services { get; set; }
    public string Address { get; set; }
    public string Secret { get; set; }
}
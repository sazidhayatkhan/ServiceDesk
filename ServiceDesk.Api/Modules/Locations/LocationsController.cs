using Microsoft.AspNetCore.Mvc;

namespace ServiceDesk.Api.Modules.Locations;

[ApiController]
[Route("api/locations")]
public class LocationsController : ControllerBase
{
    private readonly LocationService _service;

    public LocationsController(LocationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLocationRequest request)
    {
        var location = await _service.CreateAsync(request);

        return Ok(location);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }
}
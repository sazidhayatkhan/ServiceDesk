using Microsoft.AspNetCore.Mvc;

namespace ServiceDesk.Api.Modules.Requests;

[ApiController]
[Route("api/requests")]
public class RequestsController : ControllerBase
{
    private readonly ServiceRequestService _service;

    public RequestsController(ServiceRequestService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateServiceRequest request)
    {
        var serviceRequest =
            await _service.CreateAsync(request);

        return Ok(serviceRequest);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }
}
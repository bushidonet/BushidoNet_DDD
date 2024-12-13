using AMochika.Application;
using Microsoft.AspNetCore.Mvc;

namespace AMochika.Presentation.Controller;

[ApiController]
[Route("api/[controller]")]
public class ClientController: ControllerBase
{
    private readonly ClientAppService _clientAppService;

    public ClientController(ClientAppService clientAppService)
    {
        _clientAppService = clientAppService;
    }

    [HttpGet("validate-payment/{clientId}")]
    public async Task<IActionResult> ValidateMonthlyPayment(int clientId)
    {
        var result = await _clientAppService.ValidateMonthlyPaymentAsync(clientId);
        if (result)
        {
            return Ok("Client has paid the monthly fee.");
        }
        return BadRequest("Client has not paid the monthly fee.");
    }
}
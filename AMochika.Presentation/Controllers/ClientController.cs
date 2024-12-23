using AMochika.Application;
using AMochika.Application.DTOs;
using AMochika.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AMochika.Presentation.Controller;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly ClientService _client;

    public ClientController(ClientService client)
    {
        _client = client;
    }

    // [HttpGet("validate-payment/{clientId}")]
    // public async Task<IActionResult> ValidateMonthlyPayment(int clientId)
    // {
    //     var result = await _clientAppService.ValidateMonthlyPaymentAsync(clientId);
    //     if (result)
    //     {
    //         return Ok("Client has paid the monthly fee.");
    //     }
    //     return BadRequest("Client has not paid the monthly fee.");
    // }

    [HttpGet("getClient/{clientId}")]
    public async Task<IActionResult> GetClientById(int clientId)
    {
        var result = await _client.GetClientByIdAsync(clientId);
        return Ok(result);
    }

    [HttpGet("getAllClient")]
    public async Task<IActionResult> GetAllClient()
    {
        var result = await _client.GetAllClientAsync();
        return Ok(result);
    }

    [HttpPost("addClient")]
    public async Task<IActionResult> AddClient(CreateClientDTO clientDto)
    {
        var result = await _client.AddClientAsync(clientDto);
        return Ok(result);
    }

    [HttpPut("updateClient/{idClient}")]
    public async Task<IActionResult> UpdateClient([FromBody] UpdateClientDTO updateClientDto)
    {

        var result = await _client.UpdateAsync(updateClientDto);
        return Ok(result);
    }

    [HttpDelete("deleteClient/{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {

        var result = await _client.DeleteAsync(idClient);
        if (result != -1) return Ok(result);

        return BadRequest("Client not found");

    }


}
using AMochika.Application;
using AMochika.Application.DTOs;
using AMochika.Application.Services;
using AMochika.Core.Entities;
using AMochika.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AMochika.Presentation.Controller;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly ClientService _client;
    private readonly IRepository<Client> _generic;

    public ClientController(ClientService client, IRepository<Client> repository)
    {
        _client = client;
        _generic = repository;
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

    [HttpGet("getTest")]
    public async Task<IActionResult> GetTest()
    {
       
        var newClient = new Client
        {
            FirstName = "Eugen",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            Phone = "987-654-3210",
            BirthDate = new DateTime(1992, 6, 15)
        };

        //var clientGeneric = _generic.GetAllAsync().Result;
        //var client = _generic.GetByIdAsync(2).Result;
        //await _generic.AddAsync(newClient);

        await _generic.DeleteAsync(15);
        return Ok();
    }
    [HttpGet("getClient/{clientId}")]
    public async Task<IActionResult> GetClientById(int clientId)
    {
        var result =  _client.GetClientByIdAsync(clientId).Result;
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
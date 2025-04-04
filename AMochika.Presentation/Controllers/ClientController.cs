using AMochika.Application;
using AMochika.Application.DTOs;
using AMochika.Application.Interfaces;
using AMochika.Application.Services;
using AMochika.Core.Entities;
using AMochika.Infrastructure.Configuration;
using AMochika.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMochika.Presentation.Controller;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _client;
    private readonly IRepository<Client> _generic;
    private readonly AppDbContext _appDbContext;

    public ClientController(IClientService client, IRepository<Client> repository, AppDbContext appDbContext)
    {
        _client = client;
        _generic = repository;
        _appDbContext = appDbContext;
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

 
        // var query = "SELECT Id, FirstName, Email FROM Clients WHERE Id = 1";
        // return await _appDbContext.Clients.FromSqlRaw(query)
        //             .Select(c => new ClientDto
        //             {
        //                 Id = c.Id,
        //                 FirstName = c.FirstName,
        //                 Email = c.Email
        //             })
        //             .ToListAsync();
       
        // var newClient = new Client
        // {
        //     FirstName = "Eugen",
        //     LastName = "Smith",
        //     Email = "jane.smith@example.com",
        //     Phone = "987-654-3210",
        //     BirthDate = new DateTime(1992, 6, 15)
        // };

        //var clientGeneric = _generic.GetAllAsync().Result;
        //var client = _generic.GetByIdAsync(2).Result;
        //await _generic.AddAsync(newClient);

        // await _generic.DeleteAsync(15);
        // return Ok();
    [Authorize]
    [HttpGet("getClient/{clientId}")]
    public async Task<IActionResult> GetClientById(int clientId)
    {
        var result =  await _client.GetClientByIdAsync(clientId);
        return Ok(result);
    }
    [Authorize]
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

    // [HttpGet("getTest")]
    // public async Task<ClientDto> GetTest()
    // {
        //Crear un cliente DTO
        // ClientDto newClient = new ClientDto
        // {
        //     Id = 1,
        //     FirstName = "Juan",
        //     LastName = "Pérez",
        //     Email = "juan.perez@example.com",
        //     Phone = "+123456789",
        //     BirthDate = new DateTime(1990, 5, 15)
        // };
        // ClientDto newClient = null;
        //Comprobar si es cliente DTO
        // if (newClient is ClientDto myClient)
        // {
        //     return myClient;
        // }
        //
        // return new ClientDto {};
//     }
//     
}
//
// public class ClientDto
// {
//     public int Id { get; set; }           // El identificador del cliente
//     public string FirstName { get; set; }  // Nombre del cliente
//     public string LastName { get; set; }   // Apellido del cliente
//     public string Email { get; set; }      // Correo electrónico del cliente
//     public string Phone { get; set; }      // Teléfono del cliente
//     public DateTime BirthDate { get; set; } // Fecha de nacimiento del cliente
// }
using AMochika.Application.DTOs;
using AMochika.Application.Interfaces;
using AMochika.Core.Entities;
using AMochika.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AMochika.Presentation.Controller;
[Route("api/[controller]")]
[ApiController]
public class UnitOfWorkController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClientService _clientService;

    public UnitOfWorkController(IUnitOfWork unitOfWork, IClientService clientService)
    {
        _unitOfWork = unitOfWork;
        _clientService = clientService;
    }

    [HttpPost("addClient")]
    public async Task<IActionResult> AddClient(CreateClientDTO clientDto)
    {
        return Ok(await _clientService.AddClientAsync(clientDto));
    }

}
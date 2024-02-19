using System;
using System.Threading.Tasks;
using DeadmanSwitchFailed.Services.Vault.Service.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DeadmanSwitchFailed.Services.Notification.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VaultController : ControllerBase
{
  private readonly UnitOfWork _unitOfWork;

  public VaultController(UnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  [HttpGet("{id:guid}")]
  public async Task<ActionResult<Vault.Service.Domain.Models.Vault>> Get_Vault(Guid id)
  {
    return await _unitOfWork.VaultRepository.GetByIdAsync(id) is { } vault ? Ok(vault) : NotFound();
  }
}
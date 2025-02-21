using AutoMapper;
using BudgetManagement.Server.Auth;
using BudgetManagement.Server.Dtos.Record;
using BudgetManagement.Server.Extensions;
using BudgetManagement.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagement.Server.Controllers;

[ApiController]
[Route("records")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PoliciesConfiguration.UserPolicy)]
public class RecordController(
    IExpenseRecordService expenseRecordService,
    IMapper mapper,
    ILogger<CategoryController> logger)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserRecords(CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.Id<Guid>();

        var records = await expenseRecordService.GetUserRecordsAsync(userId, cancellationToken);

        var response = mapper.Map<IEnumerable<RecordDto>>(records);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecord([FromBody] CreateRecordDto command, CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.Id<Guid>();

        var record = await expenseRecordService.CreateAsync(command.Amount, command.CategoryId, userId, cancellationToken);

        var response = mapper.Map<RecordDto>(record);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecord(Guid id, CancellationToken cancellationToken)
    {
        var record = await expenseRecordService.DeleteAsync(id, cancellationToken);
        if (record is null)
            return NotFound();
        
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRecord([FromBody] UpdateRecordDto command, CancellationToken cancellationToken)
    {
        var record = await expenseRecordService.UpdateAsync(command.Id, command.Amount, command.CategoryId, cancellationToken);
        if (record is null)
            return NotFound();

        var response = mapper.Map<RecordDto>(record);

        return Ok(response);
    }
}

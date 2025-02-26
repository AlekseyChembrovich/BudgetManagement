using BudgetManagement.Server.Auth;
using BudgetManagement.Server.Dtos.Report;
using BudgetManagement.Server.Extensions;
using BudgetManagement.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagement.Server.Controllers;

[ApiController]
[Route("report")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PoliciesConfiguration.UserPolicy)]
public class ReportController(
    IExpenseRecordService expenseRecordService,
    ILogger<ReportController> logger)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetReport(
        [FromQuery] GetReportDto request,
        CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.Id<Guid>();

        var records = await expenseRecordService.GetUserRecordsAsync(
            userId,
            request.CategoryId,
            request.From,
            request.To,
            cancellationToken);

        var response = new List<ReportDto>();
        foreach (var group in records.GroupBy(x => x.CategoryId))
        {
            var recordGroup = group.ToArray();
            var totalSum = recordGroup.Sum(x => x.Amount);
            response.Add(new ReportDto
            {
                CategoryId = group.Key,
                CategoryName = recordGroup[0].Category!.Name,
                TotalSum = totalSum
            });
        }
        
        return Ok(response);
    }
}

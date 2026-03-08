using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawRobot.Web.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    [HttpPost("ingest/rss")]
    public IActionResult TriggerRssIngest() => StatusCode(StatusCodes.Status501NotImplemented);

    [HttpPost("ingest/docx")]
    public IActionResult TriggerDocxIngest() => StatusCode(StatusCodes.Status501NotImplemented);

    [HttpPost("summarize")]
    public IActionResult TriggerSummarize() => StatusCode(StatusCodes.Status501NotImplemented);

    [HttpPost("check-changes")]
    public IActionResult TriggerChangeCheck() => StatusCode(StatusCodes.Status501NotImplemented);

    [HttpGet("jobs")]
    public IActionResult Jobs() => StatusCode(StatusCodes.Status501NotImplemented);
}

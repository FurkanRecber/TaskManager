using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Abstraction;
using TaskManager.Application.Abstraction.Dto;
using TaskManager.WebAPI.Controllers.Base;
using TaskStatus = TaskManager.Common.Models.Enums.TaskStatus;

namespace TaskManager.WebAPI.Controllers;

public class TaskController(ITaskService service) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get()
        => ApiResult(await service.GetAll());
    
    [HttpGet("paged/{page:int}/{size:int}")]
    public async Task<IActionResult> GetPaged([FromRoute] int page,[FromRoute] int size)
        => ApiResult(await service.GetPagedListAsync(page, size));
    
    [HttpGet("tag/{tagId:guid}")]
    public async Task<IActionResult> GetByTag([FromRoute] Guid tagId)
        => ApiResult(await service.GetByTag(tagId));
    
    [HttpGet("state/{status:int}")]
    public async Task<IActionResult> GetByUser([FromRoute] int status)
        => ApiResult(await service.GetByState((TaskStatus)status));
    
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser([FromRoute] Guid userId)
        => ApiResult(await service.GetByUser(userId));
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
        => ApiResult(await service.GetFirstOrDefaultAsync(x => x.Id == id));

    [HttpGet("get-weekly")] 
    [Authorize(AuthenticationSchemes = "Admin")]
    public async Task<IActionResult> GetWeekly()
        => ApiResult(await service.GetByDate(7));
    
    [HttpGet("get-monthly")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public async Task<IActionResult> GetMonthly()
        => ApiResult(await service.GetByDate(30));
    
    [HttpGet("get-yearly")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public async Task<IActionResult> GetYearly()
        => ApiResult(await service.GetByDate(365));
    
    [HttpGet("get-specific-day/{day:int}")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public async Task<IActionResult> GetCustomDay([FromRoute] int day)
        => ApiResult(await service.GetByDate(day));

    [Authorize(AuthenticationSchemes = "Admin")]
    [HttpPost("add-tag/{id:guid}/{tagId:guid}")]
    public async Task<IActionResult> AddTag([FromRoute] Guid tagId, [FromRoute] Guid id)
        => ApiResult(await service.AddTag(id, tagId));

    [Authorize(AuthenticationSchemes = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto model)
        => ApiResult(await service.Create(model));
    
    [Authorize(AuthenticationSchemes = "Admin")]
    [HttpPost("update-status/{id:guid}/{status:int}")]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromRoute] int status)
        => ApiResult(await service.UpdateState(id, (TaskStatus)status));

    [Authorize(AuthenticationSchemes = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Update(TaskDto model)
        => ApiResult(await service.UpdateAsync(model));
    
    [Authorize(AuthenticationSchemes = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
        => ApiResult(await service.DeleteAsync(id));
}
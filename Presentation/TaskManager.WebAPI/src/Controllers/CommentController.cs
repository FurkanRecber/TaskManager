using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Abstraction;
using TaskManager.Application.Abstraction.Dto;
using TaskManager.WebAPI.Controllers.Base;

namespace TaskManager.WebAPI.Controllers;

public class CommentController(ICommentService service) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get()
        => ApiResult(await service.Get());
    
    [HttpGet("paged/{page:int}/{size:int}")]
    public async Task<IActionResult> GetPaged([FromRoute] int page,[FromRoute] int size)
        => ApiResult(await service.GetPagedListAsync(page, size));
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPaged([FromRoute] Guid id)
        => ApiResult(await service.GetFirstOrDefaultAsync(x => x.Id == id));

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Admin")]
    public async Task<IActionResult> Create(CreateCommentDto model)
        => ApiResult(await service.Create(model));

    [HttpPut]
    [Authorize(AuthenticationSchemes = "Admin")]
    public async Task<IActionResult> Update(CommentDto model)
        => ApiResult(await service.UpdateAsync(model));
    
    [HttpDelete]
    [Authorize(AuthenticationSchemes = "Admin")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
        => ApiResult(await service.DeleteAsync(id));
}
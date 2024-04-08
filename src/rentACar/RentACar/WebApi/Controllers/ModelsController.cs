using Application.Features.Models.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ModelsController : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetList([FromQuery]PageRequest pageRequest)
		{
			GetListModelQuery query = new GetListModelQuery{ PageRequest=pageRequest};
			GetListResponse<GetListModelListItemDto> response = await Mediator.Send(query);
			return Ok(response);
		}
	}
}

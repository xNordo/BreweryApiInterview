using Microsoft.AspNetCore.Mvc;
using WebApplication1.API.DTOs;
using WebApplication1.API.Mappers;
using WebApplication1.Domain.Enums;
using WebApplication1.Application.Services;

namespace WebApplication1.API.V1.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BreweriesController : ControllerBase
    {
        private readonly IBreweryService _breweryService;

        public BreweriesController(IBreweryService breweryService)
        {
            _breweryService = breweryService;
        }

        [HttpGet]
        [MapToApiVersion("1")]
        public async Task<ActionResult<IEnumerable<BreweryDto>>> GetBreweries(
            [FromQuery] string? search = null,
            [FromQuery] SortBy? sortBy = null,
            [FromQuery] SortDirection sortDirection = SortDirection.Ascending)
        {
            var queryParameters = BreweryMapper.ToQueryParameters(search, sortBy, sortDirection);

            var breweries = await _breweryService.GetBreweriesAsync(queryParameters);
            var dtos = breweries.Select(BreweryMapper.ToDto);

            return Ok(dtos);
        }
    }
}

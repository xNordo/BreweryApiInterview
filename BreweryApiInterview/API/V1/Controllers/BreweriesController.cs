using Microsoft.AspNetCore.Mvc;
using BreweryApiInterview.API.DTOs;
using BreweryApiInterview.API.Mappers;
using BreweryApiInterview.Application.Enums;
using BreweryApiInterview.Application.Services;
using System.ComponentModel.DataAnnotations;

namespace BreweryApiInterview.API.V1.Controllers
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
            [FromQuery] string? byCity = null,
            [FromQuery] string? byName = null,
            [FromQuery] double? latitude = null,
            [FromQuery] double? longitude = null,
            [FromQuery] SortBy? sortBy = null,
            [FromQuery] SortDirection sortDirection = SortDirection.Ascending,
            [FromQuery] [Range(1, int.MaxValue)] int page = 1,
            [FromQuery] [Range(1, 50)] int perPage = 20)
        {
            var queryParameters = BreweryMapper.ToQueryParameters(
                searchTerm: search,
                byCity: byCity,
                byName: byName,
                latitude: latitude,
                longitude: longitude,
                sortBy: sortBy,
                sortDirection: sortDirection,
                page: page,
                perPage: perPage);

            var breweries = await _breweryService.GetBreweriesAsync(queryParameters);
            var dtos = breweries.Select(BreweryMapper.ToDto);

            return Ok(dtos);
        }
    }
}
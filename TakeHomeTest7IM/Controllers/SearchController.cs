using Microsoft.AspNetCore.Mvc;
using TakeHomeTest7IM.Services;

namespace TakeHomeTest7IM.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ILogger<SearchController> _logger;

        public SearchController(ISearchService searchService, ILogger<SearchController> logger)
        {
            _searchService = searchService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return BadRequest("Please provide a search term.");
                }

                var matchingPersons = _searchService.SearchPersons(searchTerm);

                if (matchingPersons.Any())
                {
                    return Ok(matchingPersons);
                }
                else
                {
                    return NotFound("No results found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the search.");
                return StatusCode(500, "Eerror occurred. Please try again later.");
            }
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.Properties.Queries.GetAllProperties;
using RealEstateApp.Core.Application.Features.Properties.Queries.GetPropertiesById;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Properties;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [SwaggerTag("Properties queries")]
    public class PropertiesController : BaseApiController
    {
       
        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet]
        [SwaggerOperation(
           Summary = "List all properties",
           Description = "Gets all properties."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertiesViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                var properties = await Mediator.Send(new GetAllPropertiesQuery());
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet("{id}")]
        [SwaggerOperation(
           Summary = "Get property by id",
           Description = "Gets the property using the id as filter."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertiesViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var property = await Mediator.Send(new GetPropertiesByIdQuery { Id = id });
                return Ok(property);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet("{code}")]
        [SwaggerOperation(
           Summary = "Get property by code",
           Description = "Gets the property using the code as filter."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertiesViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCode(string code)
        {
            try
            {
                var property = await Mediator.Send(new GetPropertiesByCodeQuery { Code = code });
                return Ok(property);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

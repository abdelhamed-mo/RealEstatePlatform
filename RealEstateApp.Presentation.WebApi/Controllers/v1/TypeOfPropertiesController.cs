using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.Improvements.Commands.DeleteImprovementsById;
using RealEstateApp.Core.Application.Features.TypeOfProperties.Commands.CreateTypeOfProperties;
using RealEstateApp.Core.Application.Features.TypeOfProperties.Commands.DeleteTypeOfPropertiesById;
using RealEstateApp.Core.Application.Features.TypeOfProperties.Commands.UpdateTypeOfProperties;
using RealEstateApp.Core.Application.Features.TypeOfProperties.Queries.GetAllTypeOfProperties;
using RealEstateApp.Core.Application.Features.TypeOfProperties.Queries.GetTypeOfPropertiesById;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TypeOfProperties;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Type of properties management")]
    public class TypeOfPropertiesController : BaseApiController
    {
        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet]
        [SwaggerOperation(
           Summary = "List all property types",
           Description = "Gets all registered property types."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeOfPropertiesViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                var typeOfProperties = await Mediator.Send(new GetAllTypeOfPropertiesQuery());
                return Ok(typeOfProperties);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet("{id}")]
        [SwaggerOperation(
           Summary = "Get property type by id",
           Description = "Gets a property type using the id as filter."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTypeOfPropertiesViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var typeOfProperty = await Mediator.Send(new GetTypeOfPropertiesByIdQuery { Id = id});
                return Ok(typeOfProperty);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [SwaggerOperation(
          Summary = "Create property type",
          Description = "Receives the parameters required to create a new property type."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateTypeOfPropertiesCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [SwaggerOperation(
               Summary = "Update property type",
               Description = "Receives the parameters required to update an existing property type."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTypeOfPropertiesViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, UpdateTypeOfPropertiesCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (id != command.Id)
                {
                    return BadRequest();
                }
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete a property type",
            Description = "Receives the parameters required to delete an existing property type."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteTypeOfPropertiesByIdCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}


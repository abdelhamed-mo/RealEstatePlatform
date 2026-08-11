using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.Improvements.Commands.CreateImprovements;
using RealEstateApp.Core.Application.Features.Improvements.Commands.DeleteImprovementsById;
using RealEstateApp.Core.Application.Features.Improvements.Commands.UpdateImprovements;
using RealEstateApp.Core.Application.Features.Improvements.Queries.GetAllImprovements;
using RealEstateApp.Core.Application.Features.Improvements.Queries.GetImprovementsById;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Improvements;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Improvements management")]
    public class ImprovementsController : BaseApiController
    {
       
        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet]
        [SwaggerOperation(
           Summary = "List all improvements",
           Description = "Gets all registered improvements."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImprovementsViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                var improvements = await Mediator.Send(new GetAllImprovementsQuery());
                return Ok(improvements);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet("{id}")]
        [SwaggerOperation(
           Summary = "Get improvement by id",
           Description = "Gets an improvement using the id as filter."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveImprovementsViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var improvement = await Mediator.Send(new GetImprovementsByIdQuery { Id = id });
                return Ok(improvement);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [SwaggerOperation(
          Summary = "Create improvement",
          Description = "Receives the parameters required to create a new improvement."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateImprovementsCommand command)
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
               Summary = "Update improvement",
               Description = "Receives the parameters required to update an existing improvement."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveImprovementsViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Update(int id, UpdateImprovementsCommand command)
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
            Summary = "Delete an improvement",
            Description = "Receives the parameters required to delete an existing improvement."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteImprovementsByIdCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

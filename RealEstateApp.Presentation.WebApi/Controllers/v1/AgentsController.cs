using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.Agents.Commands.CreateAgents;
using RealEstateApp.Core.Application.Features.Agents.Queries.GetAgentsById;
using RealEstateApp.Core.Application.Features.Agents.Queries.GetAllAgentProperties;
using RealEstateApp.Core.Application.Features.Agents.Queries.GetAllAgents;
using RealEstateApp.Core.Application.ViewModels.Agents;
using RealEstateApp.Core.Application.ViewModels.Properties;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [SwaggerTag("Agents query")]
    public class AgentsController : BaseApiController
    {
        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet]
        [SwaggerOperation(
           Summary = "List all agents",
           Description = "Gets all registered agents."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentsViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllAgentsQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet("{id}")]
        [SwaggerOperation(
           Summary = "Get agent by id",
           Description = "Gets an agent using the id as filter."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentsViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var agent = await Mediator.Send(new GetAgentsByIdQuery { Id = id });
                return Ok(agent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet("{id}")]
        [SwaggerOperation(
           Summary = "List properties for an agent",
           Description = "Gets all properties for the specified agent id."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertiesViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAgentProperty(string id)
        {
            try
            {
                var agentProperties = await Mediator.Send(new GetAllAgentPropertiesQuery { Id = id });
                return Ok(agentProperties);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [SwaggerOperation(
           Summary = "Change agent status",
           Description = "Performs a status change for an agent."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertiesViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus(ChangeStatusAgentCommand command)
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
    }
}

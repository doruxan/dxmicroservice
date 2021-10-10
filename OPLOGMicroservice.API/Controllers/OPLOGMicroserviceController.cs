using DynamicQueryBuilder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OPLOGMicroservice.Business.Core.Interfaces;
using OPLOGMicroservice.Business.CQRS.Commands;
//using OPLOGMicroservice.Business.CQRS.Common.Queries.QueryEntities;
using OPLOGMicroservice.Infra.DynamicQuery;
using OPLOGMicroservice.Model.OPLOGMicroservice.CreateOPLOGMicroservice;
using OPLOGMicroservice.Model.OPLOGMicroservice.DeleteOPLOGMicroservice;
using OPLOGMicroservice.Model.OPLOGMicroservice.GetOPLOGMicroservice;
using OPLOGMicroservice.Model.OPLOGMicroservice.UpdateOPLOGMicroservice;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    public class OPLOGMicroserviceController : Controller
    {
        private readonly ILogger<OPLOGMicroserviceController> _logger;
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;
        private readonly IEventBus eventBus;

        public OPLOGMicroserviceController(
            ILogger<OPLOGMicroserviceController> logger,
            ICommandBus commandBus,
            IQueryProcessor queryProcessor,
            IEventBus eventBus
            )
        {
            _logger = logger;
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
            this.eventBus = eventBus;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(CreateOPLOGMicroserviceResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOPLOGMicroservice(CreateOPLOGMicroserviceRequest request, CancellationToken cancelliationToken)
        {
            var command = new CreateOPLOGMicroservice { Request = request };
            await commandBus.SendAsync<CreateOPLOGMicroservice, CreateOPLOGMicroserviceResponse>(command, cancelliationToken);
            return StatusCode(201, command.ReturnValue);
        }

        [HttpDelete]
        [Route("")]
        [ProducesResponseType(typeof(DeleteOPLOGMicroserviceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOPLOGMicroservice(DeleteOPLOGMicroserviceRequest request, CancellationToken cancelliationToken)
        {
            var command = new DeleteOPLOGMicroservice { Request = request };
            await commandBus.SendAsync<DeleteOPLOGMicroservice, DeleteOPLOGMicroserviceResponse>(command, cancelliationToken);
            return StatusCode(200, command.ReturnValue);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType(typeof(UpdateOPLOGMicroserviceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOPLOGMicroservice(UpdateOPLOGMicroserviceRequest request, CancellationToken cancelliationToken)
        {
            var command = new UpdateOPLOGMicroservice { Request = request };
            await commandBus.SendAsync<UpdateOPLOGMicroservice, UpdateOPLOGMicroserviceResponse>(command, cancelliationToken);
            return StatusCode(201, command.ReturnValue);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(DynamicQueryOutputDTO<GetOPLOGMicroserviceResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOPLOGMicroservice([FromQuery] DynamicQueryOptions options, CancellationToken cancelliationToken)
        {
            //var query = new QueryEntitiesQuery<OPLOGMicroserviceEntity, GetOPLOGMicroserviceResponse> (options);
            //var result = await queryProcessor.ProcessAsync(query, cancelliationToken);
            return StatusCode(200);
        }
    }
}

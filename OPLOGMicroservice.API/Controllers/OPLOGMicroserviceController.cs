using DynamicQueryBuilder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OPLOGMicroservice.Business.CQRS.Commands;
using OPLOGMicroservice.Business.CQRS.Common.Queries.QueryEntities;
using OPLOGMicroservice.Business.Infra;
using OPLOGMicroservice.Domain;
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
        private readonly IBaseService _baseService;

        public OPLOGMicroserviceController(
            ILogger<OPLOGMicroserviceController> logger,
            IBaseService baseService
            )
        {
            _logger = logger;
            _baseService = baseService;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(CreateOPLOGMicroserviceResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOPLOGMicroservice(CreateOPLOGMicroserviceRequest request, CancellationToken cancelliationToken)
        {
            var command = new CreateOPLOGMicroservice { Request = request };
            var result = await _baseService.SendCommandAsync(command, cancelliationToken);
            return StatusCode(201, result);
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
            var result = await _baseService.SendCommandAsync(command, cancelliationToken);
            return StatusCode(200, result);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType(typeof(UpdateOPLOGMicroserviceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOPLOGMicroservice(UpdateOPLOGMicroserviceRequest request, CancellationToken cancelliationToken)
        {
            var command = new UpdateOPLOGMicroservice { Request = request };
            var result = await _baseService.SendCommandAsync(command, cancelliationToken);
            return StatusCode(201, result);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(DynamicQueryOutputDTO<GetOPLOGMicroserviceResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOPLOGMicroservice([FromQuery] DynamicQueryOptions options, CancellationToken cancelliationToken)
        {
            var query = new QueryEntitiesQuery<OPLOGMicroserviceEntity, GetOPLOGMicroserviceResponse>(options);
            var result = await _baseService.SendQueryAsync(query, cancelliationToken);
            return StatusCode(200, result);
        }
    }
}

using AdvertAPI.Models;
using AdvertAPI.Services;
using Amazon.DynamoDBv2.Model;
using Microsoft.AspNetCore.Mvc;

namespace AdvertAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AdvertController : ControllerBase
    {
        private readonly ILogger<AdvertController> _logger;
        private readonly IAdvertStorageService _advertStorageService;

        public AdvertController(
            ILogger<AdvertController> logger,
            IAdvertStorageService advertStorageService)
        {
            _logger = logger;
            _advertStorageService = advertStorageService;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(404)]
        [ProducesResponseType(201, Type = typeof(CreateAdvertResponse))]
        public async Task<IActionResult> Create(AdvertModel model)
        {
            string recordId = default;
            try
            {
                recordId = await _advertStorageService.Add(model);
            } catch (KeyNotFoundException ex)
            {
                return new NotFoundResult();
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(201, new CreateAdvertResponse { Id = recordId });
        }

        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(CreateAdvertResponse))]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {
            try
            {
                await _advertStorageService.Confirm(model);
            }
            catch (KeyNotFoundException ex)
            {
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return new OkResult();
        }
    }
}
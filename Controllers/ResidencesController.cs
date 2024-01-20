using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResHub.Interfaces;
using ResHub.Models;
using ResHub.Services;
namespace ResHub.Controllers
{
    [Route("api/residences")]
    [ApiController]
    public class ResidencesController : ControllerBase    
    {
        private readonly IResidence _residenceService;
        
        public ResidencesController(IResidence residenceService)
        {
            this._residenceService = residenceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResidenceModel>>> GetResidences()
        {
            try
            {
                var residences = await _residenceService.GetResidences();
                return Ok(residences);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResidenceModel>> GetResidenceDetails(Guid id)
        {
            try
            {
                var residence = await _residenceService.GetResidenceDetails(id);

                if (residence == null)
                {
                    return NotFound($"Residence with id {id} not found.");
                }

                return Ok(residence);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResidenceModel>> AddResidence([FromBody] ResidenceModel residence)
        {
            try
            {
                var addedResidence = await _residenceService.AddResidence(residence);
                return CreatedAtAction(nameof(GetResidenceDetails), new { id = addedResidence.Id }, addedResidence);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ResidenceModel>> UpdateResidence(Guid id, [FromBody] ResidenceModel residence)
        {
            try
            {
                residence.Id = id; // Set the id from the route parameter
                var updatedResidence = await _residenceService.UpdateResidence(residence);

                if (updatedResidence == null)
                {
                    return NotFound($"Residence with id {id} not found.");
                }

                return Ok(updatedResidence);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<(bool, string)>> DeleteResidence(Guid id)
        {
            try
            {
                var residence = await _residenceService.GetResidenceDetails(id);

                if (residence == null)
                {
                    return NotFound($"Residence with id {id} not found.");
                }

                var result = await _residenceService.DeleteResidence(residence);

                if (!result.Item1)
                {
                    return NotFound(result.Item2);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}

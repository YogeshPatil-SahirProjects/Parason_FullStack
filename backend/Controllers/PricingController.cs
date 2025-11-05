using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services;

namespace Parason_Api.Controllers
{    
    public class PricingController : BaseController
    {
        private readonly IPriceService _service;

        public PricingController(IPriceService service)
        {
            _service = service;
        }

        [HttpPost("calculate")]
        public async Task<ActionResult<PriceCalculationResponseDto>> CalculatePrice([FromBody] PriceCalculationRequestDto request)
        {
            try
            {
                var result = await _service.CalculatePriceAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error calculating price", error = ex.Message });
            }
        }

        [HttpGet("model/{modelId}")]
        public async Task<ActionResult<decimal>> GetModelPrice(int modelId)
        {
            try
            {
                var price = await _service.GetLatestPriceForModelAsync(modelId);
                if (price == null)
                    return NotFound(new { message = $"No price found for model {modelId}" });

                return Ok(new { modelId, price });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving price", error = ex.Message });
            }
        }

        [HttpGet("equipment/{equipmentId}")]
        public async Task<ActionResult<decimal>> GetEquipmentPrice(int equipmentId)
        {
            try
            {
                var price = await _service.GetLatestPriceForEquipmentAsync(equipmentId);
                if (price == null)
                    return NotFound(new { message = $"No price found for equipment {equipmentId}" });

                return Ok(new { equipmentId, price });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving price", error = ex.Message });
            }
        }

        [HttpGet("item/{itemId}")]
        public async Task<ActionResult<decimal>> GetItemPrice(int itemId)
        {
            try
            {
                var price = await _service.GetLatestPriceForItemAsync(itemId);
                if (price == null)
                    return NotFound(new { message = $"No price found for item {itemId}" });

                return Ok(new { itemId, price });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving price", error = ex.Message });
            }
        }
    }
}

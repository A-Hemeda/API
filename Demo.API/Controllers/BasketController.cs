using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.BasketServices;
using Services.BasketServices.Dto;

namespace Demo.API.Controllers
{
    
    public class BasketController : BaseController
    {
        private readonly IBasketServices _basketServices;

        public BasketController(IBasketServices basketServices)
        {
            _basketServices = basketServices;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketById(string id)
            => Ok(await _basketServices.GetBasketAsync(id));

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto basket)
            => Ok(await _basketServices.UpdateBasketAsync(basket));
        [HttpDelete]
        public async Task<ActionResult<CustomerBasketDto>> DeleteBasketAsync(string id)
            => Ok(await _basketServices.DeleteBasketAsync(id));
    }
}

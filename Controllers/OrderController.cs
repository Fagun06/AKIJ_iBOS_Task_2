using CRUD.DTO;
using CRUD.Helper;
using CRUD.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _IOrderRepo;

        public OrderController (IOrder iOrderRepo)
        {
            _IOrderRepo = iOrderRepo;
        }


        //Api 00

        [Route("CreateProduct")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO productDTO)
        {
            var result = await _IOrderRepo.CreateProduct(productDTO);
            return Ok(result);
        }

        //API 01

        [Route("CreateOrder")]
        [HttpPost]

        public async Task<IActionResult> CreateOrder(OrderDTO order)
        {
            var result = await _IOrderRepo.CreateOrder(order);
            return Ok(result);
        }
    }
}

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

       
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductDTO productDTO)
        {
            var result = await _IOrderRepo.CreateProduct(productDTO);
            return Ok(result);
        }

        //API 01

        
        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrderDTO order)
        {
            var result = await _IOrderRepo.CreateOrder(order);
            return Ok(result);
        }

        //API 02
        [HttpPut]
        [Route("IncreaseQuantityOrder")]
        public async Task<IActionResult> IncreaseQuantityOrder(int orderId , int Quantiy)
        {
            var result =await _IOrderRepo.IncreaseQuantityOrder(orderId, Quantiy);

            return Ok(result);
        }

        //API 03

        [HttpDelete]

        [Route("DeleteOrder")]

        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var result = await _IOrderRepo.DeleteOrder(orderId);

            return Ok(result);

        }

        //API 04

        [HttpGet]

        [Route("GetAllOrdersWithProductDetails")]

        public async Task<IActionResult> GetAllOrdersWithProductDetails()
        {
            var result = await _IOrderRepo.GetAllDetailsOrderWithProduct();
            return Ok(result);
        }


    }
}

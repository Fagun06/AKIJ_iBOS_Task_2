using CRUD.DBContext;
using CRUD.DTO;
using CRUD.Helper;
using CRUD.IRepository;
using CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Repository
{
    public class Order : IOrder
    {

        private readonly AppDbContext _context;


        public Order (AppDbContext context)
        {
            _context = context;
        }

        public async Task<MassageHelper> CreateProduct(ProductDTO product)
        {
            try
            {
                var newProduct = new TblProduct
                {
                    StrProductName = product.ProductName,
                    NumUnitPrice = product.Price,
                    NumStock = product.Stock,
                };

                await _context.TblProducts.AddAsync(newProduct);
                await _context.SaveChangesAsync();

                return new MassageHelper
                {
                    massage = "Successfully add Product",
                    statusCode = 200,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        //API 01

        public async Task<MassageHelper> CreateOrder(OrderDTO order)
        {

            try
            {
                var productNameAfterTriming = order.ProductName.Trim();
                var custormerNameAfterTriming = order.CustomerName.Trim();

                // Find Product

                var product = await _context.TblProducts.FirstOrDefaultAsync(p => p.StrProductName == productNameAfterTriming);

                if (product == null)
                {
                    throw new Exception("Product Not Found");
                }

                if (product.NumStock < order.Quntity)
                {
                    throw new Exception("stock not available");
                }

                if (order.Quntity < 0)
                {
                    throw new Exception("you need Quantity geater than 0");
                }

                var newOrder = new TblOrder
                {
                    
                    IntProductId = product.IntProductId,
                    StrCustomerName = order.CustomerName,
                    NumQuantity = order.Quntity,
                    DtOrderDate = DateTime.Now,
                    IsActive = true,
                    DtLastActiveDateTime = DateTime.Now,
                };

                await _context.TblOrders.AddAsync(newOrder);

                product.NumStock -= order.Quntity;

                await _context.SaveChangesAsync();

                return new OrderMassageHelper
                {
                    massage = "Order Success",
                    statusCode = 200,
                    NewOrderId = newOrder.IntOrderId,
                };
            }
            catch (Exception )
            {
                throw ;
            }
           

            
        }
    }
}

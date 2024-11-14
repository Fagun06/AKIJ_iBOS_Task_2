using CRUD.DBContext;
using CRUD.DTO;
using CRUD.Helper;
using CRUD.IRepository;
using CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<MessageHelper> CreateProduct(ProductDTO product)
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

                return new MessageHelper
                {
                    message = "Successfully add Product",
                    statusCode = 200,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        //API 01

        public async Task<MessageHelper> CreateOrder(OrderDTO order)
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

                return new OrderMessageHelper
                {
                    message = "Order Success",
                    statusCode = 200,
                    NewOrderId = newOrder.IntOrderId,
                };
            }
            catch (Exception )
            {
                throw ;
            }
           

            
        }

        //API 02
       

        public async Task<MessageHelper> IncreaseQuantityOrder(int Orderid, int Quantity)
        {

            try
            {
                var order = await _context.TblOrders.FirstOrDefaultAsync(o => o.IntOrderId == Orderid);

                if (order == null)
                {
                    throw new Exception("Order not found");
                }

                var product = await _context.TblProducts.FirstOrDefaultAsync(p => p.IntProductId == order.IntProductId);

                if (product == null)
                {
                    throw new Exception("Product is not found");
                }

                if (product.NumStock < Quantity)
                {
                    throw new Exception("Stock not available");
                }

                order.NumQuantity += Quantity;
                product.NumStock -= Quantity;
                order.DtLastActiveDateTime = DateTime.Now;
                _context.TblOrders.Update(order);
                _context.TblProducts.Update(product);
                await _context.SaveChangesAsync();

                return new MessageHelper
                {
                    message = "Update Successfull",
                    statusCode = 200,
                };
            }
            
            catch 
            { 
                throw;
            }

        }

        //API 03 
        public async Task<MessageHelper> DeleteOrder(int OrderId)
        {
            try
            {
                var order = await _context.TblOrders.FirstOrDefaultAsync(o => o.IntOrderId == OrderId);

                if (order == null)
                {
                    throw new Exception("Order not found");
                }

                var product = await _context.TblProducts.FirstOrDefaultAsync(p => p.IntProductId == order.IntProductId);

                if (product == null)
                {
                    throw new Exception("Product is not found");
                }

                product.NumStock += order.NumQuantity;
                order.IsActive = false;
                order.DtLastActiveDateTime = DateTime.Now;
                _context.TblOrders.Update(order);
                await _context.SaveChangesAsync();

                return new MessageHelper
                {
                    message = "Order is deleted and stock updated successfully.",
                    statusCode = 200
                };
            }
            catch { 
                throw;
            }
        }
        //API 4
        public async Task<List<OrderDetailsDTO>> GetAllDetailsOrderWithProduct()
        {
            try
            {
                var orders = await(from order in _context.TblOrders
                                   join product in _context.TblProducts on order.IntProductId equals product.IntProductId
                                   where order.IsActive
                                   select new OrderDetailsDTO
                                   {
                                       OrderId = order.IntOrderId,
                                       OrderDate = order.DtOrderDate,
                                       Quantity = order.NumQuantity,
                                       ProductName = product.StrProductName,
                                       UnitPrice = product.NumUnitPrice,
                                       CustomerName = order.StrCustomerName
                                   }).ToListAsync();
                if (!orders.Any())
                {
                    throw new Exception("No Order Found.");
                }
                return orders;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProductSummaryDTO>> GetProductSummary()
        {
            try
            {
                var ordersWithProducts = await(from order in _context.TblOrders

                                               join product in _context.TblProducts on order.IntProductId equals product.IntProductId
                                               group new { order, product } by new { product.StrProductName, product.NumUnitPrice } into gr
                                               select new ProductSummaryDTO
                                               {
                                                   ProductName = gr.Key.StrProductName,
                                                   TotalQuantityOrdered = gr.Sum(x => x.order.NumQuantity),
                                                   TotalRevenue = gr.Sum(x => x.order.NumQuantity) * gr.Key.NumUnitPrice
                                               }).ToListAsync();

                return ordersWithProducts;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

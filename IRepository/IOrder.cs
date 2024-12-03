using CRUD.DTO;
using CRUD.Helper;

namespace CRUD.IRepository
{
    public interface IOrder
    {
        Task<MessageHelper> CreateProduct(ProductDTO product);

        Task<MessageHelper> CreateOrder(OrderDTO order);

        Task<MessageHelper> IncreaseQuantityOrder(int Orderid, int Quantity);

        Task<MessageHelper> DeleteOrder(int OrderId);

        Task<List<OrderDetailsDTO>> GetAllDetailsOrderWithProduct();

        Task<List<ProductSummaryDTO>> GetProductSummary();

        Task<List<ProductDTO>> GetProductsBelowQuantity(decimal quantity);
        Task<List<Top3CustomersDTO>> GetTop3CustomersByQuantity();
        Task<List<string>> GetUnorderedProducts();

        Task<MessageHelper> CreateBulkOrders(List<OrderDTO>orders);

        Task<List<ProductDTO>> GetAllProduct();

    }
}

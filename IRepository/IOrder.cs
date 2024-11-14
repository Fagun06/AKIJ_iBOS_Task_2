using CRUD.DTO;
using CRUD.Helper;

namespace CRUD.IRepository
{
    public interface IOrder
    {
        Task<MassageHelper> CreateProduct(ProductDTO product);

        Task<MassageHelper> CreateOrder(OrderDTO order);

        
    }
}

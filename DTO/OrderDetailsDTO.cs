namespace CRUD.DTO
{
    public class OrderDetailsDTO
    {
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public int OrderId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

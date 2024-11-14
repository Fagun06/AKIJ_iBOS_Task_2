namespace CRUD.Helper
{
    public class MassageHelper
    {
        public string massage { get; set; }

        public int statusCode { get; set; }
    }

    public class OrderMassageHelper : MassageHelper
    {
        public int NewOrderId { get; set; }
    }
}

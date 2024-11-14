namespace CRUD.Helper
{
    public class MessageHelper
    {
        public string message { get; set; }

        public int statusCode { get; set; }
    }

    public class OrderMessageHelper : MessageHelper
    {
        public int NewOrderId { get; set; }
    }
}

namespace Pinewood.DMSSample.Business
{
    public class PartInvoice
    {
        public PartInvoice(string stockCode, int quantity, int customerID)
            : this(-1, stockCode, quantity, customerID)
        {
        }
        public PartInvoice(int id, string stockCode, int quantity, int customerID)
        {
            ID = id;
            StockCode = stockCode;
            Quantity = quantity;
            CustomerID = customerID;
        }

        public int ID { get; set; }
        public string StockCode { get; set; }
        public int Quantity { get; set; }
        public int CustomerID { get; set; }
    }
}

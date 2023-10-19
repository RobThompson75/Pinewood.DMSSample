namespace Pinewood.DMSSample.Business
{
    public class DMSClient
    {
        private Interfaces.IPartInvoiceController __Controller;

        public DMSClient(Interfaces.IPartInvoiceController controller)
        {
            __Controller = controller;
        }

        public async Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode, int quantity, string customerName)
        {
            return await __Controller.CreatePartInvoiceAsync(stockCode, quantity, customerName);
        }
    }
}
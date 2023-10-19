namespace Pinewood.DMSSample.Business
{
    public class PartInvoiceController: Interfaces.IPartInvoiceController, IDisposable
    {
        Interfaces.ICustomerRepositoryDB _CustomerRepository;
        Interfaces.IPartAvailabilityClient _PartAvailabilityService;
        Interfaces.IPartInvoiceRepositoryDB _PartInvoiceRepository;

        public PartInvoiceController(Interfaces.ICustomerRepositoryDB customerRepository, 
            Interfaces.IPartAvailabilityClient partAvailabilityService,
            Interfaces.IPartInvoiceRepositoryDB partInvoiceRepository)
        {
            _CustomerRepository = customerRepository;
            _PartAvailabilityService = partAvailabilityService;
            _PartInvoiceRepository = partInvoiceRepository;
        }

        public async Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode, int quantity, string customerName)
        {
            if (string.IsNullOrEmpty(stockCode))
            {
                return new CreatePartInvoiceResult(false);
            }

            if (quantity <= 0)
            {
                return new CreatePartInvoiceResult(false);
            }

            Customer? _Customer = _CustomerRepository.GetByName(customerName);
            int _CustomerID = _Customer?.ID ?? 0;
            if (_CustomerID <= 0)
            {
                return new CreatePartInvoiceResult(false);
            }

            // Replaced the using to try finally as an equivalent
            try
            {
                int _Availability = await _PartAvailabilityService.GetAvailability(stockCode);
                if (_Availability <= 0)
                {
                    return new CreatePartInvoiceResult(false);
                }
            }
            finally
            {
                _PartAvailabilityService.Dispose();
            }

            PartInvoice _PartInvoice = new PartInvoice(
                stockCode: stockCode,
                quantity: quantity,
                customerID: _CustomerID
            );

            _PartInvoiceRepository.Add(_PartInvoice);

            return new CreatePartInvoiceResult(true);
        }

        /// <summary>
        /// This is an extra part to ensure the disposable object is disposed
        /// </summary>
        public void Dispose()
        {
            _PartAvailabilityService.Dispose();
        }
    }
}

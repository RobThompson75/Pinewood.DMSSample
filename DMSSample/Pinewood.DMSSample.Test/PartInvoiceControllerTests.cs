using Moq;
using Pinewood.DMSSample.Business;

namespace Pinewood.DMSSample.Test
{
    [TestClass]
    public class PartInvoiceControllerTests
    {
        private Pinewood.DMSSample.Business.PartInvoiceController _controller;

        private Moq.Mock<Business.Interfaces.IPartInvoiceRepositoryDB> _partInvoiceRepo = new Moq.Mock<Business.Interfaces.IPartInvoiceRepositoryDB>();
        private Moq.Mock<Business.Interfaces.ICustomerRepositoryDB> _customerRepo = new Moq.Mock<Business.Interfaces.ICustomerRepositoryDB>();
        private Moq.Mock<Business.Interfaces.IPartAvailabilityClient> _partAvailabilityClient = new Moq.Mock<Business.Interfaces.IPartAvailabilityClient>();


        [TestMethod]
        public async Task TestEmptyStockCode()
        {
            _controller = new Business.PartInvoiceController(_customerRepo.Object, _partAvailabilityClient.Object, _partInvoiceRepo.Object);

            var result = await _controller.CreatePartInvoiceAsync(string.Empty, 0, string.Empty);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public async Task TestInvalidQuantityLessThanZero()
        {
            _controller = new Business.PartInvoiceController(_customerRepo.Object, _partAvailabilityClient.Object, _partInvoiceRepo.Object);

            var result = await _controller.CreatePartInvoiceAsync("Test", -1, string.Empty);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public async Task TestInvalidQuantityZero()
        {
            _controller = new Business.PartInvoiceController(_customerRepo.Object, _partAvailabilityClient.Object, _partInvoiceRepo.Object);

            var result = await _controller.CreatePartInvoiceAsync("Test", 0, string.Empty);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public async Task TestEmptyCustomerName()
        {
            _controller = new Business.PartInvoiceController(_customerRepo.Object, _partAvailabilityClient.Object, _partInvoiceRepo.Object);

            var result = await _controller.CreatePartInvoiceAsync("Test", 1, string.Empty);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public async Task TestInvalidCustomerName()
        {
            const string customerName = "Unknown";

            _customerRepo.Setup(c => c.GetByName(customerName)).Returns(new Customer(-1, string.Empty, string.Empty));
            _controller = new Business.PartInvoiceController(_customerRepo.Object, _partAvailabilityClient.Object, _partInvoiceRepo.Object);

            var result = await _controller.CreatePartInvoiceAsync("Test", 1, customerName);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public async Task TestNoPartAvailability()
        {
            const string customerName = "Known";
            const string stockCode = "Test";

            _customerRepo.Setup(c => c.GetByName(customerName)).Returns(new Customer(11, string.Empty, string.Empty));
            _partAvailabilityClient.Setup(p => p.GetAvailability(stockCode)).ReturnsAsync(0);
            _controller = new Business.PartInvoiceController(_customerRepo.Object, _partAvailabilityClient.Object, _partInvoiceRepo.Object);

            var result = await _controller.CreatePartInvoiceAsync(stockCode, 1, customerName);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public async Task TestPositiveResult()
        {
            const string customerName = "Known";
            const string stockCode = "Test";

            _customerRepo.Setup(c => c.GetByName(customerName)).Returns(new Customer(11, string.Empty, string.Empty));
            _partAvailabilityClient.Setup(p => p.GetAvailability(stockCode)).ReturnsAsync(1);
            _controller = new Business.PartInvoiceController(_customerRepo.Object, _partAvailabilityClient.Object, _partInvoiceRepo.Object);

            var result = await _controller.CreatePartInvoiceAsync(stockCode, 1, customerName);

            Assert.IsTrue(result.Success);
        }
    }
}
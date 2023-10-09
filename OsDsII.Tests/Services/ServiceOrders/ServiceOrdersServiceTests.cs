using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OsDsII.api.DAL.Repositories.Customers;
using OsDsII.api.DAL.Repositories.ServiceOrders;
using OsDsII.api.Services.ServiceOrders;
using OsDsII.DAL.UnitOfWork;
using OsDsII.api.Models;
using OsDsII.api.DTO;
using OsDsII.api.Exceptions;

namespace OsDsII.Tests.Services.ServiceOrders
{
    [TestClass()]
    public class ServiceOrdersServiceTests
    {
        private readonly Mock<ICustomersRepository> _mockCustomersRepository;
        private readonly Mock<IServiceOrdersRepository> _mockServiceOrdersRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ServiceOrdersService _service;

        public ServiceOrdersServiceTests()
        {
            _mockCustomersRepository = new Mock<ICustomersRepository>();
            _mockServiceOrdersRepository = new Mock<IServiceOrdersRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new ServiceOrdersService(_mockServiceOrdersRepository.Object, _mockCustomersRepository.Object, _mockUnitOfWork.Object);
        }

        [TestMethod]
        public async Task ShouldReturnAListOfServiceOrders()
        {
            List<ServiceOrder> expectedServiceOrders = new List<ServiceOrder>
            {
                new ServiceOrder { Id = 100001, Customer = new Customer { Id = 1, Name = "John Doe", Phone = "+5511999999999", Email = "johndoe@mail.com" }, OpeningDate = DateTimeOffset.Now, Description="Pegou fogo",},
                new ServiceOrder { Id = 100002, Customer = new Customer { Id = 1, Name = "John Doe", Phone = "+5511999999999", Email = "johndoe@mail.com" }, OpeningDate = DateTimeOffset.UtcNow, Description="Pegou fogo",},
            };
            _mockServiceOrdersRepository.Setup(repo => repo.GetAllServiceOrdersAsync()).ReturnsAsync(expectedServiceOrders);

            IEnumerable<ServiceOrderDTO> serviceOrdersDtoMock = expectedServiceOrders.Select(s => s.ToServiceOrder());

            var result = await _service.GetServiceOrdersAsync();

            CollectionAssert.AreEqual(serviceOrdersDtoMock.ToList(), result.ToList());
        }

        [TestMethod]
        public async Task ShouldReturnAValidServiceOrderIfIdExists()
        {
            const int serviceOrderId = 100005;
            ServiceOrder expectedServiceOrder = new ServiceOrder

            { Id = serviceOrderId, Customer = new Customer { Id = 1, Name = "John Doe", Phone = "+5511999999999", Email = "johndoe@mail.com" }, OpeningDate = DateTimeOffset.Now, Description = "Pegou fogo", };
            _mockServiceOrdersRepository.Setup(repo => repo.GetServiceOrderByIdAsync(serviceOrderId)).ReturnsAsync(expectedServiceOrder);

            ServiceOrderDTO serviceOrdersDtoMock = expectedServiceOrder.ToServiceOrder();

            var result = await _service.GetServiceOrderByIdAsync(serviceOrderId);

            Assert.AreEqual(serviceOrdersDtoMock, result);
        }

        [TestMethod]
        public void ShouldThrowANotFoundExceptionIfServiceOrderIdDontExists()
        {
            const int serviceOrderId = 100005;
            _mockServiceOrdersRepository.Setup(repo => repo.GetServiceOrderByIdAsync(serviceOrderId)).ReturnsAsync((ServiceOrder)null);

            Assert.ThrowsExceptionAsync<NotFoundException>(() => _service.GetServiceOrderByIdAsync(2));
        }
    }
    // Assert.AreEqual falhou.
    // //Esperado:<System.Linq.Enumerable+SelectListIterator`2[OsDsII.api.Models.ServiceOrder,OsDsII.api.DTO.ServiceOrderDTO]>.
    // Real:<System.Linq.Enumerable+SelectListIterator`2[OsDsII.api.Models.ServiceOrder,OsDsII.api.DTO.ServiceOrderDTO]>. 
}


/*
 private readonly Mock<ICustomersRepository> _mockRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CustomersService _service;

    public CustomersServiceTests()
    {
        _mockRepository = new Mock<ICustomersRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _service = new CustomersService(_mockRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCustomers()
    {
        List<Customer> expectedCustomers = new List<Customer>
        {
            new Customer {Id = 1, Name = "John Doe", Phone = "+5511999999999", Email = "johndoe@mail.com"},
            new Customer {Id = 1, Name = "Jolyne", Phone = "+5521988874665", Email = "jolyne@mail.com"}
        };

        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedCustomers);

        var result = await _service.GetAllAsync();

        Assert.Equal(expectedCustomers, result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnsCustomerById()
    {
        Customer expectedCustomer = new Customer { Id = 1, Name = "John Doe", Phone = "+5511999999999", Email = "johndoe@mail.com" };

        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(expectedCustomer);

        Customer result = await _service.GetByIdAsync(1);

        Assert.Equal(expectedCustomer, result);
    }

    [Fact]
    public async Task GetByIdAsync_ThrowsNotFoundException()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Customer)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(4942615));
    }
 */


/*
 private readonly Mock<ICustomersService> _mockService;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _mockService = new Mock<ICustomersService>();
            _controller = new CustomersController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOk()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John" },
                new Customer { Id = 2, Name = "Jane" },
            };

            _mockService.Setup(svc => svc.GetAllAsync()).ReturnsAsync(customers);

            // Act
            IActionResult result = await _controller.GetAllAsync();

            // Assert
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);

            // If you want to further check the body content
            var body = Assert.IsType<HttpResponseApi<IEnumerable<CustomerDTO>>>(objectResult.Value);
            Assert.Equal(customers.Count, body.Data.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsOk()
        {
            // Arrange
            Customer customer = new Customer { Id = 1, Name = "John" };

            _mockService.Setup(svc => svc.GetByIdAsync(1)).ReturnsAsync(customer);

            // Act
            IActionResult result = await _controller.GetByIdAsync(1);

            // Assert
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);

            // If you want to further check the body content
            var body = Assert.IsType<HttpResponseApi<CustomerDTO>>(objectResult.Value);
            Assert.Equal(customer.Id, body.Data.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNotFound()
        {
            var exception = new BaseException("NotFound", "Customer not found", HttpStatusCode.NotFound, 404, "/api/v1/Customers/1", DateTimeOffset.UtcNow, null);
            // Arrange
            _mockService.Setup(svc => svc.GetByIdAsync(1)).ThrowsAsync(exception);

            // Act
            IActionResult result = await _controller.GetByIdAsync(1);

            // Assert
            ContentResult contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal((int)HttpStatusCode.NotFound, contentResult.StatusCode);

            // If you want to further check the body content
            var body = JsonConvert.DeserializeObject<HttpErrorResponse>(contentResult.Content);
            Assert.Equal("NotFound", body.ErrorCode);
        }
    }
 
 */
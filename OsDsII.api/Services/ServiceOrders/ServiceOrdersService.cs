using OsDsII.DAL.Repositories;
using OsDsII.DAL.UnitOfWork;
using OsDsII.DTOS;
using OsDsII.Models;
using OsDsII.Exceptions;
namespace OsDsII.Services;
public class ServiceOrdersService : IServiceOrdersService
{
    private readonly IServiceOrdersRepository _serviceOrdersRepository;
    private readonly ICustomersRepository _customersRepository;
    private readonly IUnitOfWork _uow;
    public ServiceOrdersService(IServiceOrdersRepository serviceOrdersRepository, ICustomersRepository customersRepository, IUnitOfWork uow)
    {
        _serviceOrdersRepository = serviceOrdersRepository;
        _customersRepository = customersRepository;
        _uow = uow;
    }

    public async Task<IEnumerable<ServiceOrderDTO>> GetServiceOrdersAsync()
    {
        var serviceOrders = await _serviceOrdersRepository.GetAllServiceOrdersAsync();
        return serviceOrders.Select(s => s.ToServiceOrder());
    }

    public async Task<ServiceOrderDTO> GetServiceOrderByIdAsync(int id)
    {
        ServiceOrder serviceOrder = await _serviceOrdersRepository.GetServiceOrderByIdAsync(id);
        if (serviceOrder is null)
        {
            throw new NotFoundException("Service Order");
        }
        ServiceOrderDTO serviceOrderDto = serviceOrder.ToServiceOrder();

        return serviceOrderDto;
    }

    public async Task<ServiceOrderDTO> SaveServiceOrder(ServiceOrderInput serviceOrderInput)
    {
        if (serviceOrderInput is null)
        {
            throw new BadRequestException("Service order cannot be null");
        }

        Customer customer = await _customersRepository.GetByIdAsync(serviceOrderInput.Id);

        if (customer is null)
        {
            throw new NotFoundException("Customer");
        }

        // Create a ServiceOrder object from the ServiceOrderInput
        ServiceOrder serviceOrder = ServiceOrder.FromServiceOrderInput(serviceOrderInput, customer);

        // Add to database
        await _serviceOrdersRepository.CreateServiceOrderAsync(serviceOrder);
        await _uow.SaveChangesAsync();

        return serviceOrder.ToServiceOrder();
    }

    public async Task FinishServiceOrderAsync(int id)
    {
        ServiceOrder serviceOrder = await _serviceOrdersRepository.GetServiceOrderByIdAsync(id);

        serviceOrder.FinishOS();
        _serviceOrdersRepository.UpdateServiceOrder(serviceOrder);
        await _uow.SaveChangesAsync();
    }

    public async Task CancelServiceOrderAsync(int id)
    {
        ServiceOrder serviceOrder = await _serviceOrdersRepository.GetServiceOrderByIdAsync(id);

        serviceOrder.Cancel();
        _serviceOrdersRepository.UpdateServiceOrder(serviceOrder);
        await _uow.SaveChangesAsync();
    }


}
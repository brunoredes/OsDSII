using OsDsII.api.Models;

namespace OsDsII.api.DAL.Repositories.ServiceOrders;
public interface IServiceOrdersRepository
{
    public Task<IEnumerable<ServiceOrder>> GetAllServiceOrdersAsync();
    public Task<ServiceOrder> GetServiceOrderByIdAsync(int id);
    public Task CreateServiceOrderAsync(ServiceOrder serviceOrder);
    public void UpdateServiceOrder(ServiceOrder serviceOrder);
    public Task<ServiceOrder> GetServiceOrderWithComments(int serviceOrderId);
}
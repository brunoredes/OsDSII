using OsDsII.Models;

namespace OsDsII.DAL.Repositories;
public interface IServiceOrdersRepository
{
    public Task<IEnumerable<ServiceOrder>> GetAllServiceOrdersAsync();
    public Task<ServiceOrder> GetServiceOrderByIdAsync(int id);
    public Task CreateServiceOrderAsync(ServiceOrder serviceOrder);
    public void UpdateServiceOrder(ServiceOrder serviceOrder);
}
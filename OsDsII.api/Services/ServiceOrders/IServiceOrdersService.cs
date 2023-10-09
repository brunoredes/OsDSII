using OsDsII.Models;
using OsDsII.DTOS;

namespace OsDsII.Services;
public interface IServiceOrdersService
{
    public Task<IEnumerable<ServiceOrderDTO>> GetServiceOrdersAsync();
    public Task<ServiceOrderDTO> GetServiceOrderByIdAsync(int id);
    public Task<ServiceOrderDTO> SaveServiceOrder(ServiceOrderInput serviceOrder);
    public Task FinishServiceOrderAsync(int id);
    public Task CancelServiceOrderAsync(int id);
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsDsII.Data;
using OsDsII.Models;

namespace OsDsII.DAL.Repositories;

public class ServiceOrdersRepository : IServiceOrdersRepository
{
    private readonly DataContext _dataContext;

    public ServiceOrdersRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<ServiceOrder>> GetAllServiceOrdersAsync()
    {
        return await _dataContext.ServiceOrders.Include(s => s.Customer).ToListAsync();
    }
    public async Task<ServiceOrder> GetServiceOrderByIdAsync(int id)
    {
        return await _dataContext.ServiceOrders
            .Include(s => s.Customer)
            .FirstOrDefaultAsync(s => id == s.Id);
    }

    public async Task CreateServiceOrderAsync(ServiceOrder serviceOrder)
    {
        await _dataContext.ServiceOrders.AddAsync(serviceOrder);
    }

    public void UpdateServiceOrder(ServiceOrder serviceOrder)
    {
        _dataContext.ServiceOrders.Update(serviceOrder);
    }


}
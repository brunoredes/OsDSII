using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsDsII.api.Data;
using OsDsII.api.Models;
using OsDsII.api.Services.ServiceOrders;
using OsDsII.api.DTO;
using OsDsII.api.Exceptions;
using OsDsII.api.http;

namespace OsDsII.api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ServiceOrdersController : ControllerBase
    {
        private readonly IServiceOrdersService _serviceOrdersService;

        public ServiceOrdersController(IServiceOrdersService serviceOrdersService)
        {
            _serviceOrdersService = serviceOrdersService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllServiceOrderAsync()
        {
            try
            {
                IEnumerable<ServiceOrderDTO> serviceOrderList = await _serviceOrdersService.GetServiceOrdersAsync();
                return HttpResponseApi<IEnumerable<ServiceOrderDTO>>.Ok(serviceOrderList);
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceOrderById(int id)
        {
            try
            {
                ServiceOrderDTO serviceOrder = await _serviceOrdersService.GetServiceOrderByIdAsync(id);
                return HttpResponseApi<ServiceOrderDTO>.Ok(serviceOrder);
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceOrderAsync([FromBody] ServiceOrderInput serviceOrderInput)
        {
            ServiceOrderDTO createdServiceOrder = await _serviceOrdersService.SaveServiceOrder(serviceOrderInput);
            return HttpResponseApi<ServiceOrderDTO>.Created(createdServiceOrder);
        }

        [HttpPut("{id}/status/finish")]
        public async Task<IActionResult> FinishServiceOrderAsync(int id)
        {
            try
            {
                await _serviceOrdersService.FinishServiceOrderAsync(id);
                return NoContent();

            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }

        [HttpPut("{id}/status/cancel")]
        public async Task<IActionResult> CancelServiceOrder(int id)
        {
            try
            {
                await _serviceOrdersService.CancelServiceOrderAsync(id);

                return NoContent();
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }
    }
}
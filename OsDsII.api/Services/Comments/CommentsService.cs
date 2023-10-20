using OsDsII.api.DAL.Repositories.ServiceOrders;
using OsDsII.api.DTO;
using OsDsII.api.Exceptions;
using OsDsII.api.Models;
using OsDsII.DAL.UnitOfWork;

namespace OsDsII.api.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly IServiceOrdersRepository _serviceOrdersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommentsService(IServiceOrdersRepository serviceOrdersRepository, IUnitOfWork unitOfWork)
        {
            _serviceOrdersRepository = serviceOrdersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceOrderDetailDTO> GetCommentAsync(int serviceOrderId)
        {
            ServiceOrder serviceOrder = await _serviceOrdersRepository.GetServiceOrderWithComments(serviceOrderId);

            if (serviceOrder is null)
            {
                throw new NotFoundException("OS not found");
            }
           return handleServiceOrderDTO(serviceOrder);
        }

        public async Task<> AddCommentAsync(ServiceOrderInput input)
        {

        }

        private ServiceOrderDetailDTO handleServiceOrderDTO(ServiceOrder serviceOrder)
        {
            return new ServiceOrderDetailDTO
            {
                Id = serviceOrder.Id,
                Description = serviceOrder.Description,
                Price = serviceOrder.Price,
                Status = serviceOrder.Status,
                OpeningDate = serviceOrder.OpeningDate,
                FinishDate = serviceOrder.FinishDate,
                Customer = new CustomerDetailDTO
                {
                    Name = serviceOrder.Customer?.Name
                },
                Comments = serviceOrder.Comments.Select(c => new CommentDetailDTO
                {
                    Description = c.Description,
                    SendDate = c.SendDate
                }).ToList()
            };
        }
    }
}

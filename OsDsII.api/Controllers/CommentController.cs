using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsDsII.api.http;
using OsDsII.api.Models;
using OsDsII.api.Data;
using OsDsII.api.Services.Comments;
using OsDsII.api.DTO;

namespace OsDsII.api.Controllers
{

    [ApiController]
    [Route("api/v1/ServiceOrders/{id}/comment")]
    public class CommentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICommentsService _commentsService;

        public CommentController(DataContext context)
        {
            _context = context;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HttpResponseApi<ServiceOrderDetailDTO>))]
        [HttpGet]
        public async Task<IActionResult> GetCommentsAsync([FromRoute(Name = "id")] int serviceOrderId)
        {
            ServiceOrderDetailDTO serviceOrderDetailDTO = await _commentsService.GetCommentAsync(serviceOrderId);
            return HttpResponseApi<ServiceOrderDetailDTO>.Ok(serviceOrderDetailDTO);
            
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromRoute(Name = "id")] int serviceOrderId, [FromBody] CommentInput commentInput)
        {
            try
            {
                var os = await _context.ServiceOrders.Include(c => c.Customer).FirstOrDefaultAsync(s => serviceOrderId == s.Id);

                if (os == null)
                {
                    return NotFound("ServiceOrder not found.");
                }

                Comment comment = HandleCommentObject(serviceOrderId, commentInput.Description);

                await _context.Comments.AddAsync(comment); // This line adds the comment to the context
                await _context.SaveChangesAsync();

                CommentDTO commentDto = new CommentDTO
                {
                    Id = comment.Id,
                    Description = comment.Description,
                    ServiceOrderId = comment.ServiceOrderId,
                    SendDate = comment.SendDate,
                };


                return Created("Comment created successfully", commentDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private Comment HandleCommentObject(int id, string description)
        {
            Comment comment = new Comment();
            comment.Description = description;
            comment.ServiceOrderId = id;
            return comment;
        }
    }
}


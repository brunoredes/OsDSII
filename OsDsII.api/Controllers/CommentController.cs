using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsDsII.api.http;
using OsDsII.api.Models;
using OsDsII.api.Data;

namespace OsDsII.api.Controllers
{

    [ApiController]
    [Route("api/v1/ServiceOrders/{id}/comment")]
    public class CommentController : ControllerBase
    {
        private readonly DataContext _context;

        public CommentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsAsync([FromRoute(Name = "id")] int serviceOrderId)
        {
            var os = await _context.ServiceOrders
                .Include(c => c.Customer)
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(s => s.Id == serviceOrderId);
            if (os is null)
            {
                return NotFound("OS not found");
            }

            ServiceOrderDetailDTO dto = new ServiceOrderDetailDTO
            {
                Id = os.Id,
                Description = os.Description,
                Price = os.Price,
                Status = os.Status,
                OpeningDate = os.OpeningDate,
                FinishDate = os.FinishDate,
                Customer = new CustomerDetailDTO
                {
                    Name = os.Customer?.Name
                },
                Comments = os.Comments.Select(c => new CommentDetailDTO
                {
                    Description = c.Description,
                    SendDate = c.SendDate
                }).ToList()
            };

            return Ok(new { Data = dto });
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
                    // ServiceOrderDescription = comment.ServiceOrder.Description,
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

public class CommentDTO
{
    public long Id { get; set; }
    public string Description { get; set; }
    public int ServiceOrderId { get; set; }
    public DateTimeOffset SendDate { get; set; }
    public string ServiceOrderDescription { get; set; }
    // Add any other properties from ServiceOrder that you might need in the response
}


public record ServiceOrderDetailDTO
{
    public int Id { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public StatusServiceOrder Status { get; set; } // Assuming StatusServiceOrder is an Enum
    public DateTimeOffset OpeningDate { get; set; }
    public DateTimeOffset FinishDate { get; set; }
    public CustomerDetailDTO Customer { get; set; }
    public List<CommentDetailDTO> Comments { get; set; }
}

public record CustomerDetailDTO
{
    public string Name { get; set; }
}

public record CommentDetailDTO
{
    public string Description { get; set; }
    public DateTimeOffset SendDate { get; set; }
}
using OsDsII.api.DTO;
using OsDsII.api.Models;

namespace OsDsII.api.Services.Comments
{
    public interface ICommentsService
    {
        public Task<ServiceOrderDetailDTO> GetCommentAsync(int id);
        public Task AddCommentAsync(CommentInput input);
        public Comment HandleComment(int id, string description);
    }
}

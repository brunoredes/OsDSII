using OsDsII.api.Models;

namespace OsDsII.api.DTO
{
    public class ServiceOrderDetailDTO
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
}

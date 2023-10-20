namespace OsDsII.api.DTO
{
    public record CommentDTO
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public int ServiceOrderId { get; set; }
        public DateTimeOffset SendDate { get; set; }
        public string ServiceOrderDescription { get; set; }
    }

}

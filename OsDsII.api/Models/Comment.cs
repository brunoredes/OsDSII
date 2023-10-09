using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace OsDsII.api.Models
{
    [Index(nameof(Id))]
    [PrimaryKey(nameof(Id))]
    [Table("comment")]
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonIgnore]
        [Column("id")]
        public long Id { get; set; }

        [Column("description", TypeName = "text")]
        [StringLength(500)]
        [NotNull]
        public string Description { get; set; } = null!;

        // [Column("service_order_id")]
        public int ServiceOrderId { get; set; }
        public ServiceOrder ServiceOrder { get; set; }

        [NotNull]
        [Column("send_date")]
        [Timestamp]
        public DateTimeOffset SendDate { get; set; } = DateTimeOffset.UtcNow;


        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
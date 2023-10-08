using System.Diagnostics.CodeAnalysis;

namespace OsDsII.Models
{
    public class ServiceOrderInput : BaseEntity
    {
        [NotNull]
        public string Description { get; set; } = "";
        
        [AllowNull]
        public double Price { get; set; }
    }
}
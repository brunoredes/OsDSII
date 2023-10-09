using System.Diagnostics.CodeAnalysis;

namespace OsDsII.api.Models
{
    public class BaseEntity
    {
        [NotNull]
        public int Id { get; set; }
    }
}
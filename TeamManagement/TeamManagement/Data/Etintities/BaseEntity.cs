using System.Diagnostics.CodeAnalysis;

namespace TeamManagement.Data.Etintities
{
    public class BaseEntity
    {
        [NotNull]
        public Guid Id { get; set; }
    }
}

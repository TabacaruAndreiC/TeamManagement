namespace TeamManagement.Data.Etintities
{
    public class TeamOwner:BaseEntity
    {
        public Guid UserId { get; set; } = Guid.Empty;

        public Guid TeamId { get; set; } = Guid.Empty;


    }
}

namespace TeamManagement.Services.Dtos
{
    public class TeamAddTeamOwnerDto
    {
        public Guid Id { get; set; }
        public Guid TeamOwnerId { get; set; } = Guid.Empty;
    }
}

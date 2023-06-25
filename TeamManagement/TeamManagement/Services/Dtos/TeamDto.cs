namespace TeamManagement.Services.Dtos
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid TeamOwnerId { get; set; } = Guid.Empty;
    }
}

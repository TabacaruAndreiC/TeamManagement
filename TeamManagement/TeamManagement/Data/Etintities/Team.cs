namespace TeamManagement.Data.Etintities
{
    public class Team:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public List<Player> Players { get; set; } = new List<Player>();
        public Guid TeamOwnerId { get; set; } = Guid.Empty;
        


    }
}

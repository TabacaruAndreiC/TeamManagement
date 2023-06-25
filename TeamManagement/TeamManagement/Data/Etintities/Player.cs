namespace TeamManagement.Data.Etintities
{
    public class Player:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public Guid TeamId { get; set; } = Guid.Empty;
        public Team? Team { get; set; }

    }
}

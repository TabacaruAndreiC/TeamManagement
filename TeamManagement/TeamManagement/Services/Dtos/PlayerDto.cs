namespace TeamManagement.Services.Dtos
{
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Salary { get; set; }
        public Guid TeamId { get; set; } = Guid.Empty;
        
    }
}

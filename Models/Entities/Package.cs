namespace GymManagementSystem.Models.Entities
{
    public class Package
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public int Duration { get; set; }
        public double fee { get; set; }
    }
}

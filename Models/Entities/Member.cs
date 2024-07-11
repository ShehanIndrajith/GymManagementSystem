using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Models.Entities
{
    public class Member
    {
        
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date")]
        public DateOnly Birthday { get; set; }
        [Required(ErrorMessage = "Telephone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid telephone number")]
        public int TelNO { get; set; }
        [Required(ErrorMessage = "Join date is required")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date")]
        public DateOnly JoinDate { get; set; }
    }
}
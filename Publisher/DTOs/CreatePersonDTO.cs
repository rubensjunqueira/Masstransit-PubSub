using System.ComponentModel.DataAnnotations;

namespace Publisher.DTOs
{
    public class CreatePersonDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}

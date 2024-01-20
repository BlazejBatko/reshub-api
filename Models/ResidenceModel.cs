using System.ComponentModel.DataAnnotations;

namespace ResHub.Models
{
    public class ResidenceModel
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Description { get; set; }

        public string? Cover { get; set; }


    }
}

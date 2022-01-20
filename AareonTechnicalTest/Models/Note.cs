using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public int PersonId { get; set; }

        public int TicketId { get; set; }
    }
}

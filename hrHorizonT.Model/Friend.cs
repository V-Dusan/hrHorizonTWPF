using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrHorizonT.Model
{
    [Table("Friend", Schema = "HR")]
    public class Friend
    {            
        public int Id { get; set; }

        [Required]
        [StringLength(50)]  // ili [MaxLength] koji se koristi kao univezalni ne samo za string
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
    }
}

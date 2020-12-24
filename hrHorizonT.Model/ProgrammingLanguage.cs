using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrHorizonT.Model
{
    [Table("ProgrammingLanguage", Schema = "HR")]
    public class ProgrammingLanguage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}

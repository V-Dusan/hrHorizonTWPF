using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace hrHorizonT.Model
{
    [Table("Drzava", Schema = "HR")]
    public class Drzava
    {
        public object naziv;

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Polje šifra je obavezno")]
        public int? Sifra { get; set; }

        [Required(ErrorMessage = "Polje oznaka je obavezno")]
        [StringLength(3, ErrorMessage = "Maksimalna dužina je 3")]
        public string Oznaka { get; set; }

        [Required(ErrorMessage = "Polje naziv je obavezno")]
        [StringLength(50, ErrorMessage = "Maksimalna dužina je 50")] 
        public string Naziv { get; set; }

        [ConcurrencyCheck]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [NotMapped]
        public uint xmin { get; set; }
    }
}

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

        [Required]
        public int? Sifra { get; set; }

        [Required]
        [StringLength(3)]
        public string Oznaka { get; set; }

        [Required]
        [StringLength(50)] 
        public string Naziv { get; set; }
    }
}

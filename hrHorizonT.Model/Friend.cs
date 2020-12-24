using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrHorizonT.Model
{
    [Table("Friend", Schema = "HR")]
    public class Friend
    {   
        public Friend()
        {
            PhoneNumbers = new Collection<FriendPhoneNumber>();
        }


        public int Id { get; set; }

        [Required]
        [StringLength(50)]  // ili [MaxLength] koji se koristi kao univezalni ne samo za string
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        public int? FavoriteLanguageId { get; set; }

        public ProgrammingLanguage FavoriteLanguage { get; set; }

        public ICollection<FriendPhoneNumber> PhoneNumbers { get; set; }
    }
}

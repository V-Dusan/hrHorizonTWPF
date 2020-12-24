using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrHorizonT.Model
{
    [Table("FriendPhoneNumber", Schema = "HR")]
    public class FriendPhoneNumber
    {
        public int Id { get; set; }

        [Phone]
        [Required]
        public string Number { get; set; }

        public int FriendId { get; set; }

        public Friend Friend { get; set; }
    }
}

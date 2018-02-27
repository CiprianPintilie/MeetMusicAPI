using System.ComponentModel.DataAnnotations.Schema;

namespace MeetMusicModels.Models
{
    [Table("music_user")]
    public class UserMusicModel
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("family_id")]
        public int FamilyId { get; set; }

        [Column("rank")]
        public int Position { get; set; }
    }
}

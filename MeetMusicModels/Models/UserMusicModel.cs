using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetMusicModels.Models
{
    [Table("music_user")]
    public class UserMusicModel
    {
        [Column("user_id"), Key]
        public Guid UserId { get; set; }

        [Column("family_id"), Key]
        [Required]
        public int FamilyId { get; set; }

        [Column("rank")]
        [Required]
        public int Position { get; set; }
    }
}

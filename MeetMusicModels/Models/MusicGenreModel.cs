using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetMusicModels.Models
{
    [Table("music_genre")]
    public class MusicGenreModel
    {

        [Column("genre_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("family_id")]
        public int FamilyId { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}

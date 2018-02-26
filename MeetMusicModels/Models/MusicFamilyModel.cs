using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeetMusicModels.Models
{
    [Table("music_family")]
    public class MusicFamilyModel
    {
        [Column("family_id")]
        public int Id { get; set; }

        [Column("family")]
        public string Name { get; set; }
    }
}

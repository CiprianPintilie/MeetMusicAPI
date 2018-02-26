﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MeetMusicModels.Models
{
    [Table("music_family")]
    public class MusicFamilyModel
    {
        [Column("family_id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}

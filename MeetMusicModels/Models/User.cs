using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetMusicModels.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Column("username")]
        [Required(ErrorMessage = "required")]
        [RegularExpression(@"^[a-zA-Z0-9]{4,20}$",
        ErrorMessage = "Username must be at least 5 characters and not exceed 20 characters")]
        public string Username { get; set; }

        [Column("password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "required")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Password must contains between {2} & {1} characters.")]
        public string Password { get; set; }

        [Column("mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "required")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&’*+\/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$",
            ErrorMessage = "Not a mail adress")]
        public string Email { get; set; }

        [Column("gender")]
        public int Gender { get; set; }

        [Column("avatar_url")]
        public string AvatarUrl { get; set; }

        [Column("phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("longitude")]
        public string Longitude { get; set; }

        [Column("latitude")]
        public string Latitude { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetMusicModels.Models
{
    [Table("user")]
    public class UserModel
    {
        [Key]
        [Column("user_id")]
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
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Password must contains between {2} & {1} characters.")]
        public string Password { get; set; }

        [Column("firstname")]
        [Required(ErrorMessage = "required")]
        [RegularExpression(@"^[a-zA-Z]{2,40}$",
            ErrorMessage = "Firstname must be at least 2 characters and not exceed 40 characters")]
        public string FirstName { get; set; }

        [Column("lastname")]
        [Required(ErrorMessage = "required")]
        [RegularExpression(@"^[a-zA-Z]{2,40}$",
            ErrorMessage = "Lastname must be at least 2 characters and not exceed 40 characters")]
        public string LastName { get; set; }

        [Column("mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "required")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&’*+\/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$",
            ErrorMessage = "Not a mail adress")]
        public string Email { get; set; }
        
        [Column("gender")]
        [Required(ErrorMessage = "required")]
        [RegularExpression(@"^[1-2]$", ErrorMessage = "Gender must be an integer between 1 and 2")]
        public int Gender { get; set; }

        [Column("avatar_url")]
        public string AvatarUrl { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("birth_date")]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("longitude")]
        public string Longitude { get; set; }

        [Column("latitude")]
        public string Latitude { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetMusicModels.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "required")]
        [RegularExpression(@"^[a-zA-Z0-9]{4,20}$",
        ErrorMessage = "Username must be at least 5 characters and not exceed 20 characters")]
        public string Username { get; set; }


        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "required")]
        [RegularExpression(@"^([a-zA-Z0-9_-.]+)@(([[0-9]{1,3}" +
                           @".[0-9]{1,3}.[0-9]{1,3}.)|(([a-zA-Z0-9-]+" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(]?)$",
        ErrorMessage = "Not a mail adress")]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "required")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Password must contains between {2} & {1} characters.")]
        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }
    }
}

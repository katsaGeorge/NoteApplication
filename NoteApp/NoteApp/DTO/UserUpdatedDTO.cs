using System.ComponentModel.DataAnnotations;

namespace NoteApp.DTO
{
    public class UserUpdatedDTO : BaseDTO
    {
        [Required(ErrorMessage = "The {0} field is required")]
        [MinLength(4, ErrorMessage = "The {0} must be minimun of {1} characters")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Spaces are not allowed")]
        public string? Username {get; set;}

        [Required(ErrorMessage = "The {0} field is required")]
        [MinLength(4, ErrorMessage = "The {0} must be minimun of {1} characters")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Spaces are not allowed")]
        public string? Password { get; set;}

    }
}

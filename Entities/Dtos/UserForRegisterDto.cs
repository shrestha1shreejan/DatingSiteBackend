using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage = "Username is required")]       
        public string Username { get; set; }

        [Required(ErrorMessage = "password is required field")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must suppli password between 4 and 8 characters")]
        public string Password { get; set; }
    }
}

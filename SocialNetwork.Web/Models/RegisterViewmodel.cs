using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Senha")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Confirme senha")]
        //[DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
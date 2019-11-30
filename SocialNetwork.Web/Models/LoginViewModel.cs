using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Usuário (e-mail)")]
        public string Username { get; set; }

        [Required]
        [DisplayName("Senha")]
        //[DataType(DataType.Password)] só descomentar quando tiver tudo pronto, facilita a vida
        public string Password { get; set; }
    }
}
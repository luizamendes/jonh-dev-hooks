using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SocialNetwork.Web.Models
{
    public class ProfileViewModel
    {
        [DisplayName("Nome)")]
        public string FirstName { get; set; }

        [DisplayName("Sobrenome)")]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
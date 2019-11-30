using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SocialNetwork.Web.Models
{
    public class PhotoViewModel
    {
        public int PhotoId { get; set; }
        [DisplayName("Descrição")]
        public string PhotoName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
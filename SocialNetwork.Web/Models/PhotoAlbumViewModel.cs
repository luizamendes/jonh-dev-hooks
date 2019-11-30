using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SocialNetwork.Web.Models
{
    public class PhotoAlbumViewModel
    {
        public int PhotoAlbumId { get; set; }

        [DisplayName("Nome")]
        public string AlbumName { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Models
{
    public class PhotoAlbum
    {
        public int PhotoAlbumId { get; set; }
        public string AlbumName { get; set; }
        public string Description { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}

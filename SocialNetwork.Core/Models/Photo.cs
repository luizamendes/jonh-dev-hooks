using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string PhotoName { get; set; }
        public string PhotoUrl { get; set; }
        public virtual int PhotoAlbumId { get; set; }
    }
}

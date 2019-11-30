using System.Collections.Generic;

namespace SocialNetwork.Core.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
        public string AccountId { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<PhotoAlbum> PhotoAlbums { get; set; }
    }
}

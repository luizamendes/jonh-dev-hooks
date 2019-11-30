using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public string AccountId { get; set; }
    }
}

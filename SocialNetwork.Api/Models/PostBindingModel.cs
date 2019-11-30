using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Api.Models
{
    public class PostBindingModel
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Api.Models
{
    public class ProfileBindingModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
        public string AccountId { get; set; }
    }
}
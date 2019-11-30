using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SocialNetwork.Api
{
    public class DataContext : DbContext
    {
        public DataContext() : base("ProfileConnection")
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<SocialNetwork.Api.Models.ProfileBindingModel> ProfileBindingModels { get; set; }
    }
}
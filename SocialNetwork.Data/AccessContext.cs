using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SocialNetwork.Data
{
    public class AccessContext : DbContext
    {
        public AccessContext() : base("ProfileConnection")
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        //public DbSet<Post> Posts { get; set; }
        public DbSet<PhotoAlbum> PhotoAlbums { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<SocialNetwork.Core.Models.Post> Posts { get; set; }
    }
}

using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;

namespace SocialNetwork.Data.Repositories
{
    public class PostRepository
    {
        private AccessContext db = new AccessContext();

        public IQueryable<Post> GetAll()
        {
            return db.Posts;
        }

        public Post GetPostById(int id)
        {
            return db.Posts.Find(id);
        }

        public void Create(Post post)
        {
            db.Posts.Add(post);
            db.SaveChanges();
        }

        public void Delete(Post post)
        {
            db.Posts.Remove(post);
            db.SaveChanges();
        }

        public void Put(Post post)
        {
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool PostExists(int id)
        {
            return db.Posts.Count(e => e.PostId == id) > 0;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
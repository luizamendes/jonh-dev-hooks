using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SocialNetwork.Core.Models;

namespace SocialNetwork.Data.Repositories
{
    public class ProfileRepository
    {
        private AccessContext db = new AccessContext();

        public IQueryable<Profile> GetAll()
        {
            return db.Profiles;
        }

        public Profile GetProfileById(int id)
        {
            return db.Profiles.Find(id);
        }

        public void Create(Profile profile)
        {
            db.Profiles.Add(profile);
            db.SaveChanges();
        }

        public void Delete(Profile profile)
        {
            db.Profiles.Remove(profile);
            db.SaveChanges();
        }

        public void Put(Profile profile)
        {
            db.Entry(profile).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool ProfileExists(int id)
        {
            return db.Profiles.Count(e => e.Id == id) > 0;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}

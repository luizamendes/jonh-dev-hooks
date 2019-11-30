using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SocialNetwork.Core.Models;
using SocialNetwork.Data;

namespace SocialNetwork.Api.Controllers
{
    public class PhotosController : ApiController
    {
        private AccessContext db = new AccessContext();

        // GET: api/Photos
        public IQueryable<Photo> GetPhotos()
        {


            var lista = from album in db.PhotoAlbums
                        join photo in db.Photos
                        on album.PhotoAlbumId equals 1
                        where 

            var lista2 = db.PhotoAlbums.Single(p => p.PhotoAlbumId == 1).Photos;

            //.Where(sc => sc.PhotoAlbumId == 1)

            return db.Photos;
        }

        // GET: api/Photos? PhotoAlbum = 5
        public IQueryable<Photo> GetPhotos(int id)
        {
            var lista = db.Photos.Join(
                db.PhotoAlbums,
                photo => photo.PhotoId,
                photoAlbum => photoAlbum.PhotoAlbumId,
                (photo, photoAlbum) => new
                {
                    PhotoId = photo.PhotoId,
                    PhotoName = photo.PhotoName,
                    PhotoURL = photo.PhotoUrl,
                    PhotoAlbumId = photoAlbum.PhotoAlbumId
                }).
                Where(sc => sc.PhotoAlbumId == id).ToList();

            return null;
        }

        // GET: api/Photos/5
        [ResponseType(typeof(Photo))]
        public IHttpActionResult GetPhoto(int id)
        {
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return NotFound();
            }

            return Ok(photo);
        }

        // PUT: api/Photos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhoto(int id, Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != photo.PhotoId)
            {
                return BadRequest();
            }

            db.Entry(photo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Photos
        [ResponseType(typeof(Photo))]
        public IHttpActionResult PostPhoto(Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Photos.Add(photo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = photo.PhotoId }, photo);
        }

        // DELETE: api/Photos/5
        [ResponseType(typeof(Photo))]
        public IHttpActionResult DeletePhoto(int id)
        {
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return NotFound();
            }

            db.Photos.Remove(photo);
            db.SaveChanges();

            return Ok(photo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhotoExists(int id)
        {
            return db.Photos.Count(e => e.PhotoId == id) > 0;
        }
    }
}
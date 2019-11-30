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
    [RoutePrefix("api/PhotoAlbums")]
    public class PhotoAlbumsController : ApiController
    {
        private AccessContext db = new AccessContext();

        // GET: api/PhotoAlbums
        public IQueryable<PhotoAlbum> GetPhotoAlbums()
        {
            return db.PhotoAlbums;
        }

        // GET: api/PhotoAlbums/5
        [ResponseType(typeof(PhotoAlbum))]
        public IHttpActionResult GetPhotoAlbum(int id)
        {
            PhotoAlbum photoAlbum = db.PhotoAlbums.Find(id);
            if (photoAlbum == null)
            {
                return NotFound();
            }

            return Ok(photoAlbum);
        }

        // PUT: api/PhotoAlbums/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhotoAlbum(int id, PhotoAlbum photoAlbum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != photoAlbum.PhotoAlbumId)
            {
                return BadRequest();
            }

            db.Entry(photoAlbum).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoAlbumExists(id))
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

        // POST: api/PhotoAlbums
        [ResponseType(typeof(PhotoAlbum))]
        public IHttpActionResult PostPhotoAlbum(PhotoAlbum photoAlbum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PhotoAlbums.Add(photoAlbum);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = photoAlbum.PhotoAlbumId }, photoAlbum);
        }

        // DELETE: api/PhotoAlbums/5
        [ResponseType(typeof(PhotoAlbum))]
        public IHttpActionResult DeletePhotoAlbum(int id)
        {
            PhotoAlbum photoAlbum = db.PhotoAlbums.Find(id);
            if (photoAlbum == null)
            {
                return NotFound();
            }

            db.PhotoAlbums.Remove(photoAlbum);
            db.SaveChanges();

            return Ok(photoAlbum);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhotoAlbumExists(int id)
        {
            return db.PhotoAlbums.Count(e => e.PhotoAlbumId == id) > 0;
        }
    }
}
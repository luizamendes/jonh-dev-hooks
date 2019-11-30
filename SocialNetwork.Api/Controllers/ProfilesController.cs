using Microsoft.AspNet.Identity;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using SocialNetwork.Api.Models;
using SocialNetwork.Core.Models;
using SocialNetwork.Data.Repositories;
using System;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SocialNetwork.Api.Controllers
{
    public class ProfilesController : ApiController
    {
        // GET: api/Profiles
        public IQueryable<Profile> Get()
        {
            var repository = new ProfileRepository();

            return repository.GetAll();
        }

        // GET: api/Profiles/5
        // No momento em que faço o get do Perfil, eu deveria ter que puxar seus posts ou o entity deveria trazer pronto? 
        public IHttpActionResult Get(int id)
        {
            var repository = new ProfileRepository();
            var profile = repository.GetProfileById(id);

            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }

        // POST: api/Profiles
        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            var result = await Request.Content.ReadAsMultipartAsync();

            var requestJson = await result.Contents[0].ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ProfileBindingModel>(requestJson);

            if (result.Contents.Count > 1)
            {
                model.PictureUrl = await CreateBlob(result.Contents[1]);
            }

            var accountId = User.Identity.GetUserId();

            var profile = new Profile()
            {
                AccountId = accountId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PictureUrl = model.PictureUrl
            };

            var repository = new ProfileRepository();
            repository.Create(profile);

            return Ok();
        }

        // PUT: api/Profiles/5
        public IHttpActionResult Put(int id, Profile profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profile.Id)
            {
                return BadRequest();
            }

            var repository = new ProfileRepository();

            try
            {
                repository.Put(profile);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!repository.ProfileExists(id))
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

        // DELETE: api/Profiles/5
        public IHttpActionResult Delete(int id)
        {
            var repository = new ProfileRepository();
            var profile = repository.GetProfileById(id);

            if (profile == null)
            {
                return NotFound();
            }
            repository.Delete(profile);

            return Ok();
        }

        private async Task<string> CreateBlob(HttpContent httpContent)
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var blobContainerName = "dev-hooks";
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(blobContainerName);
            await blobContainer.CreateIfNotExistsAsync();

            await blobContainer.SetPermissionsAsync(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            var fileName = httpContent.Headers.ContentDisposition.FileName;
            var byteArray = await httpContent.ReadAsByteArrayAsync();

            var blob = blobContainer.GetBlockBlobReference(GetRandomBlobName(fileName));
            await blob.UploadFromByteArrayAsync(byteArray, 0, byteArray.Length);

            return blob.Uri.AbsoluteUri;
        }

        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var repository = new ProfileRepository();
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

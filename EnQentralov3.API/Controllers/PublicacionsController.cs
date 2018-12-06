using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EnQentralov3.API.Helpers;
using EnQentralov3.Common.Models;
using EnQentralov3.Domain.Models;

namespace EnQentralov3.API.Controllers
{
    public class PublicacionsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Publicacions
        public IQueryable<Publicacion> GetPublicacions()
        {
            return db.Publicacions.OrderBy(p => p.Fecha);
        }

        // GET: api/Publicacions/5
        [ResponseType(typeof(Publicacion))]
        public async Task<IHttpActionResult> GetPublicacion(int id)
        {
            var publicacion = await db.Publicacions.FindAsync(id);
            if (publicacion == null)
            {
                return NotFound();
            }

            return Ok(publicacion);
        }

        // PUT: api/Publicacions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPublicacion(int id, Publicacion publicacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != publicacion.PubId)
            {
                return BadRequest();
            }

            db.Entry(publicacion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicacionExists(id))
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

        // POST: api/Publicacions
        [ResponseType(typeof(Publicacion))]
        public async Task<IHttpActionResult> PostPublicacion(Publicacion publicacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (publicacion.ImageArray != null && publicacion.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(publicacion.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/Publicacions";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    publicacion.ImagePath = fullPath;
                }
            }

            db.Publicacions.Add(publicacion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = publicacion.PubId }, publicacion);
        }

        // DELETE: api/Publicacions/5
        [ResponseType(typeof(Publicacion))]
        public async Task<IHttpActionResult> DeletePublicacion(int id)
        {
            Publicacion publicacion = await db.Publicacions.FindAsync(id);
            if (publicacion == null)
            {
                return NotFound();
            }

            db.Publicacions.Remove(publicacion);
            await db.SaveChangesAsync();

            return Ok(publicacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PublicacionExists(int id)
        {
            return db.Publicacions.Count(e => e.PubId == id) > 0;
        }
    }
}
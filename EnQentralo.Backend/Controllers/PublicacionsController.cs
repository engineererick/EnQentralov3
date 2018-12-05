using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnQentralo.Backend.Models;
using EnQentralov3.Common.Models;
using EnQentralo.Backend.Helpers;

namespace EnQentralo.Backend.Controllers
{
    public class PublicacionsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Publicacions
        public async Task<ActionResult> Index()
        {
            return View(await db.Publicacions.ToListAsync());
        }

        // GET: Publicacions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = await db.Publicacions.FindAsync(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            return View(publicacion);
        }

        // GET: Publicacions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publicacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PublicacionView publicacion)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Publicacions";

                if (publicacion.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(publicacion.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                var publicn = this.ToPublicacion(publicacion, pic);
                this.db.Publicacions.Add(publicn);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(publicacion);
        }

        private Publicacion ToPublicacion(PublicacionView publicacion, string pic)
        {
            return new Publicacion
            {
                Descripcion = publicacion.Descripcion,
                Titulo = publicacion.Titulo,
                Tipo = publicacion.Tipo,
                Fecha = publicacion.Fecha,
                Lugar = publicacion.Lugar,
                ImagePath = pic,
                UsuPub = publicacion.UsuPub,

            };
        }

        // GET: Publicacions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var publicacion = await this.db.Publicacions.FindAsync(id);
            //Publicacion publicacion = await db.Publicacions.FindAsync(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            return View(publicacion);
        }

        // POST: Publicacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PubId,Tipo,Titulo,Descripcion,ImagePath,Fecha,Lugar,UsuPub")] Publicacion publicacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publicacion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(publicacion);
        }

        // GET: Publicacions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = await db.Publicacions.FindAsync(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            return View(publicacion);
        }

        // POST: Publicacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Publicacion publicacion = await db.Publicacions.FindAsync(id);
            db.Publicacions.Remove(publicacion);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

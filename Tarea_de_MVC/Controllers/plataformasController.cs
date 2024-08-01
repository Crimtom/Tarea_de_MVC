using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tarea_de_MVC.Models;

namespace Tarea_de_MVC.Views
{
    public class plataformasController : Controller
    {
        private eleccionesEntities db = new eleccionesEntities();

        // GET: plataformas
        public ActionResult Index()
        {
            var plataformas = db.plataformas.Include(p => p.partidos);
            return View(plataformas.ToList());
        }

        // GET: plataformas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plataformas plataformas = db.plataformas.Find(id);
            if (plataformas == null)
            {
                return HttpNotFound();
            }
            return View(plataformas);
        }

        // GET: plataformas/Create
        public ActionResult Create()
        {
            ViewBag.partido = new SelectList(db.partidos, "nombre_partido", "nombre_partido");
            return View();
        }

        // POST: plataformas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "plataforma_nombre,partido,posicion_espectro")] plataformas plataformas)
        {
            if (ModelState.IsValid)
            {
                db.plataformas.Add(plataformas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.partido = new SelectList(db.partidos, "nombre_partido", "nombre_partido", plataformas.partido);
            return View(plataformas);
        }

        // GET: plataformas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plataformas plataformas = db.plataformas.Find(id);
            if (plataformas == null)
            {
                return HttpNotFound();
            }
            ViewBag.partido = new SelectList(db.partidos, "nombre_partido", "nombre_partido", plataformas.partido);
            return View(plataformas);
        }

        // POST: plataformas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "plataforma_nombre,partido,posicion_espectro")] plataformas plataformas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plataformas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.partido = new SelectList(db.partidos, "nombre_partido", "nombre_partido", plataformas.partido);
            return View(plataformas);
        }

        // GET: plataformas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plataformas plataformas = db.plataformas.Find(id);
            if (plataformas == null)
            {
                return HttpNotFound();
            }
            return View(plataformas);
        }

        // POST: plataformas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            plataformas plataformas = db.plataformas.Find(id);
            db.plataformas.Remove(plataformas);
            db.SaveChanges();
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

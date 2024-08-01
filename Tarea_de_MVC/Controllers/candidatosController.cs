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
    public class candidatosController : Controller
    {
        private eleccionesEntities db = new eleccionesEntities();

        // GET: candidatos
        public ActionResult Index()
        {
            var candidatos = db.candidatos.Include(c => c.partidos).Include(c => c.plataformas);
            return View(candidatos.ToList());
        }

        // GET: candidatos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            candidatos candidatos = db.candidatos.Find(id);
            if (candidatos == null)
            {
                return HttpNotFound();
            }
            return View(candidatos);
        }

        // GET: candidatos/Create
        public ActionResult Create()
        {
            ViewBag.partido = new SelectList(db.partidos, "nombre_partido", "nombre_partido");
            ViewBag.plataforma = new SelectList(db.plataformas, "plataforma_nombre", "partido");
            return View();
        }

        // POST: candidatos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cedula,nombre,partido,plataforma,edad")] candidatos candidatos)
        {
            if (ModelState.IsValid)
            {
                db.candidatos.Add(candidatos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.partido = new SelectList(db.partidos, "nombre_partido", "nombre_partido", candidatos.partido);
            ViewBag.plataforma = new SelectList(db.plataformas, "plataforma_nombre", "partido", candidatos.plataforma);
            return View(candidatos);
        }

        // GET: candidatos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            candidatos candidatos = db.candidatos.Find(id);
            if (candidatos == null)
            {
                return HttpNotFound();
            }
            ViewBag.partido = new SelectList(db.partidos, "nombre_partido", "nombre_partido", candidatos.partido);
            ViewBag.plataforma = new SelectList(db.plataformas, "plataforma_nombre", "partido", candidatos.plataforma);
            return View(candidatos);
        }

        // POST: candidatos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cedula,nombre,partido,plataforma,edad")] candidatos candidatos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidatos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.partido = new SelectList(db.partidos, "nombre_partido", "nombre_partido", candidatos.partido);
            ViewBag.plataforma = new SelectList(db.plataformas, "plataforma_nombre", "partido", candidatos.plataforma);
            return View(candidatos);
        }

        // GET: candidatos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            candidatos candidatos = db.candidatos.Find(id);
            if (candidatos == null)
            {
                return HttpNotFound();
            }
            return View(candidatos);
        }

        // POST: candidatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            candidatos candidatos = db.candidatos.Find(id);
            db.candidatos.Remove(candidatos);
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

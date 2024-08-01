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
    public class partidosController : Controller
    {
        private eleccionesEntities db = new eleccionesEntities();

        // GET: partidos
        public ActionResult Index()
        {
            return View(db.partidos.ToList());
        }

        // GET: partidos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            partidos partidos = db.partidos.Find(id);
            if (partidos == null)
            {
                return HttpNotFound();
            }
            return View(partidos);
        }

        // GET: partidos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: partidos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre_partido,miembros")] partidos partidos)
        {
            if (ModelState.IsValid)
            {
                db.partidos.Add(partidos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partidos);
        }

        // GET: partidos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            partidos partidos = db.partidos.Find(id);
            if (partidos == null)
            {
                return HttpNotFound();
            }
            return View(partidos);
        }

        // POST: partidos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nombre_partido,miembros")] partidos partidos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partidos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partidos);
        }

        // GET: partidos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            partidos partidos = db.partidos.Find(id);
            if (partidos == null)
            {
                return HttpNotFound();
            }
            return View(partidos);
        }

        // POST: partidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            partidos partidos = db.partidos.Find(id);
            db.partidos.Remove(partidos);
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

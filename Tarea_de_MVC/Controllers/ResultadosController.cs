using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tarea_de_MVC.Models;
using Tarea_de_MVC.ViewModel;

namespace Tarea_de_MVC.Controllers
{
    public class ResultadosController : Controller
    {
        private readonly eleccionesEntities entities;

        public ResultadosController(eleccionesEntities entities)
        {
            this.entities = entities;
        }

        public ActionResult Index()
        {
            var candi_y_votos = from c in entities.candidatos
                                join v in entities.votos
                                on c.nombre equals v.voto_candidato
                                select new ResultadosViewModel
                                {
                                    candi_nombre = c.nombre,
                                    voto_nombre = v.voto_candidato
                                };
            return View(candi_y_votos.ToList());
        }

        // GET: Resultados
        
    }
}
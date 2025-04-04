using ExamenDosServiciosWeb.Clases;
using ExamenDosServiciosWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ExamenDosServiciosWeb.Controllers
{
    [RoutePrefix("api/Camiones")]
    public class CamionesController : ApiController
    {
        //GET: Se utiliza para consultar información, no se debe modificar la base de datos
        //POST: Se utiliza para insertar información en la base de datos
        //PUT: Se utiliza para modificar (Actualizar) información en la base de datos
        //DELETE: Se utiliza para eliminar información en la base de datos
        //[HttpGet] //Es el servicio que se va a exponer: GET, POST, PUT, DELETE
        //[Route("ConsultarTodos")] //Es el nombre de la funcionalidad que se va a ejecutar
        //public List<//GET: Se utiliza para consultar información, no se debe modificar la base de datos
        //POST: Se utiliza para insertar información en la base de datos
        //PUT: Se utiliza para modificar (Actualizar) información en la base de datos
        //DELETE: Se utiliza para eliminar información en la base de datos
        [HttpGet] //Es el servicio que se va a exponer: GET, POST, PUT, DELETE
        [Route("ConsultarTodos")] //Es el nombre de la funcionalidad que se va a ejecutar
        public List<Camion> ConsultarTodos()
        {
            //Se crea una instancia de la clase clsCamion
            ClsCamion Camion = new ClsCamion();
            //Se invoca el método ConsultarTodos() de la clase clsCamion
            return Camion.ConsultarTodos();
        }
        [HttpGet]
        [Route("ConsultarXPlaca")]
        public Camion ConsultarXPlaca(string Placa)
        {
            //Se crea una instancia de la clases clsCamion
            ClsCamion Camion = new ClsCamion();
            return Camion.Consultar(Placa);
        }
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Camion camion)
        {
            //Se crea una instancia de la clase clsCamion
            ClsCamion Camion = new ClsCamion();
            //Se pasa la propieadad camion al objeto de la clases clsCamion
            Camion.camion = camion;
            //Se invoca el método insertar
            return Camion.Insertar();
        }
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Camion camion)
        {
            ClsCamion Camion = new ClsCamion();
            Camion.camion = camion;
            return Camion.Actualizar();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] Camion camion)
        {
            ClsCamion Camion = new ClsCamion();
            Camion.camion = camion;
            return Camion.Eliminar();
        }
        [HttpDelete]
        [Route("EliminarXPlaca")]
        public string EliminarXDocumento(string Placa)
        {
            ClsCamion Camion = new ClsCamion();
            return Camion.Eliminar(Placa);
        }

        [HttpGet]
        [Route("ConsultarPesajes")]
        public IQueryable ConsultarPesajes(string placa)
        {
            ClaseFotosPesaje claseFotosPesaje = new ClaseFotosPesaje();
            return claseFotosPesaje.ListarImagenes(placa);
        }

    }
}
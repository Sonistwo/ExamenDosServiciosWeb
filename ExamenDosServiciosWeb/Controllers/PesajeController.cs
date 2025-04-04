using ExamenDosServiciosWeb.Clases;
using ExamenDosServiciosWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExamenDosServiciosWeb.Controllers
{
    [RoutePrefix("api/Pesaje")]
    public class PesajeController : ApiController
    {
        private DBExamenEntities db = new DBExamenEntities();

        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Pesaje> ConsultarTodos()
        {
            ClasePesaje clasePesaje = new ClasePesaje();
            return clasePesaje.ConsultarPesajes();
        }

        [HttpGet]
        [Route("ConsultarPorCodigo")]
        public Pesaje ConsultarPorCodigo(int codigo)
        {
            ClasePesaje clasePesaje = new ClasePesaje();
            return clasePesaje.ConsultarPesajeCodigo(codigo);
        }

        [HttpGet]
        [Route("ConsultarPorPlaca")]
        public List<Pesaje> ConsultarPorPlaca(string placa)
        {
            ClasePesaje clasePesaje = new ClasePesaje();
            return clasePesaje.ConsultarPesajePlaca(placa);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Pesaje pesaje, string marca, int ejes)
        {
            ClsCamion clsCamion = new ClsCamion();

            if (clsCamion.Consultar(pesaje.PlacaCamion) == null) //Camion no existe
            {
                clsCamion.camion.Placa = pesaje.PlacaCamion;
                clsCamion.camion.Marca = marca;
                clsCamion.camion.NumeroEjes = ejes;
                clsCamion.Insertar();
            }

            ClasePesaje clasePesaje = new ClasePesaje();
            clasePesaje.pesaje = pesaje;
            return clasePesaje.InsertarPesaje();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Pesaje pesaje)
        {
            ClasePesaje clasePesaje = new ClasePesaje();
            clasePesaje.pesaje = pesaje;
            return clasePesaje.ActualizarPesaje();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int codigo)
        {
            ClasePesaje clasePesaje = new ClasePesaje();
            clasePesaje.pesaje = clasePesaje.ConsultarPesajeCodigo(codigo);
            return clasePesaje.EliminarPesaje();
        }
    }
}
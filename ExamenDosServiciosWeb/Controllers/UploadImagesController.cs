using ExamenDosServiciosWeb.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExamenDosServiciosWeb.Controllers
{
    [RoutePrefix("api/UploadImages")]
    public class UploadImagesController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> CargarImagen(HttpRequestMessage request, string Datos, string Proceso)
        {
            ClaseUpload upload = new Clases.ClaseUpload();
            upload.Datos = Datos;
            upload.Proceso = Proceso;
            upload.request = request;
            return await upload.GrabarArchivo(false);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> ActualizarImagen(HttpRequestMessage request, string Datos, string Proceso)
        {
            ClaseUpload upload = new Clases.ClaseUpload();
            upload.Datos = Datos;
            upload.Proceso = Proceso;
            upload.request = request;
            return await upload.GrabarArchivo(true);
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> EliminarImagen([FromUri] string imagen)
        {
            ClaseUpload upload = new Clases.ClaseUpload();
            return await upload.BorrarArchivo(imagen);
        }
    }
}
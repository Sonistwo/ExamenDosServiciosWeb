using ExamenDosServiciosWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ExamenDosServiciosWeb.Clases
{
    public class ClaseUpload
    {
        public string Datos { get; set; }
        public string Proceso { get; set; }
        public HttpRequestMessage request { get; set; }
        private List<string> Archivos { get; set; }
        public async Task<HttpResponseMessage> GrabarArchivo(bool actualizar)
        {
            if (!request.Content.IsMimeMultipartContent())
            {
                return request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "No se envió imagen para procesar");
            }

            string root = HttpContext.Current.Server.MapPath("~/Imagenes");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                bool existe = false;
                await request.Content.ReadAsMultipartAsync(provider);
                if (provider.FileData.Count > 0)
                {
                    Archivos = new List<string>();
                    foreach (MultipartFileData file in provider.FileData)
                    {
                        string fileName = file.Headers.ContentDisposition.FileName.Trim('"');

                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                        {
                            fileName = fileName.Trim('"');
                        }
                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        {
                            fileName = Path.GetFileName(fileName);
                        }

                        if (File.Exists(Path.Combine(root, fileName)))
                        {
                            if (actualizar)
                            {
                                File.Delete(Path.Combine(root, fileName));
                                File.Move(file.LocalFileName, Path.Combine(root, fileName));
                            }
                            else
                            {
                                File.Delete(Path.Combine(root, file.LocalFileName));
                                existe = true;
                            }
                        }
                        else
                        {
                            if (!actualizar)
                            {
                                existe = false;
                                Archivos.Add(fileName);
                                File.Move(file.LocalFileName, Path.Combine(root, fileName));
                            }
                            else 
                            {
                                return request.CreateErrorResponse(HttpStatusCode.Conflict, "El archivo no existe");
                            }
                            
                        }
                    }
                    if (!existe)
                    {
                        string respuestaBD = ProcesarBD();
                        return request.CreateResponse(HttpStatusCode.OK, "Se cargaron los archivos en el servidor. " + respuestaBD);
                    }
                    else
                    {
                        return request.CreateErrorResponse(HttpStatusCode.Conflict, "El archivo ya existe");
                    }
                }
                else
                {
                    return request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "No se envió imagen para procesar");
                }
            }
            catch(Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error al procesar la imagen: " + ex.Message);
            }
        }
        public HttpResponseMessage DescargarArchivo(string Imagen)
        {
            try
            {
                string Ruta = HttpContext.Current.Server.MapPath("~/Archivos");
                string Archivo = Path.Combine(Ruta, Imagen);
                if (File.Exists(Archivo))
                {
                    HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                    var stream = new FileStream(Archivo, FileMode.Open);
                    response.Content = new StreamContent(stream);
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = Imagen;
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    return response;
                }
                else
                {
                    return request.CreateErrorResponse(System.Net.HttpStatusCode.NoContent, "No se encontró el archivo");
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<HttpResponseMessage> BorrarArchivo(string Imagen)
        {
            try
            {
                string Ruta = HttpContext.Current.Server.MapPath("~/Imagenes");
                string Archivo = Path.Combine(Ruta, Imagen);

                if (File.Exists(Archivo))
                {
                    DBExamenEntities db = new DBExamenEntities();
                    File.Delete(Archivo);
                    FotoPesaje fotopsj = db.FotoPesajes.FirstOrDefault(fp => fp.ImagenVehiculo == Imagen);
                    db.FotoPesajes.Remove(fotopsj);
                    db.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Se eliminó la imagen")
                    };
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("No se halló la imagen para eliminar")
                    };
                }
            }
            catch(Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Error al borrar la imagen: " + ex.Message)
                };
            }
        }

        private string ProcesarBD()
        {
            ClaseFotosPesaje claseFotosPesaje = new ClaseFotosPesaje();
            return claseFotosPesaje.GrabarImagenPesaje(Convert.ToInt32(Datos), Archivos);
        }
    }
}
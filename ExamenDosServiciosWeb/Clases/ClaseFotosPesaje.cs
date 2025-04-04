using ExamenDosServiciosWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenDosServiciosWeb.Clases
{
    public class ClaseFotosPesaje
    {
        DBExamenEntities db = new DBExamenEntities();
        public string GrabarImagenPesaje(int CodigoPesaje, List<string> imagenes)
        {
            try
            {
                foreach (string imagen in imagenes)
                {
                    FotoPesaje fotoPesaje = new FotoPesaje();
                    fotoPesaje.idPesaje = CodigoPesaje;
                    fotoPesaje.ImagenVehiculo = imagen;
                    db.FotoPesajes.Add(fotoPesaje);
                    db.SaveChanges();
                }
                return "Se grabó correctamente la imagen del pesaje";
            }
            catch (Exception ex)
            {
                return "Error al grabar la imagen: " + ex.Message;
            }
        }

        public IQueryable<object> ListarImagenes(string placa)
        {
            return from p in db.Pesajes
                   join c in db.Camions on p.PlacaCamion equals c.Placa
                   join f in db.FotoPesajes on p.id equals f.idPesaje
                   where c.Placa == placa
                   select new
                   {
                       CodigoPesaje = p.id,
                       Placa = c.Placa,
                       Marca = c.Marca,
                       Ejes = c.NumeroEjes,
                       FechaPesaje = p.FechaPesaje,
                       Peso = p.Peso,
                       Estacion = p.Estacion,
                       Evidencia = f.ImagenVehiculo
                   };
        }
    }
}
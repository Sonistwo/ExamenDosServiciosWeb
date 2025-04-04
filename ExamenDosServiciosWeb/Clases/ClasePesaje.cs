using ExamenDosServiciosWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace ExamenDosServiciosWeb.Clases
{
    public class ClasePesaje
    {
        DBExamenEntities db = new DBExamenEntities();

        public Pesaje pesaje { get; set; }

        public string InsertarPesaje()
        {
            try
            {
                db.Pesajes.Add(pesaje);
                db.SaveChanges();
                return "Se insertó correctamente el pesaje";
            }
            catch (Exception ex)
            {
                return "Error al insertar el pesaje: " + ex.Message;
            }
        }
        public List<Pesaje> ConsultarPesajes()
        {
            return db.Pesajes.ToList();
        }
        public Pesaje ConsultarPesajeCodigo(int codigo)
        {
            return db.Pesajes.FirstOrDefault(pesaje => pesaje.id == codigo);
        }

        public List<Pesaje> ConsultarPesajePlaca(string placa)
        {
            return db.Pesajes.ToListAsync().Result
                .Where(p => p.PlacaCamion == placa.ToString())
                .ToList();
        }
        public string ActualizarPesaje()
        {
            try
            {
                Pesaje ps = ConsultarPesajeCodigo(pesaje.id);

                if (ps == null)
                {
                    return "Pesaje de código " + pesaje.id + " no encontrado";
                }

                db.Pesajes.AddOrUpdate(pesaje);
                db.SaveChanges();

                return "Se actualizó el pesaje correctamente";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el pesaje: " + ex.Message;
            }
        }
        public string EliminarPesaje()
        {
            try
            {
                Pesaje psj = ConsultarPesajeCodigo(pesaje.id);

                if (psj == null)
                {
                    return "Pesaje de código " + pesaje.id + " no encontrado para eliminar";
                }

                db.Pesajes.Remove(psj);
                db.SaveChanges();

                return "Se eliminó el pesaje correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el pesaje: " + ex.Message;
            }
        }
    }
}
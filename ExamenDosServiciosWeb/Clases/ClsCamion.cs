using ExamenDosServiciosWeb.Clases;
using ExamenDosServiciosWeb.Models;
using System;

using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

using System.Web;
using System.Web.Http;

namespace ExamenDosServiciosWeb.Clases

{

    public class ClsCamion

    {

        private DBExamenEntities DBExamen = new DBExamenEntities(); // Es el atributo (Propiedad) para gestionar la conexión a la base de datos

        public Camion camion { get; set; } = new Camion(); //Propiedad para manipular la información en la base de datos: Permite agregar, modificar o eliminar

        public string Insertar()

        {

            try

            {

                DBExamen.Camions.Add(camion); //Agregar el objeto Camion a la lista de "Camiones". Todavía no se agrega a la base de datos. Se debe invocar el método SaveChanges()

                DBExamen.SaveChanges(); //Guardar los cambios en la base de datos

                return "Camion insertado correctamente";

            }

            catch (Exception ex)

            {

                return "Error al insertar el camion: " + ex.Message;

            }

        }

        public string Actualizar()

        {

            try

            {

                //Antes de actualizar un elemento (camion), se debe consultar para verificar que exista, y ahí si poderlo actualizar

                Camion cam = Consultar(camion.Placa);

                if (cam == null)

                {

                    return "El camion con la placa ingresada no existe, por lo tanto no se puede actualizar";

                }

                //El camion existe lo podemos actualizar

                DBExamen.Camions.AddOrUpdate(camion); //Actualiza el objeto camion en la lista de "camiones". Todavía no se actualiza en la base de datos. Se debe invocar el método SaveChanges()

                DBExamen.SaveChanges(); //Guardar los cambios en la base de datos

                return "Se actualizó el camion correctamente";

            }

            catch (Exception ex)

            {

                return "No se pudo actualizar el camion: " + ex.Message;

            }

        }

        public List<Camion> ConsultarTodos()

        {

            return DBExamen.Camions

                .OrderBy(e => e.Placa) //OrderBy() es una función que permite ordenar los elementos de una lista de acuerdo a un criterio específico. En este caso, se ordena por la placa

                .ToList(); //ToList() es una función que convierte una lista de datos en una lista de objetos

        }

        public Camion Consultar(string Placa)

        {

            //Expresiones lambda.  => permite definir funciones anónimas o instancias de objetos, sin la creación formal, dependiendo de la lista a la cual se hace referencia

            //FirstOrDefault. Es una función que permite consultar el primer elemento de una lista que cumple las condiciones solicitadas

            return DBExamen.Camions.FirstOrDefault(e => e.Placa == Placa);

        }

        public string Eliminar()

        {

            try

            {

                //Antes de eliminar se debe verificar si el camion existe

                Camion cam = Consultar(camion.Placa);

                if (cam == null)

                {

                    return "El camion con la placa ingresada no existe, por lo tanto no se puede eliminar";

                }

                //El camion existe lo podemos eliminar. Se elimina el objeto camion que se busca, no el que se envía como parámetro

                DBExamen.Camions.Remove(cam); //Eliminar el objeto camion de la lista de "camiones". Todavía no se elimina de la base de datos. Se debe invocar el método SaveChanges()

                DBExamen.SaveChanges(); //Guardar los cambios en la base de datos

                return "Se eliminó el camion correctamente";

            }

            catch (Exception ex)

            {

                return "No se pudo eliminar el camion: " + ex.Message;

            }

        }

        public string Eliminar(string Placa)

        {

            try

            {

                //Antes de eliminar se debe verificar si el camion existe

                Camion cam = Consultar(Placa);

                if (cam == null)

                {

                    return "El camion con la placa ingresada no existe, por lo tanto no se puede eliminar";

                }

                //El camion existe lo podemos eliminar. Se elimina el objeto camion que se busca, no el que se envía como parámetro

                DBExamen.Camions.Remove(cam); //Eliminar el objeto camion de la lista de "camiones". Todavía no se elimina de la base de datos. Se debe invocar el método SaveChanges()

                DBExamen.SaveChanges(); //Guardar los cambios en la base de datos

                return "Se eliminó el camion correctamente";

            }

            catch (Exception ex)

            {

                return "No se pudo eliminar el camion: " + ex.Message;

            }

        }

    }

}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMapasTiendav2.Models;
using WSMapasTiendav2.Models.Peticiones;
using WSMapasTiendav2.Models.Respuestas;

namespace WSMapasTiendav2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        //Respuesta Generica para el cliente
        RespuestaGenerica miRes = new RespuestaGenerica();

        //Consultar Tiendas
        [HttpGet]
        public IActionResult GetStores() {            

            try {
                using (MapasTiendaWSV2Context db = new MapasTiendaWSV2Context())
                {
                    var Tiendas = db.Stores.OrderByDescending(t => t.Id).ToList();

                    //llenamos objs de respuesta para el cliente
                    miRes.Exito = 1;
                    miRes.Data = Tiendas;
                    miRes.Mensaje = "Tiendas Consultada Exitosamente";
                }
            }
            catch (Exception err) { miRes.Mensaje = err.Message; }
            return Ok(miRes); 
        }

        //Consultar Tiendas por su ID
        [HttpGet("{id}")]
        public IActionResult GetStoresById(int id) {            

            try
            {
                using (MapasTiendaWSV2Context db = new MapasTiendaWSV2Context())
                {
                    Store Tienda = db.Stores.Find(id);
                    //llenamos objs de respuesta para el cliente
                    miRes.Exito = 1;
                    miRes.Data = Tienda;
                    miRes.Mensaje = "Tienda Consultada Exitosamente";
                }
            }
            catch (Exception err) { miRes.Mensaje = err.Message; }
            return Ok(miRes);
        }

        //Agregar Tiendas
        [HttpPost]
        public IActionResult AddStores(StoresPeticion storeP) {            

            try
            {
                using (MapasTiendaWSV2Context db = new MapasTiendaWSV2Context())
                {   
                    //Llenamos el obj Tienda con la clase StoresPeticion
                    Store Tienda = new Store();
                    Tienda.Nombre = storeP.nombre;
                    Tienda.Direccion = storeP.direccion;
                    Tienda.Telefono = storeP.telefono;
                    Tienda.Latitud = storeP.latitud;
                    Tienda.Longitud = storeP.longitud;
                    //Insertamos en la base de datos y guardamos
                    db.Add(Tienda);
                    db.SaveChanges();
                    //llenamos objs de respuesta para el cliente
                    miRes.Exito = 1;
                    miRes.Data = Tienda;
                    miRes.Mensaje = "Tienda Insertada Exitosamente";
                }
            }
            catch (Exception err) { miRes.Mensaje = err.Message; }
            return Ok(miRes);
        }

        //Modificar Tiendas
        [HttpPut]
        public IActionResult EditStores(StoresPeticion storeP) {            

            try
            {
                using (MapasTiendaWSV2Context db = new MapasTiendaWSV2Context())
                {
                    //Llenamos el obj Tienda con la clase StoresPeticion
                    Store Tienda = db.Stores.Find(storeP.id);
                    Tienda.Nombre = storeP.nombre;
                    Tienda.Direccion = storeP.direccion;
                    Tienda.Telefono = storeP.telefono;
                    Tienda.Latitud = storeP.latitud;
                    Tienda.Longitud = storeP.longitud;
                    //Insertamos en la base de datos y guardamos
                    db.Add(Tienda);
                    db.SaveChanges();
                    //llenamos objs de respuesta para el cliente
                    miRes.Exito = 1;
                    miRes.Data = Tienda;
                    miRes.Mensaje = "Tienda Modificada Exitosamente";
                }
            }
            catch (Exception err) { miRes.Mensaje = err.Message; }
            return Ok(miRes);
        }

        //ELIMINAR Tiendas
        [HttpDelete]
        public IActionResult DeleteStores(int id) {           

            try
            {
                using (MapasTiendaWSV2Context db = new MapasTiendaWSV2Context())
                {
                    Store Tienda = db.Stores.Find(id);
                    db.Remove(Tienda);
                    db.SaveChanges();
                    //llenamos objs de respuesta para el cliente
                    miRes.Exito = 1;
                    miRes.Data = Tienda;
                    miRes.Mensaje = "Tienda ELIMINADA Exitosamente";
                }
            }
            catch (Exception err) { miRes.Mensaje = err.Message; }
            return Ok(miRes);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPractica.Models;

namespace WebApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContext;
        public equiposController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            try
            {

            List<equipos> listadoEquipos = (from e in _equiposContext.equipos
                                            select e).ToList();
            if(listadoEquipos.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            equipos? equipo = (from e in _equiposContext.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);

            
        }
        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByDescription(string filtro)
        {
            equipos? equipo = (from e in _equiposContext.equipos
                               where e.descripcion.Contains(filtro)
                               select e).FirstOrDefault();
            if(equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody] equipos equipo)
        {
            try
            {
                _equiposContext.equipos.Add(equipo);
                _equiposContext.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
                
        }
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarEquipo(int id, [FromBody] equipos equipoModificar)
        {
            try
            {
                /*Para actualizar un registro, se obtiene el registro original de la base de datos 
             al cual alteramos alguna propiedad*/

                equipos? equipoActual = (from e in _equiposContext.equipos
                                         where e.id_equipos == id
                                         select e).FirstOrDefault();

                /*Verificamos que el registro exista según ID*/
                if (equipoActual == null)
                {
                    return NotFound();
                }

                /*Si se  encuentra el registro, se alteran los campos modificables*/

                equipoActual.nombre = equipoModificar.nombre;
                equipoActual.descripcion = equipoModificar.descripcion;
                equipoActual.marca_id = equipoModificar.marca_id;
                equipoActual.tipo_equipo_id = equipoModificar.tipo_equipo_id;
                equipoActual.anio_compra = equipoModificar.anio_compra;
                equipoActual.costo = equipoModificar.costo;

                /*Se marca el registro como modificado en el contexto y se envía la modificación a la base de datos*/

                _equiposContext.Entry(equipoActual).State = EntityState.Modified;
                _equiposContext.SaveChanges();

                return Ok(equipoModificar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarEquipo(int id)
        {
            try
            {
                /*Para actualizar un registro, se obtiene el registro original de la base de datos el cual eliminaremos*/
                equipos? equipo = (from e in _equiposContext.equipos
                                   where e.id_equipos == id
                                   select e).FirstOrDefault();

                //Verificamos que exista el registro según su id
                if (equipo == null)
                {
                    return NotFound();
                }

                //Ejecutamos la acción de eliminar el registro 
                _equiposContext.equipos.Attach(equipo);
                _equiposContext.equipos.Remove(equipo);
                _equiposContext.SaveChanges();

                return Ok(equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

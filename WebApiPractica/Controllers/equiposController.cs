using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
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

    }
}

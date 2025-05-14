using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VisionHive.DTO.Request;
using VisionHive.Infrastructure.Contexts;
using VisionHive.Infrastructure.Persistence;

namespace VisionHive.Controllers
{
    [Route("api/[controller]")]
    [Tags("Areas")]
    [ApiController]
    public class AreaController: ControllerBase
    {
        private readonly AreaContext _context;
        public AreaController(AreaContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Retorna uma lista de áreas
        /// </summary>
        /// <remarks>
        /// Exemplo de Solicitação:
        /// 
        ///     GET api/areas
        /// 
        /// </remarks>
        /// <response code = "200"> Retorna uma lista de áreas</response>
        /// <response code = "500"> Erro interno do servidor</response>
        /// <response code = "503"> Serviço indisponível</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<Area>>> GetAreas()
        {
            return await _context.Areas.ToListAsync();
            
        }

        // GET: api/Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetArea(Guid id)
        {
            var area = await _context.Areas.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }
            return area;
        }

        // POST: api/Areas
        [HttpPost]
        public async Task<ActionResult<Area>> PostArea(AreaRequest areaRequest)
        {
            var area = Area.Create(areaRequest.Nome);
            if (area == null)
            {
                return BadRequest();
            }
            _context.Areas.Add(area);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArea", new { id = area.Id }, area);
        }

        // PUT: api/Areas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea(Guid id, AreaRequest areaRequest)
        {
            var area = await _context.Areas.FindAsync(id);
            
            if (area == null)
            {
                return NotFound();
            }
            if (id != area.Id)
            {
                return BadRequest();
            }

            // Atualiza os dados
            area.AtualizarDados(areaRequest.Nome);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/Areas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea(Guid id)
        {
            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }
            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool AreaExists(Guid id)
        {
            return _context.Areas.Any(a => a.Id == id);
        }
    }
}

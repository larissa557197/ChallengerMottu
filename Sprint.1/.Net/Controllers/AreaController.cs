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
        private readonly AreaContext _areaContext;
        public AreaController(AreaContext context)
        {
            _areaContext = context;
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
            return await _areaContext.Areas.ToListAsync();
        }

        // GET: api/Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetArea(Guid id)
        {
            var area = await _areaContext.Areas.FindAsync(id);

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
            _areaContext.Areas.Add(area);
            await _areaContext.SaveChangesAsync();

            return CreatedAtAction("GetArea", new { id = area.Id }, area);
        }

        // PUT: api/Areas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea(Guid id, AreaRequest areaRequest)
        {
            var area = await _areaContext.Areas.FindAsync(id);
            if (id != area.Id)
            {
                return BadRequest();
            }
            _areaContext.Entry(area).State = EntityState.Modified;
            try
            {
                await _areaContext.SaveChangesAsync();
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
            var area = await _areaContext.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }
            _areaContext.Areas.Remove(area);
            await _areaContext.SaveChangesAsync();
            return NoContent();
        }

        private bool AreaExists(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

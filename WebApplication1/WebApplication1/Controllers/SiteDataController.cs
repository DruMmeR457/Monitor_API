using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MetricsAPI.Models;

namespace MetricsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteDataController : ControllerBase
    {
        private readonly MetricContext _context;

        public SiteDataController(MetricContext context)
        {
            _context = context;
        }

        // GET: api/SiteData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SiteData>>> GetSiteData()
        {
            return await _context.SiteData.ToListAsync();
        }

        // GET: api/SiteData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SiteData>> GetSiteData(long id)
        {
            var siteData = await _context.SiteData.FindAsync(id);

            if (siteData == null)
            {
                return NotFound();
            }

            return siteData;
        }

        // PUT: api/SiteData/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSiteData(long id, SiteData SiteData)
        {
            if (id != SiteData.Monitor_ID)
            {
                return BadRequest();
            }

            _context.Entry(SiteData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteDataExists(id))
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

        // POST: api/SiteData
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SiteData>> PostSiteData(SiteData SiteData)
        {
            _context.SiteData.Add(SiteData);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetSiteData", new { id = SiteData.Id }, SiteData);
            return CreatedAtAction(nameof(GetSiteData), new { id = SiteData.Monitor_ID }, SiteData);
        }

        // DELETE: api/SiteData/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SiteData>> DeleteSiteData(long id)
        {
            var siteData = await _context.SiteData.FindAsync(id);
            if (siteData == null)
            {
                return NotFound();
            }

            _context.SiteData.Remove(siteData);
            await _context.SaveChangesAsync();

            return siteData;
        }

        private bool SiteDataExists(long id)
        {
            return _context.SiteData.Any(e => e.Monitor_ID == id);
        }
    }
}

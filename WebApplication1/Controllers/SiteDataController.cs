///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 1
/// File Name:      SiteDataController.cs
/// Description:    
///                 Class used to act upon API
/// Course:         CSCI 4350 - Software Engineering
/// Authors:        
///                 Darien Roach,   roachda@etsu.edu,   Developer
///                 Grant Watson,   watsongo@etsu.edu,  Developer
///                 Stephen Dalton, daltonsa@etsu.edu,  Developer
///                 Kelly King,     kingkr1@etsu.edu,   Developer
///                 Jackson Brooks, brooksjt@etsu.edu,  Developer
///                 Nick Ehrhart,   ehrhart@etsu.edu,   Product Owner
///                 Anna Cade,      cadea1@etsu.edu,    Scrum Master
///                 
/// Created:        Monday, September 14th, 2020
///
//////////////////////////////////////////////////////////////////////////

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

    /// <summary>
    /// SiteDataController class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SiteDataController : ControllerBase
    {
        private readonly MetricContext _context;

        /// <summary>
        /// Used to set read only _context from passed MetricContext
        /// </summary>
        /// <param name="context"></param>
        public SiteDataController(MetricContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Used to return SiteData from _context
        /// </summary>
        /// <returns>_context.SiteData in Async list</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SiteData>>> GetSiteData()
        {
            return await _context.SiteData.ToListAsync();
        }

        /// <summary>
        /// Used to return specific SiteData based upon ID
        /// </summary>
        /// <param name="id">contains ID of item being sought</param>
        /// <returns>siteData</returns>
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
        /// <summary>
        /// Put command for the API
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="SiteData">SiteData</param>
        /// <returns>Returns based upon status of put command</returns>
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

        /// <summary>
        /// Post command for API
        /// </summary>
        /// <param name="SiteData">Acceptable Data type</param>
        /// <returns>Returns status of Post command</returns>
        [HttpPost]
        public async Task<ActionResult<SiteData>> PostSiteData(SiteData SiteData)
        {
            _context.SiteData.Add(SiteData);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetSiteData", new { id = SiteData.Id }, SiteData);
            return CreatedAtAction(nameof(GetSiteData), new { id = SiteData.Monitor_ID }, SiteData);
        }

        /// <summary>
        /// Delete command for API
        /// </summary>
        /// <param name="id">Identifier of item to be deleted</param>
        /// <returns>Status of deletion</returns>
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
        /// <summary>
        /// Checks to see if identifier exists.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>status of whether identifier is related to data</returns>
        private bool SiteDataExists(long id)
        {
            return _context.SiteData.Any(e => e.Monitor_ID == id);
        }
    }
}

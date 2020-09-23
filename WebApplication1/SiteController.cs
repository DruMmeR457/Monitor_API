using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAPI
{
    [Route("api/[controller]")]
    public class SiteController : ControllerBase
    {
        public AppDb Db { get; }

        public SiteController(AppDb db)
        {
            Db = db;
        }

        //GET api/metrics
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new SiteDataQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        //GET api/metrics/5
        [HttpGet("{monitor_Id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new SiteDataQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        //POST api/metrics
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SiteData body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        //PUT api/metrics/5
        [HttpPut("{monitor_Id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]SiteData body)
        {
            await Db.Connection.OpenAsync();
            var query = new SiteDataQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.TransactionsOverTime = body.TransactionsOverTime;
            result.NumberOfLogins = body.NumberOfLogins;
            result.WebpageSpeed = body.WebpageSpeed;
            result.ErrorRate = body.ErrorRate;
            result.ServiceAvailability = body.ServiceAvailability;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        //DELETE api/metrics/5
        [HttpDelete("{monitor_Id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new SiteDataQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        //DELETE api/metrics
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new SiteDataQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }
    }
}

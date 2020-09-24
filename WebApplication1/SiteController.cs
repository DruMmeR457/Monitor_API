///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 1
/// File Name:      SiteController.cs
/// Description:    
///                 This program serves control the site and issue commands to 
///                 manipulate it
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
using Microsoft.AspNetCore.Mvc;

namespace MetricsAPI
{
    [Route("api/[controller]")]
    public class SiteController : ControllerBase
    {
        /// <summary>
        /// Get Db
        /// </summary>
        public AppDb Db { get; }
        public SiteData TestData { get; set; }

        /// <summary>
        /// Constructor with AppDb passed
        /// </summary>
        /// <param name="db"></param>
        public SiteController(AppDb db)
        {
            Db = db;
            TestData = new SiteData();
            TestData.Db = Db;
        }

        [Route("/test")]
        public IActionResult Test()
        {
            return Content("Test");
        }

        [Route("/addition")]
        public async Task<IActionResult> Addition()
        {
            await Db.Connection.OpenAsync();
            TestData.ErrorRate = 1;
            TestData.NumberOfLogins = 0;
            TestData.ServiceAvailability = 1;
            TestData.TransactionsOverTime = 1;
            TestData.WebpageSpeed = 1;
            await TestData.InsertAsync();
            return new OkObjectResult(TestData);
        }

        [Route("/update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new SiteDataQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.ErrorRate += 1;
            result.NumberOfLogins += 1;
            result.ServiceAvailability += 1;
            result.TransactionsOverTime += 1;
            result.WebpageSpeed += 1;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        [Route("/remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new SiteDataQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        /// <summary>
        /// Obtains the latest result from the DB
        /// </summary>
        /// <returns>Returns latest result</returns>
        //GET api/metrics
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new SiteDataQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Obtains one result based upon the passed ID
        /// </summary>
        /// <param name="id">ID passed </param>
        /// <returns>Returns object based on passed ID</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new SiteDataQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Issues post command to API
        /// </summary>
        /// <param name="body">Posts body to API</param>
        /// <returns>Returns based upon operation status</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SiteData body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        /// <summary>
        /// Issues put command for API
        /// </summary>
        /// <param name="id">ID for object for Put to act upon</param>
        /// <param name="body">Body object that will be put</param>
        /// <returns></returns>
        [HttpPut("id")]
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

        /// <summary>
        /// Deletes object based upon ID passed
        /// </summary>
        /// <param name="id">ID for object to delete</param>
        /// <returns>Returns status of deletion</returns>
        [HttpDelete("id")]
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

        /// <summary>
        /// Deletes everything from the database
        /// </summary>
        /// <returns>Returns result of deletion</returns>
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

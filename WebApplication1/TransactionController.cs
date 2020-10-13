///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 2
/// File Name:      TransactionController.cs
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
/// Created:        Tuesday, October 13th, 2020
///
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAPI
{
    // http://localhost:5001/api/transaction
    [Route("api/transaction")]
    public class TransactionController : ControllerBase
    {
        /// <summary>
        /// Get Db
        /// </summary>
        public AppDb Db { get; }
        public TransactionData TestData { get; set; }

        /// <summary>
        /// Constructor with AppDb passed
        /// </summary>
        /// <param name="db"></param>
        public TransactionController(AppDb db)
        {
            Db = db;
            TestData = new TransactionData();
            TestData.Db = Db;
        }

        //[Route("/test")]
        //public IActionResult Test()
        //{
        //return Content("Test");
        //}

        //[Route("/addition")]
        //public async Task<IActionResult> Addition()
        //{
        //    await Db.Connection.OpenAsync();
        //    TestData.ErrorRate = 1;
        //    TestData.NumberOfLogins = 0;
        //    TestData.ServiceAvailability = 1;
        //    TestData.TransactionsOverTime = 1;
        //    TestData.WebpageSpeed = 1;
        //    await TestData.InsertAsync();
        //    return new OkObjectResult(TestData);
        //}

        //[Route("/update/{id}")]
        //public async Task<IActionResult> Update(int id)
        //{
        //    await Db.Connection.OpenAsync();
        //    var query = new SiteDataQuery(Db);
        //    var result = await query.FindOneAsync(id);
        //    if (result is null)
        //        return new NotFoundResult();
        //    result.ErrorRate += 1;
        //    result.NumberOfLogins += 1;
        //    result.ServiceAvailability += 1;
        //    result.TransactionsOverTime += 1;
        //    result.WebpageSpeed += 1;
        //    await result.UpdateAsync();
        //    return new OkObjectResult(result);
        //}

        //[Route("/remove/{id}")]
        //public async Task<IActionResult> Remove(int id)
        //{
        //    await Db.Connection.OpenAsync();
        //    var query = new SiteDataQuery(Db);
        //    var result = await query.FindOneAsync(id);
        //    if (result is null)
        //        return new NotFoundResult();
        //    await result.DeleteAsync();
        //    return new OkResult();
        //}

        /// <summary>
        /// Obtains the latest result from the DB
        /// </summary>
        /// <returns>Returns latest result</returns>
        /// GET
        // Route: api/transaction
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new TransactionDataQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Obtains one result based upon the passed ID
        /// </summary>
        /// <param name="id">ID passed </param>
        /// <returns>Returns object based on passed ID</returns>
        /// GET - Conditional
        // Route: api/transaction/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new TransactionDataQuery(Db);
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
        /// POST
        // Route: api/transaction/post
        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] TransactionData body)
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
        /// PUT - Conditional
        // Route: api/transaction/put/{id}
        [HttpPut("put/{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] TransactionData body)
        {
            await Db.Connection.OpenAsync();
            var query = new TransactionDataQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.Time_Stamp = body.Time_Stamp;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Deletes object based upon ID passed
        /// </summary>
        /// <param name="id">ID for object to delete</param>
        /// <returns>Returns status of deletion</returns>
        /// DELETE - Conditional
        // Route: api/transaction/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new TransactionDataQuery(Db);
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
        /// DELETE - All
        // Route: api/transaction/delete
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new TransactionDataQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }
    }
}

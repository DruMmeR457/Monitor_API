///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 2
/// File Name:      LoginController.cs
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
/// Created:        Thursday, October 8th, 2020
///
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAPI
{
    // http://localhost:5001/api/login
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// Get Db
        /// </summary>
        public AppDb Db { get; }
        public LoginData TestData { get; set; }

        /// <summary>
        /// Constructor with AppDb passed
        /// </summary>
        /// <param name="db"></param>
        public LoginController(AppDb db)
        {
            Db = db;
            TestData = new LoginData();
            TestData.Db = Db;
        }

        /// <summary>
        /// Obtains the latest result from the DB
        /// </summary>
        /// <returns>Returns latest result</returns>
        /// GET
        // Route: api/login
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new LoginDataQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Obtains one result based upon the passed ID
        /// </summary>
        /// <param name="id">ID passed </param>
        /// <returns>Returns object based on passed ID</returns>
        /// GET - Conditional
        // Route: api/login/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new LoginDataQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult(); //404 Error
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Issues post command to API
        /// </summary>
        /// <param name="body">Posts body to API</param>
        /// <returns>Returns based upon operation status</returns>
        /// POST
        // Route: api/login/post
        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] LoginData body)
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
        // Route: api/login/put/{id}
        [HttpPut("put/{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] LoginData body)
        {
            await Db.Connection.OpenAsync();
            var query = new LoginDataQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult(); //404 Error
            result.AccountName = body.AccountName;
            result.AccountType = body.AccountType;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Deletes object based upon ID passed
        /// </summary>
        /// <param name="id">ID for object to delete</param>
        /// <returns>Returns status of deletion</returns>
        /// DELETE - Conditional
        // Route: api/login/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new LoginDataQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult(); //404 Error
            await result.DeleteAsync();
            return new OkResult();
        }

        /// <summary>
        /// Deletes everything from the database
        /// </summary>
        /// <returns>Returns result of deletion</returns>
        /// DELETE - All
        // Route: api/login/delete
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new LoginDataQuery(Db);
            await query.DeleteAllAsync();
            await query.ResetAuto();
            return new OkResult();
        }
    }
}
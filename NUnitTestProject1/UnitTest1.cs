using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using MetricsAPI;

namespace NUnitTestProject1
{
    public class Tests
    {
        public AppDb Db { get; set; }
        public ErrorRateController Err { get; set; }
        public LoginController Log { get; set; }
        public TransactionController Tra { get; set; }
        public WebController Web { get; set; }

        [SetUp]
        public void Setup()
        {
            Db = new AppDb("server=metrics-database.c3qkgc3zryke.us-east-1.rds.amazonaws.com;user id=master;password=connection;port=3306;database=metrics");
            Err = new ErrorRateController(Db);
            Log = new LoginController(Db);
            Tra = new TransactionController(Db);
            Web = new WebController(Db);
        }

        [Test]
        public async Task ErrorRate_GetLatest_ReturnsValue_Test()
        {
            var expected = await Err.GetLatest();
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task ErrorRate_GetOne_ReturnsValue_Test()
        {
            var expected = await Err.GetOne(1);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task ErrorRate_Post_ReturnsValue_Test()
        {
            ErrorRateData body = new ErrorRateData();
            body.Time_Stamp = DateTime.Now;
            var expected = await Err.Post(body);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task ErrorRate_Put_ReturnsValue_Test()
        {
            ErrorRateData body = new ErrorRateData();
            body.Time_Stamp = DateTime.Now;
            var expected = await Err.PutOne(1, body);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task ErrorRate_Put_ReturnsNotFoundResultOnNullIndex_Test()
        {
            ErrorRateData body = new ErrorRateData();
            body.Time_Stamp = DateTime.Now;
            var actual = await Err.PutOne(999, body);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<NotFoundResult>(actual);
        }

        [Test]
        public async Task ErrorRate_DeleteOne_ReturnsNotFoundResultOnNullIndex_Test()
        {
            var actual = await Err.DeleteOne(999);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<NotFoundResult>(actual);
        }

        [Test]
        public async Task ErrorRate_GetOne_ReturnsNotFoundResultOnNullIndex_Test()
        {
            var actual = await Err.GetOne(999);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<NotFoundResult>(actual);
        }

        [Test]
        public async Task ErrorRate_Put_ReturnsOkObjectResultOnExistingIndex_Test()
        {
            ErrorRateData body = new ErrorRateData();
            body.Time_Stamp = DateTime.Now;
            var actual = await Err.PutOne(2, body);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<OkObjectResult>(actual);
        }

        [Test]
        public async Task ErrorRate_GetOne_ReturnsOkObjectResultOnExistingIndex_Test()
        {
            var actual = await Err.GetOne(2);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<OkObjectResult>(actual);
        }

        [Test]
        public async Task ErrorRate_DeleteOne_ReturnsValue_Test()
        {
            var expected = await Err.DeleteOne(1);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task ErrorRate_DeleteAll_ReturnsValue_Test()
        {
            var expected = await Err.DeleteAll();
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Login_GetLatest_ReturnsValue_Test()
        {
            var expected = await Log.GetLatest();
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);

        }

        [Test]
        public async Task Login_GetOne_ReturnsValue_Test()
        {
            var expected = await Log.GetOne(1);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Login_Post_ReturnsValue_Test()
        {
            LoginData body = new LoginData();
            body.Time_Stamp = DateTime.Now;
            body.AccountName = "test";
            body.AccountType = "test";
            var expected = await Log.Post(body);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Login_Put_ReturnsValue_Test()
        {
            LoginData body = new LoginData();
            body.Time_Stamp = DateTime.Now;
            body.AccountName = "new";
            body.AccountType = "new";
            var expected = await Log.PutOne(1, body);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Login_Put_ReturnsNotFoundResultOnNullIndex_Test()
        {
            LoginData body = new LoginData();
            body.Time_Stamp = DateTime.Now;
            body.AccountName = "test";
            body.AccountType = "test";
            var actual = await Log.PutOne(999, body);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<NotFoundResult>(actual);
        }

        [Test]
        public async Task Login_DeleteOne_ReturnsNotFoundResultOnNullIndex_Test()
        {
            var actual = await Log.DeleteOne(999);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<NotFoundResult>(actual);
        }

        [Test]
        public async Task Login_GetOne_ReturnsNotFoundResultOnNullIndex_Test()
        {
            var actual = await Log.GetOne(999);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<NotFoundResult>(actual);
        }

        [Test]
        public async Task Login_Put_ReturnsOkObjectResultOnExistingIndex_Test()
        {
            LoginData body = new LoginData();
            body.Time_Stamp = DateTime.Now;
            body.AccountName = "test";
            body.AccountType = "test";
            var actual = await Log.PutOne(2, body);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<OkObjectResult>(actual);
        }

        [Test]
        public async Task Login_GetOne_ReturnsOkObjectResultOnExistingIndex_Test()
        {
            var actual = await Log.GetOne(2);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<OkObjectResult>(actual);
        }

        [Test]
        public async Task Login_DeleteOne_ReturnsValue_Test()
        {
            var expected = await Log.DeleteOne(1);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Login_DeleteAll_ReturnsValue_Test()
        {
            var expected = await Log.DeleteAll();
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Transaction_GetLatest_ReturnsValue_Test()
        {
            var expected = await Tra.GetLatest();
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Transaction_GetOne_ReturnsValue_Test()
        {
            var expected = await Tra.GetOne(1);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Transaction_Post_ReturnsValue_Test()
        {
            TransactionData body = new TransactionData();
            body.Time_Stamp = DateTime.Now;
            var expected = await Tra.Post(body);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Transaction_Put_ReturnsValue_Test()
        {
            TransactionData body = new TransactionData();
            body.Time_Stamp = DateTime.Now;
            var expected = await Tra.PutOne(1, body);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Transaction_Put_ReturnsNotFoundResultOnNullIndex_Test()
        {
            TransactionData body = new TransactionData();
            body.Time_Stamp = DateTime.Now;
            var actual = await Tra.PutOne(999, body);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<NotFoundResult>(actual);
        }

        [Test]
        public async Task Transaction_DeleteOne_ReturnsNotFoundResultOnNullIndex_Test()
        {
            var actual = await Tra.DeleteOne(999);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<NotFoundResult>(actual);
        }

        [Test]
        public async Task Transaction_GetOne_ReturnsNotFoundResultOnNullIndex_Test()
        {
            var actual = await Tra.GetOne(999);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<NotFoundResult>(actual);
        }

        [Test]
        public async Task Transaction_Put_ReturnsOkObjectResultOnExistingIndex_Test()
        {
            TransactionData body = new TransactionData();
            body.Time_Stamp = DateTime.Now;
            var actual = await Tra.PutOne(2, body);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<OkObjectResult>(actual);
        }

        [Test]
        public async Task Transaction_GetOne_ReturnsOkObjectResultOnExistingIndex_Test()
        {
            var actual = await Tra.GetOne(2);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<OkObjectResult>(actual);
        }

        [Test]
        public async Task Transaction_DeleteOne_ReturnsValue_Test()
        {
            var expected = await Tra.DeleteOne(1);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Transaction_DeleteAll_ReturnsValue_Test()
        {
            var expected = await Tra.DeleteAll();
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Web_GetLatest_ReturnsValue_Test()
        {
            var expected = await Web.GetLatest();
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Web_GetOne_ReturnsValue_Test()
        {
            var expected = await Web.GetOne(1);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Web_Post_ReturnsValue_Test()
        {
            WebData body = new WebData();
            body.Time_Stamp = DateTime.Now;
            body.Speed = 0.0;
            var expected = await Web.Post(body);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Web_Put_ReturnsValue_Test()
        {
            WebData body = new WebData();
            body.Time_Stamp = DateTime.Now;
            body.Speed = 0.1;
            var expected = await Web.PutOne(1, body);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Web_Put_ReturnsNotFoundResultOnNullIndex_Test()
        {
            WebData body = new WebData();
            body.Time_Stamp = DateTime.Now;
            body.Speed = 0.0;
            var actual = await Web.PutOne(999, body);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<NotFoundResult>(actual);
        }

        [Test]
        public async Task Web_DeleteOne_ReturnsNotFoundResultOnNullIndex_Test()
        {
            var actual = await Web.DeleteOne(999);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<NotFoundResult>(actual);
        }

        [Test]
        public async Task Web_GetOne_ReturnsNotFoundResultOnNullIndex_Test()
        {
            var actual = await Web.GetOne(999);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<NotFoundResult>(actual);
        }

        [Test]
        public async Task Web_Put_ReturnsOkObjectResultOnExistingIndex_Test()
        {
            WebData body = new WebData();
            body.Time_Stamp = DateTime.Now;
            body.Speed = 0.0;
            var actual = await Web.PutOne(2, body);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<OkObjectResult>(actual);
        }

        [Test]
        public async Task Web_GetOne_ReturnsOkObjectResultOnExistingIndex_Test()
        {
            var actual = await Web.GetOne(2);
            await Db.Connection.CloseAsync();

            Assert.IsInstanceOf<OkObjectResult>(actual);
        }

        [Test]
        public async Task Web_DeleteOne_ReturnsValue_Test()
        {
            var expected = await Web.DeleteOne(1);
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }

        [Test]
        public async Task Web_DeleteAll_ReturnsValue_Test()
        {
            var expected = await Web.DeleteAll();
            await Db.Connection.CloseAsync();

            Assert.IsNotNull(expected);
        }



    }
}
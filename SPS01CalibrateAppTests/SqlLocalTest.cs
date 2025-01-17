using System;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPS01CalibrateApp;

namespace SPS01CalibrateAppTests
{
    [TestClass]
    [TestSubject(typeof(SqlLocal))]
    public class SqlLocalTest
    {

        [TestMethod]
        public void METHOD()
        {
            SqlLocal sqlLocal = new SqlLocal();
            sqlLocal.Database = "SPS01CalibrateApp.db";
            
            var create_sql = "CREATE TABLE if not exists SPS01CalibrateApp (ID int, Name varchar(255), Value int)";
            var insert_sql = "INSERT INTO SPS01CalibrateApp (ID,Name,Value) VALUES (1,'Test',100)";
            var update_sql = "UPDATE SPS01CalibrateApp SET Value = 500 WHERE ID = 1";
            var drop_sql = "Drop Table SPS01CalibrateApp";
            var select_sql = "SELECT * FROM SPS01CalibrateApp";
            var delete_sql = "DELETE FROM SPS01CalibrateApp WHERE ID = 1";
            
            sqlLocal.Connect();
            sqlLocal.Drop(drop_sql);
            sqlLocal.CreateTable(create_sql);
            sqlLocal.Insert(insert_sql);
            var result = sqlLocal.Select(select_sql);
            foreach (var item in result)
            {
                Assert.AreEqual(item[0], "1");
                Assert.AreEqual(item[1], "Test");
                Assert.AreEqual(item[2], "100");
            }

            sqlLocal.Update(update_sql);
            result = sqlLocal.Select(select_sql);
            foreach (var item in result)
            {
                Assert.AreEqual(item[0], "1");
                Assert.AreEqual(item[1], "Test");
                Assert.AreEqual(item[2], "500");
            }
            sqlLocal.Delete(delete_sql);
            result = sqlLocal.Select(select_sql);
            Assert.AreEqual(0, result.Length);
            Assert.IsNull(result);
            
             // throw new Exception("Test");
            // sqlLocal.UpdateData(update_sql);
            
        }
    }
}
using NBi.Core.SqlServer.Batch;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NBi.Testing.Core.SqlServer.Integration.Batch
{
    [TestFixture]
    [Category("LocalSQL")]
    public class BatchRunnerSmoTest
    {
        private const string BATCH_FILE = "Batch.sql";

        public void CleanTemporaryTable(string tableName, string connectionString)
        {
            //Clean the table if needed
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand() { Connection = conn })
                {
                    conn.Open();
                    cmd.CommandText = string.Format("if (exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA = 'dbo' and TABLE_NAME = '{0}')) begin drop table dbo.{0}; end", tableName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [SetUp]
        public void EnsureLocalSqlServerRunning()
        {
            var startTag = @"(local)\";
            var start = ConnectionStringReader.GetLocalSqlClient().IndexOf(startTag) + startTag.Length;
            var end = ConnectionStringReader.GetLocalSqlClient().IndexOf(';', start);
            var serviceName = "MSSQL$" + ConnectionStringReader.GetLocalSqlClient().Substring(start, end - start);

            using (var service = new ServiceController(serviceName))
            {
                if (service.Status != ServiceControllerStatus.Running)
                    Assert.Ignore("Local SQL Server not started.");
            }
        }

        [Test]
        public void Execute_Batch_TablesAreCreated()
        {
            //Create table
            CleanTemporaryTable("TablexxxOne", ConnectionStringReader.GetLocalSqlClient());
            CleanTemporaryTable("TablexxxTwo", ConnectionStringReader.GetLocalSqlClient());

            //Build the fullpath for the file to read
            var fileName = FileOnDisk.CreatePhysicalFile(BATCH_FILE, $"{GetType().Namespace}.Resources.{BATCH_FILE}");

            //Apply the test
            var runner = new BatchRunnerSmo(fileName, ConnectionStringReader.GetLocalSqlClient());
            runner.Execute();

            var countTable = 0;
            using (var conn = new SqlConnection(ConnectionStringReader.GetLocalSqlClient()))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA = 'dbo' and TABLE_NAME like 'Tablexxx%'";
                    countTable = (int)cmd.ExecuteScalar();
                }
            }
            Assert.That(countTable, Is.EqualTo(2));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using NBi.Core.SqlServer.Batch;
using NBi.Extensibility.Decoration.DataEngineering;
using NBi.Extensibility;

namespace NBi.Testing.Unit.Core.SqlServer.Batch
{
    [TestFixture]
    public class BatchRunnerSmoFactoryTest
    {
        [Test]
        public void Get_NullConnection_Exception()
        {
            var args = Mock.Of<IBatchRunnerArgs>(
                x => x.FullPath == "C:\\foo.sql"
            );

            var factory = new BatchRunnerSmoFactory();
            var ex = Assert.Throws<ArgumentNullException>(() => factory.Instantiate(args));
            Assert.That(ex.ParamName, Does.Contain("connection"));
        }

        [Test]
        public void Get_OleDbConnection_Exception()
        {
            var args = Mock.Of<IBatchRunnerArgs>(
                x => x.FullPath == "C:\\foo.sql"
                && x.ConnectionString == "Provider=SQLNCLI11;Server=.;Database=master"
            );

            var factory = new BatchRunnerSmoFactory();
            var ex = Assert.Throws<NBiException>(() => factory.Instantiate(args));
            Assert.That(ex.Message, Does.Contain("SqlConnection"));
        }

        [Test]
        public void Get_SqlConnection_Instantiated()
        {
            var args = Mock.Of<IBatchRunnerArgs>(
                x => x.FullPath == "C:\\foo.sql"
                && x.ConnectionString == "Data Source=.;Initial Catalog=db;Integrated Security=true"
            );

            var factory = new BatchRunnerSmoFactory();
            var runner = factory.Instantiate(args);
            Assert.That(runner, Is.Not.Null);
            Assert.That(runner, Is.TypeOf<BatchRunnerSmo>());
        }

    }
}

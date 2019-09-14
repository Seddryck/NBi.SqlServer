using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBi.Extensibility.Decoration.DataEngineering;
using System.Data.SqlClient;
using NBi.Extensibility;

namespace NBi.Core.SqlServer.Batch
{
    public class BatchRunnerSmoFactory : IBatchRunnerFactory
    {
        public IBatchRunner Instantiate(IBatchRunnerArgs args)
        {
            if (string.IsNullOrEmpty(args.FullPath))
                throw new ArgumentNullException("No file containing the batch to run is defined for the sql-run");

            if (string.IsNullOrEmpty(args.ConnectionString))
                throw new ArgumentNullException("No connection-string defined for the sql-run");

            try
#pragma warning disable CS0642 // Possible mistaken empty statement
            { using (var connection = new SqlConnection(args.ConnectionString)) ; }
#pragma warning restore CS0642 // Possible mistaken empty statement
            catch (ArgumentException)
            {
                throw new NBiException(
                 $"To execute a SQL Batch on a SQL Server, you must provide a connection-string that is associated to a '{typeof(SqlConnection).Name}'. The connection-string '{args.ConnectionString}' is associated to a another kind of connection."
                 );
            }
            return new BatchRunnerSmo(args.FullPath, args.ConnectionString);
        }
    }
}

using System;
using System.IO;
using System.Xml;

namespace NBi.Testing.Core.SqlServer.Integration
{
    class ConnectionStringReader
    {
        public static string Get(string name)
        {
            var xmldoc = new XmlDocument();
            xmldoc.Load(GetFilename());
            XmlNodeList nodes = xmldoc.GetElementsByTagName("add");
            foreach (XmlNode node in nodes)
                if (node.Attributes["name"].Value == name)
                    return node.Attributes["connectionString"].Value;
            throw new Exception();
        }


        public static string GetOleDbCube()
        {
            return Get("OleDbCube");
        }

        public static string GetOleDbSql()
        {
            return Get("OleDbSql");
        }

        public static string GetOdbcSql()
        {
            return Get("OdbcSql");
        }

        public static string GetLocalOleDbSql()
        {
            return Get("LocalOleDbSql");
        }

        public static string GetLocalOdbcSql()
        {
            return Get("LocalOdbcSql");
        }

        public static string GetAdomd()
        {
            return Get("Adomd");
        }

        public static string GetSqlClient()
        {
            return Get("SqlClient");
        }

        private static string GetFilename()
        {
            //If available use the user file
            if (File.Exists(@"C:\Users\cedri\Projects\NBi.SqlServer\NBi.Testing.Core.SqlServer.Integration\bin\Debug\ConnectionString.user.config"))
                return @"C:\Users\cedri\Projects\NBi.SqlServer\NBi.Testing.Core.SqlServer.Integration\bin\Debug\ConnectionString.user.config";
            else if (File.Exists("ConnectionString.config"))
                return "ConnectionString.config";
            throw new ArgumentException();
        }

        internal static string GetAdomdTabular()
        {
            return Get("AdomdTabular");
        }


        internal static string GetLocalSqlClient()
        {
            return Get("LocalSqlClient");
        }
        internal static string GetReportServerDatabase()
        {
            return Get("ReportServerDatabase");
        }

        internal static string GetIntegrationServer()
        {
            return Get("IntegrationServer");
        }

        internal static string GetIntegrationServerTargetDatabase()
        {
            return Get("IntegrationServerTargetDatabase");
        }

    }
}

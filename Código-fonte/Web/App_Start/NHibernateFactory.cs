using AplicacaoArduino.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace AplicacaoArduino.App_Start
{
    public class NHibernateFactory
    {
        static bool init = false;

        public static ISessionFactory CreateSession(string connectionString)
        {

            var _session = Fluently.Configure()
            .Database(MySQLConfiguration.Standard.ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()));
            var session = _session.BuildConfiguration();


            if(!init)
            {
                //var schemaExport = new SchemaExport(session);
                //schemaExport.Execute(false, true, true);
                //schemaExport.Drop(false, true);
                //schemaExport.Create(false, true);

                //var updater = new SchemaUpdate(session);
                //updater.Execute(true, true);
                //var ex = updater.Exceptions;

                //init = true;
            }

            return _session.BuildSessionFactory();
        }
    }

    public class MySqlDriver : NHibernate.Driver.ReflectionBasedDriver
    {
        public MySqlDriver() : base(
            "MySql.Data, Version=5.6.39, Culture=neutral",
            "MySql.Data.MySqlClient.MySqlConnection, MySql.Data, Version=5.6.39, Culture=neutral",
            "MySql.Data.MySqlClient.MySqlCommand, MySql.Data, Version=5.6.39, Culture=neutral"
        )
        { }

        public override bool UseNamedPrefixInParameter
        {
            get { return true; }
        }

        public override bool UseNamedPrefixInSql
        {
            get { return true; }
        }

        public override string NamedPrefix
        {
            get { return "@"; }
        }

        public override bool SupportsMultipleOpenReaders
        {
            get { return false; }
        }
    }
}
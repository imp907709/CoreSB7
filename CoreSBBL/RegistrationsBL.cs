using CoreSBBL.Logging.Infrastructure.GN;
using CoreSBBL.Logging.Infrastructure.TC;
using CoreSBBL.Logging.Infrastructure.Mongo;
using CoreSBBL.Logging.Services;
using CoreSBShared.Registrations;
using CoreSBShared.Universal.Infrastructure.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreSBBL
{
    public static class RegistrationsBL
    {
        /// <summary>
        /// Register contexts for database
        /// </summary>
        public static void RegisterContextsBL(this WebApplicationBuilder builder)
        {
            RegisterEFContextsTC(builder);
            RegisterEFContextsGC(builder);
                
            RegisterMongoContexts(builder);
            RegisterElasticContexts(builder);
        }

        /// <summary>
        /// Register stores, services
        /// </summary>
        public static void RegisterServicesBL(this WebApplicationBuilder builder)
        {
            RegisterEFStoresBL(builder);
            RegisterMongoStores(builder);
            RegisterServices(builder);
        }
        
        /// <summary>
        ///     Register db contexts
        /// </summary>
        internal static void RegisterEFContextsTC(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<LogsContextTC>(options =>

                //options.UseSqlServer(ConnectionsRegister.Connections.MSSQL));
                options.UseSqlServer(ConnectionsRegister.Connections.MSSQLLOCAL));
            builder.Services.AddScoped<DbContext, LogsContextTC>();

        }
        internal static void RegisterEFContextsGC(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<LogsContextGN>(options =>

                //options.UseSqlServer(ConnectionsRegister.Connections.MSSQL));
                options.UseSqlServer(ConnectionsRegister.Connections.MSSQLLOCAL));
            builder.Services.AddScoped<DbContext, LogsContextGN>();

        }
        

        internal static void RegisterEFStoresBL(this WebApplicationBuilder builder)
        {
            // Interface for LogsEFStore
            builder.Services.AddScoped<ILogsEFStore, LogsEFStore>();

            // Interface for LogsEFStoreG<T, K>
            builder.Services.AddScoped(typeof(ILogsEFStoreG<,>), typeof(LogsEFStoreG<,>));

            // Interface for LogsEFStoreGInt
            builder.Services.AddScoped<ILogsEFStoreGInt, LogsEFStoreGInt>();

            // Interface for LogsEFStoreG
            builder.Services.AddScoped<ILogsEFStoreG, LogsEFStoreG>();
        }

        internal static void RegisterMongoStores(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ILoggingMongoStore>(s =>
                new LoggingMongoStore(ConnectionsRegister.MongoConnection.ConnectionString,
                    ConnectionsRegister.MongoConnection.DatabaseName));
        }

        internal static void RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ILoggingService, LoggingService>();
        }

        internal static void RegisterMongoContexts(this WebApplicationBuilder builder)
        {
        }

        /// Register db contexts
        /// </summary>
        internal static void RegisterElasticContexts(this WebApplicationBuilder builder)
        {
        }

     
    }
}

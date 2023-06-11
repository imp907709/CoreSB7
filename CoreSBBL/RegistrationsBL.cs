using CoreSBBL.Logging.Infrastructure.EF;
using CoreSBBL.Logging.Infrastructure.Mongo;
using CoreSBBL.Logging.Services;
using CoreSBShared.Registrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreSBBL
{
    public static class RegistrationsBL
    {
        public static void RegisterContextsBL(this WebApplicationBuilder builder)
        {
            RegisterEFContexts(builder);
            RegisterMongoContexts(builder);
            RegisterElasticContexts(builder);
        }

        /// <summary>
        ///     Register db contexts
        /// </summary>
        internal static void RegisterEFContexts(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<LogsContext>(options =>

                //options.UseSqlServer(ConnectionsRegister.Connections.MSSQL));
                options.UseSqlServer(ConnectionsRegister.Connections.MSSQLLOCAL));
            builder.Services.AddScoped<DbContext, LogsContext>();

            builder.Services.AddScoped<ILogsEFStore, LogsEFStore>();

            builder.Services.AddScoped<ILoggingMongoStore>(s =>
                new LoggingMongoStore(ConnectionsRegister.MongoConnection.ConnectionString,
                    ConnectionsRegister.MongoConnection.DatabaseName));

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

        public static void RegisterServicesBL(this WebApplicationBuilder builder)
        {
            // builder.Services.AddScoped<ILoggingService,LoggingService>();
        }
    }
}

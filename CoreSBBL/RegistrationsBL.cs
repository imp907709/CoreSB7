﻿using CoreSBBL.Logging.Infrastructure.Mongo;
using CoreSBShared.Registrations;
using CoreSBShared.Universal.Infrastructure;
using CoreSBShared.Universal.Infrastructure.EF;
using CoreSBShared.Universal.Infrastructure.EF.Infrastructure.Mongo;
using CoreSBShared.Universal.Infrastructure.Elastic;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using CoreSBShared.Universal.Infrastructure.Mongo;
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
        /// Register db contexts
        /// </summary>
        internal static void RegisterEFContexts(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<LoggingContext>(options =>
                //options.UseSqlServer(ConnectionsRegister.Connections.MSSQL));
                options.UseSqlServer(ConnectionsRegister.Connections.MSSQLLOCAL));
            builder.Services.AddScoped<DbContext, LoggingContext>();

            
            builder.Services.AddScoped<IEFStore,EFStore>();
            builder.Services.AddScoped<IMongoStore, MongoStore>();
            builder.Services.AddScoped<IElasticStoreNest, ElasticStoreNest>();
            
            
            builder.Services.AddScoped<ILoggingEFStore,LoggingEFStore>();
            builder.Services.AddScoped<ILoggingMongoStore>(s=> 
                new LoggingMongoStore(ConnectionsRegister.MongoConnection.ConnectionString, 
                    ConnectionsRegister.MongoConnection.DatabaseName));
            
            builder.Services.AddScoped<ILoggingService,LoggingService>();
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

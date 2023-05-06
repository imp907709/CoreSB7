using CoreSBShared.Registrations;
using CoreSBShared.Universal.Infrastructure;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreSBBL
{
    public static class Registrations
    {
        public static void RegisterContexts(this WebApplicationBuilder builder)
        {
            RegisterMongoContexts(builder);
            RegisterElasticContexts(builder);
        }

        internal static void RegisterMongoContexts(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IMongoStore>(s=> 
                new MongoStore(ConnectionsRegister.MongoConnection.ConnectionString, 
                    ConnectionsRegister.MongoConnection.DatabaseName));
            
        }
        
        /// Register db contexts
        /// </summary>
        internal static void RegisterElasticContexts(this WebApplicationBuilder builder)
        {

        }
    }
}
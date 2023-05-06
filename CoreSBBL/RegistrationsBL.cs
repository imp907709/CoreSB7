using CoreSBShared.Registrations;
using CoreSBShared.Universal.Infrastructure;
using CoreSBShared.Universal.Infrastructure.EF;
using CoreSBShared.Universal.Infrastructure.Interfaces;
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
            builder.Services.AddDbContext<EFcontext>(options =>
                options.UseSqlServer(ConnectionsRegister.Connections.MSSQL));
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
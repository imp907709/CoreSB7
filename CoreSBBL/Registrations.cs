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
        /// Register db contexts
        /// </summary>
        public static void RegisterEFContexts(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<EFcontext>(options =>
                options.UseSqlServer(ConnectionsRegister.Connections.MSSQL));
        }
    }
}
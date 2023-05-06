using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreSBShared.Registrations
{
    /// <summary>
    /// Startup registrations, builder, app
    /// </summary>
    public static class Registrations
    {
        public static void DefaultRegistrations(this WebApplicationBuilder builder)
        {
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        /// <summary>
        /// Register connections from app settings to register with option pattern
        /// </summary>
        public static void RegisterConnections(this WebApplicationBuilder builder)
        {
            builder.Configuration.GetSection(Connections.SectionName).Bind(ConnectionsRegister.Connections);
            builder.Configuration.GetSection(MongoConnection.SectionName).Bind(ConnectionsRegister.MongoConnection);
            builder.Configuration.GetSection(ElasticConenction.SectionName).Bind(ConnectionsRegister.ElasticConenction);
        }

        public static void Registration(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

        }
        
    }
}
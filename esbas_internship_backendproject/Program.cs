using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using esbas_internship_backendproject;
using esbas_internship_backendproject.Entities;
using System.Text.Json.Serialization;

namespace esbas_internship_backendproject
{
#nullable disable
    public class Program
    {
            public static void Main(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(opt =>

            {

                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ESBAS API", Version = "v1" });
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    c.SupportNonNullableReferenceTypes();
                    c.MapType<object>(() => new OpenApiSchema { Type = "object" });
                });

               
                var connectionString = builder.Configuration.GetConnectionString("EsbasDbContext");

                builder.Services.AddDbContext<EsbasDbContext>(options =>
                    options.UseSqlServer(connectionString));

                var app = builder.Build();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ESBAS API");

                    });
                }

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();
                app.Run();
            }
        }
    }
    
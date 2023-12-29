using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NoteApp.Configuration;
using NoteApp.Data;
using NoteApp.Repositories;
using NoteApp.Services;

namespace NoteApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
            var connString = builder.Configuration.GetConnectionString("NotesDBConnection");
            builder.Services.AddDbContext<NotedbContext>(options => options.UseSqlServer(connString));
            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddScoped<IAppliationService, ApplicationService>();
            builder.Services.AddRepositories();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowSpecificOrigins",
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200")
                                                 .AllowAnyMethod()
                                                 .AllowAnyHeader();
                                  });
            });
            // Add services to the container.

            builder.Services.AddControllers();
            /*builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "/User/Login";
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });*/

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                app.UseHttpsRedirection();
                app.UseRouting(); 
                app.UseCors("MyAllowSpecificOrigins");
               
                
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseEndpoints(endpoints =>
                {
                endpoints.MapControllers();
                });



            app.Run();

            }
     }
  }

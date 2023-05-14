using DoctorWho.Db;
using DoctorWho.Db.Repositories.Implementations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DoctorWho.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DoctorWhoCoreDbContext>(DbContextOptions => DbContextOptions.UseSqlServer(builder.Configuration["ConnectionStrings:DoctorWhoDbConnectionString"]));

            builder.Services.AddValidatorsFromAssemblyContaining<DoctorValidator>(); 
            builder.Services.AddValidatorsFromAssemblyContaining<EpisodeValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<EnemyValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CompanionValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<AuthorValidator>();

            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.Run();
        }
    }
}
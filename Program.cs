
using Hangfire;
using Hangfire.MemoryStorage;

namespace HangfireJob
{
    public class Program
    {
        public static void Main(string[] args)
        {



            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHangfire(config =>
            {
                config.UseMemoryStorage();
            });
            builder.Services.AddHangfireServer();
            builder.Services.AddTransient<JobService>();
            builder.Services.AddTransient<EmailService>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseHangfireDashboard();
            app.MapControllers();

            app.Run();
        }
    }
}
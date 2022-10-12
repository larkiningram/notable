using Newtonsoft.Json;
using notable.Services;

namespace notable
{
    public class Startup
    {
        public Startup(IConfiguration env)
        {
            Configuration = (IConfigurationRoot)env;
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers()
                .ConfigureApiBehaviorOptions(opt => { opt.SuppressModelStateInvalidFilter = true; });

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            services.AddHttpContextAccessor();

            services.AddSwaggerGen();

            services.AddScoped<IAppointmentService, AppointmentService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test1 Api v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();
        }
    }
}
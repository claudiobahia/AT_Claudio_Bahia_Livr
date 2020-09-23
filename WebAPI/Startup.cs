using System.Text;
using Editora.Repository;
using Editora.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<UsuarioService>();

            services.AddDbContext<EditoraContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("EditoraConnection"));
            });

            var key = Encoding.UTF8.GetBytes(this.Configuration["Token:Secret"]);

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = "Bearer";
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters.ValidIssuer = "EDITORA-API";
                o.TokenValidationParameters.ValidAudience = "EDITORA-API";
                o.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(key);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

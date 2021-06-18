using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Music_Review_Application_Services.Interfaces;
using Music_Review_Application_DB_Managers;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Services;

namespace Music_Review_Application_GUI
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
            services.AddRazorPages();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddSingleton<IAlbumDbManager, AlbumDbManager>();
            services.AddSingleton<IArtistDbManager, ArtistDbManager>();
            services.AddSingleton<IGenreDbManager, GenreDbManager>();
            services.AddSingleton<ISongDbManager, SongDbManager>();
            services.AddSingleton<IUserListDbManager, UserListDbManager>();
            services.AddSingleton<ISqlManager, SqlManager>();
            services.AddSingleton<IImageConverter, ImageConverter>();
            services.AddSingleton<ICreateService, CreateService>();
            services.AddSingleton<IDiscoverService, DiscoverService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

using BusinessObject;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;

namespace eBookStoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Book>("Books").EntityType.HasKey(x => x.BookId);
            builder.EntitySet<User>("Users").EntityType.HasKey(x => x.UserId);
            builder.EntitySet<Role>("Roles").EntityType.HasKey(x => x.RoleId);
            builder.EntitySet<Author>("Authors").EntityType.HasKey(x => x.AuthorId);
            builder.EntitySet<BookAuthor>("BookAuthors").EntityType.HasKey(x => new { x.AuthorId, x.BookId });
            builder.EntitySet<Publisher>("Publishers").EntityType.HasKey(x => x.PublisherId);
            return builder.GetEdmModel();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddControllers();
            //services.AddDbContext<eBookStoreDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("eBookStore")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eBookStoreAPI", Version = "v1" });
            });
            services.AddControllers().AddOData(option => option
            .Select()
            .Filter()
            .Count()
            .OrderBy()
            .Expand()
            .SetMaxTop(100)
            .AddRouteComponents("odata", GetEdmModel()));
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "eBookStoreAPI v1"));
            }

            app.UseODataBatching();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

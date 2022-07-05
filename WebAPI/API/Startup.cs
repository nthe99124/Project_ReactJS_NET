using API.Common;
using API.Common.Interface;
using API.Repositories;
using API.Repositories.Interface;
using API.Service;
using API.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // if not use, system back error: "unable to resolve service for type..."
            //services.AddScoped<DbContext, MyDbContext>();
            //services.AddDbContext<MyDbContext>(option => option.UseSqlServer("Data Source=.;Initial Catalog=LaptopDB;Integrated Security=True"));

            // add to use in controller
            services.AddDbContext<MyDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("Laptop"));
            });
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddControllers();

            // các Interface khác cần addTransient
            //AddTransient - Một thể hiện của service sẽ được cung cấp đến mỗi class request nó.
            //AddScoped - Một thể hiện của service sẽ được tạo trên mỗi request.
            //AddSingleton - Một thể hiện của service sẽ được tạo cho vòng đời của ứng dụng

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IProductColorRepository, ProductColorRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IBillStatusRepository, BillStatusRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IFavoriteListRepository, FavoriteListRepository>();
            services.AddScoped<INewsImageRepository, NewsImageRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IProductColorService, ProductColorService>();
            services.AddScoped<IProductImageService, ProductImageService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IBillStatusService, BillStatusService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IFavoriteListService, FavoriteListService>();
            services.AddScoped<INewsImageService, NewsImageService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IUserService, UserService>();

            #region config Swagger - Use Bearer
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        In = ParameterLocation.Header,
                        Description = "Bearer Auth"
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        new string[]{}
                    }
                    });
                });
            #endregion

            #region config token
            var secretKey = Configuration["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        // các mã xác thực thông báo
                        //grant token
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        //sign token
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                        ClockSkew = TimeSpan.Zero
                    };
                });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                // return exception to page
                app.UseDeveloperExceptionPage();
                // middleware for swagger
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
            // auto redirection from http to https
            app.UseHttpsRedirection();
            app.UseRouting();

            //need for authentication and author
            app.UseAuthentication();
            app.UseAuthorization();

            //need when config body with status code
            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    if (context.Response.StatusCode == 404)
            //    {
            //        //object for error
            //        var rs = new Paging(1, 5);
            //        context.Response.ContentType = "application/json";
            //        await context.Response.WriteAsync(JsonConvert.SerializeObject(rs), Encoding.UTF8);
            //    }
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

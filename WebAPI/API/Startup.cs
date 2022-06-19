using API.Common;
using API.Common.Interface;
using API.Repositories;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Model.BaseEntity;
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

        public IConfiguration Configuration { get; }

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
            services.AddTransient<IGenericRepository<Product>, GenericRepository<Product>>();
            services.AddTransient<IGenericRepository<Image>, GenericRepository<Image>>();
            services.AddTransient<IGenericRepository<ProductColor>, GenericRepository<ProductColor>>();
            services.AddTransient<IGenericRepository<ProductImage>, GenericRepository<ProductImage>>();
            services.AddTransient<UnitOfWork>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IProductColorRepository, ProductColorRepository>();
            services.AddTransient<IProductImageRepository, ProductImageRepository>();

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

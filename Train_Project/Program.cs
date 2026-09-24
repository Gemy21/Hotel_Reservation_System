using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Train_Project.Authentication;
using Train_Project.Authentication.AuthEntity;
using Train_Project.Data;
using Train_Project.Handlers;
using Train_Project.Middleware;
using Train_Project.Services;
using Train_Project.Services.Interfaces;

namespace Train_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer",
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Description = "Enter: Bearer {your JWT token}"
                    });

                options.AddSecurityRequirement(
                    new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                    {
                        {
                            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                            {
                                Reference =
                                    new Microsoft.OpenApi.Models.OpenApiReference
                                    {
                                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                            },
                            Array.Empty<string>()
                        }
                    });
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    op => op.MigrationsAssembly(
                        typeof(AppDbContext).Assembly.FullName)));

            builder.Services.AddScoped<IBuildingService, BuildingService>();
            builder.Services.AddScoped<IComponentService, ComponentService>();
            builder.Services.AddScoped<IHotelService, HotelService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IStandardRoomServices, StandardRoomServices>();
            builder.Services.AddScoped<IVipRoomService, VipRoomService>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();

            var jwtoptions =
                builder.Configuration.GetSection("Jwt").Get<JwtOption>();

            builder.Services.AddSingleton<IOptions<JwtOption>>(
                Options.Create(jwtoptions));

            builder.Services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateAudience = true,
                            ValidAudience = jwtoptions.Audience,

                            ValidateIssuer = true,
                            ValidIssuer = jwtoptions.Issuer,

                            ValidateIssuerSigningKey = true,

                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(
                                        jwtoptions.SignKey))
                        };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                {
                    policy.RequireRole(Roles.Admin);
                });

                options.AddPolicy("CustomerOnly", policy =>
                {
                    policy.RequireRole(Roles.Customer);
                });
            });

            // Team exception handlers
            builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
            builder.Services.AddExceptionHandler<BusinessExceptionHandler>();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            var app = builder.Build();

            app.UseExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Custom middleware
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<RateLimitMiddleware>();

            // Authentication & authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
using Hotel.AccountManagement.Data;
using Hotel.AccountManagement.Entities;
using Hotel.HotelManagement;
using Hotel.HotelManagement.Data;
using Hotel.Restaurant;
using Hotel.Restaurant.Data;
using Hotel.BookManagement;
using Hotel.BookManagement.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hotel.AccountManagement.Helpers;
using Microsoft.OpenApi.Models;
using Stripe;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hotel.BookManagement.DTOs;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Adding Identity with custom User and Role classes
builder.Services.AddIdentityCore<User>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.SignIn.RequireConfirmedEmail = false;
})
    .AddRoles<Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddEntityFrameworkStores<AccountManagementDbContext>()
    .AddSignInManager<SignInManager<User>>()
    .AddDefaultTokenProviders();

// JWT Authentication setup
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var tokenKey = "best-secure-token--best-secure-token--best-secure-token--best-secure-token--best-secure-token--best-secure-token";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };

    });

// Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});

// Adding DbContexts
builder.Services.AddDbContext<HotelManagementDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<AccountManagementDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseSqlServer(connectionString));

// Module and service setup
builder.Services.AddHotelManagementModule();
builder.Services.AddAccountManagementModule(builder.Configuration);
builder.Services.AddRestaurantModule();
builder.Services.AddBookManagementModule();


// Stripe Configuration
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

// Controllers and AutoMapper
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Swagger setup with JWT Bearer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// CORS setup for allowed origins

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:4200", "https://127.0.0.1:4200", "https://localhost:61529", "https://127.0.0.1:61529")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials());
});



var app = builder.Build();



// Middleware setup
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

// Apply CORS policy
app.UseCors("AllowSpecificOrigin");


// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Seed database
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();


    await Seed.SeedUsers(userManager, roleManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while populating the database.");
}
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Static files and controller routes
app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();

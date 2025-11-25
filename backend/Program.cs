using Microsoft.EntityFrameworkCore;
using Hongsa.Rtms.Api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Scalar.AspNetCore;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

// Entity Framework Core MS SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Adding Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
       { 
        // Require at least one digit
        options.Password.RequireDigit = true;
        // Require at least one lowercase character
        options.Password.RequireLowercase = true;
        // Require at least one uppercase character
        options.Password.RequireUppercase = true;
        // Require at least one special character
        options.Password.RequireNonAlphanumeric = false;
        // Require at least 6 characters
        options.Password.RequiredLength = 6;
        })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options  => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetSection("JWT:ValidAudience").Value!,
        ValidIssuer = builder.Configuration.GetSection("JWT:ValidIssuer").Value!,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Secret").Value!))
    };
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

     // Scalar API Reference Configuration
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Hongsa RTMS API (Scalar)")
            .WithTheme(ScalarTheme.Laserwave) // light, dark, purple
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

// ปิดการใช้งาน HTTPS ชั่วคราว (ถ้าจำเป็น)

// app.UseHttpsRedirection();

// Add Authentication
app.UseAuthentication();

// Add Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();

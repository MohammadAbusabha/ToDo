using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
using ToDo.Core.Mapping;
using ToDo.Core.Resources;
using ToDo.Core.Services;
using ToDo.Core.SpecTest;
using ToDo.Infrastructure;
using ToDo.Infrastructure.Context;
using ToDo.Infrastructure.ServiceTest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<DataContext>();

//Registry

builder.Services.AddScoped<IDataService, DataService>();//data 
builder.Services.AddScoped<IAccountService, AccountService>();//account 
builder.Services.AddTransient<IJWTService, JWTService>();//jwt
builder.Services.AddTransient<IRoleService, RoleService>();//roles
builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();//CurrentUser 
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));//Repo
builder.Services.AddScoped(typeof(ISpecification<>), typeof(Specifications<>));//Spec
builder.Services.AddScoped<Seeder>();//seeder

builder.Services.AddScoped<IOrganizationService, OrganizationService>();// organization

//database connection

builder.Services.AddDbContext<DataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("MyCon")));

//JWT / Authentication

builder.Services.Configure<JwtSettingsResource>(
    builder.Configuration.GetSection("JWT"));

var key = builder.Configuration["JWT:SecretKey"];
if (string.IsNullOrEmpty(key))
    throw new Exception("JWT SECRET KEY IS NULL HERE");

builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(o =>
{
    o.ClaimsIssuer = builder.Configuration["JWT:Issuer"];
    o.Audience = builder.Configuration["JWT:Audience"];
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
        ValidateLifetime = true,
    };
    o.MapInboundClaims = false;
});

//Authorization //Policy

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    o.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Mapster

Mapping.ApplyMapping();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seeding

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var seeder = service.GetRequiredService<Seeder>();
    await seeder.SeedAsync();
}

app.Run();

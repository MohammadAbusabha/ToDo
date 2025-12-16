using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using ToDo;
using ToDo.Context;
using ToDo.Entities;
using ToDo.Handlers;
using ToDo.Interfaces;
using ToDo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

//

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<DataContext>();

//Registry

builder.Services.AddScoped<IDataOperationService, DataOperationsService>();//data api
builder.Services.AddScoped<IAccountManagementService, AccountManagementService>();//account api
builder.Services.AddTransient<IJWTtokenCreationService, JWTtokenCreationService>();//jwt
builder.Services.AddTransient<IRoleManagementService, RoleManagementService>();//roles
builder.Services.AddScoped<IPrivilegeManagementService, PrivilegeManagementService>();//permissions
builder.Services.AddSingleton<IAuthorizationHandler, AdminBypass>();//admin role bypass
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();//CurrentUser info

//database connection

builder.Services.AddDbContext<DataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("MyCon")));

//JWT / Authentication

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

//Authorization

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("Delete", policy => policy.RequireClaim("Permission", "Delete"));
    o.AddPolicy("Write", policy => policy.RequireClaim("Permission", "Delet", "Write"));
    o.AddPolicy("Read", policy => policy.RequireClaim("Permission", "Delet", "Write", "Read"));
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

app.Run();

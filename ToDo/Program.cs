using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using ToDo.Controllers;
using ToDo.Interfaces;
using ToDo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo.Entitys;
using ToDo.IdentityEntity_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ToDo.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IDataOperationService, DataOperationsService>();
builder.Services.AddScoped<IAccountManagementService,  AccountManagementService>();
builder.Services.AddTransient<IJWTtokenCreationService, JWTtokenCreationService>();
builder.Services.AddTransient<IRoleManagementService, RoleManagementService>();
builder.Services.AddScoped<IPermissionManagementService, PermissionManagementService>();
builder.Services.AddSingleton<IAuthorizationHandler, AdminBypass>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, DataContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole, DataContext, Guid>>();

builder.Services.AddDbContext<DataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("MyCon")));

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
    o.MapInboundClaims =false;
});

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    o.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
    o.AddPolicy("CanCreate", policy => policy.RequireClaim("Permission", "CanCreate"));
    o.AddPolicy("CanUpdate", policy => policy.RequireClaim("Permission", "CanUpdate"));
    o.AddPolicy("CanGet", policy => policy.RequireClaim("Permission", "CanGet"));
    o.AddPolicy("CanDelete", policy => policy.RequireClaim("Permission", "CanDelete"));
    o.AddPolicy("CanList", policy => policy.RequireClaim("Permission", "CanList"));
    o.AddPolicy("CanSearch", policy => policy.RequireClaim("Permission", "CanSearch"));
});

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

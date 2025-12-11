using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;
using System.Text;
using ToDo;
using ToDo.Context;
using ToDo.Entitys;
using ToDo.Handlers;
using ToDo.IdentityEntity_s;
using ToDo.Interfaces;
using ToDo.Resources;
using ToDo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddScoped<IDataOperationService, DataOperationsService>();//data api
builder.Services.AddScoped<IAccountManagementService, AccountManagementService>();//account api
builder.Services.AddTransient<IJWTtokenCreationService, JWTtokenCreationService>();//jwt
builder.Services.AddTransient<IRoleManagementService, RoleManagementService>();//roles
builder.Services.AddScoped<IPermissionManagementService, PermissionManagementService>();//permissions
builder.Services.AddSingleton<IAuthorizationHandler, AdminBypass>();//admin role bypass

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
    o.MapInboundClaims = false;
});

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("CanCreate", policy => policy.RequireClaim("Permission", "CanCreate"));
    o.AddPolicy("CanUpdate", policy => policy.RequireClaim("Permission", "CanUpdate"));
    o.AddPolicy("CanGet", policy => policy.RequireClaim("Permission", "CanGet"));
    o.AddPolicy("CanDelete", policy => policy.RequireClaim("Permission", "CanDelete"));
    o.AddPolicy("CanList", policy => policy.RequireClaim("Permission", "CanList"));
    o.AddPolicy("CanSearch", policy => policy.RequireClaim("Permission", "CanSearch"));
});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

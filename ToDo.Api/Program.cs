using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDo.Api.Authorization;
using ToDo.Core.Entities;
using ToDo.Core.Enums;
using ToDo.Core.Evaluator;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;
using ToDo.Infrastructure;
using ToDo.Infrastructure.BaseSpecifications;
using ToDo.Infrastructure.Context;
using ToDo.Infrastructure.Resolver;
using ToDo.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

//

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<DataContext>();

//Registry

builder.Services.AddScoped<IDataService, DataService>();//data 
builder.Services.AddScoped<IAccountService, AccountService>();//account 
builder.Services.AddTransient<IJWTService, JWTService>();//jwt
builder.Services.AddTransient<IRoleService, RoleService>();//roles
builder.Services.AddScoped<IPrivilegeEvaluator, PrivilegeEvaluator>();//permissions
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();//CurrentUser 


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));//Repo
builder.Services.AddScoped(typeof(IBaseSpecification<>), typeof(BaseSpecifications<>));//Spec
//builder.Services.AddScoped<IAuthorizationRequirement, PrivilegeRequirement>();//PrivilegeRequirement
//builder.Services.AddScoped<IPrivilegeEvaluator, PrivilegeEvaluator>();//PrivilegeEvaluator
//builder.Services.AddScoped<IRoleLevelResolver, RoleLevelResolver>();//RoleLevelResolver

//database connection

builder.Services.AddDbContext<DataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("MyCon")));

//JWT / Authentication

builder.Services.Configure<JwtSettingsResource>(
    builder.Configuration.GetSection("JWT"));

var test = builder.Configuration["JWT:SecretKey"];
if (string.IsNullOrEmpty(test))
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
    o.AddPolicy("Owner", policy => policy.Requirements.Add(new PrivilegeRequirement(Privileges.Owner)));
    o.AddPolicy("CanDelete", policy => policy.Requirements.Add(new PrivilegeRequirement(Privileges.Delete)));
    o.AddPolicy("CanWrite", policy => policy.Requirements.Add(new PrivilegeRequirement(Privileges.Write)));
    o.AddPolicy("CanRead", policy => policy.Requirements.Add(new PrivilegeRequirement(Privileges.Read)));
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

// Role Seeding 

using (var scope = app.Services.CreateScope())
{
    var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

    foreach (var roleName in Enum.GetNames(typeof(RoleLevel)))
    {
        if (!await rolemanager.RoleExistsAsync(roleName))
        {
            await rolemanager.CreateAsync(new ApplicationRole() { Name = roleName, Value = (int)Enum.Parse<RoleLevel>(roleName) });
        }
    }
}

app.Run();

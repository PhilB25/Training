

using Asp.Versioning;
using AT_API.App_Code;
using AT_API.Model_Action;
using AT_API.Services;
using AT_API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("WrokshopAPI");
builder.Services.AddDbContext<WorkshopAPI>(options => options.UseSqlServer(connection));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<ICourseLibraryRepository, CourseLibraryRepository>();

builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "AT_API",
        Version = "v1",
        Description = "9EXPert Course Library API",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Email = "ajarn_ek@outlook.com",
            Name = "Ekpongtorn Ueaprasertvanich",
            Url = new Uri("http://www.ajarn-ek.com")
        }
    });
    c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "AT_API",
        Version = "v2",
        Description = "9EXPert Course Library API",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Email = "ajarn_ek@outlook.com",
            Name = "Ekpongtorn Ueaprasertvanich",
            Url = new Uri("http://www.ajarn-ek.com")
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your Bearer token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var cmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
    c.IncludeXmlComments(cmlCommentFullPath);

});
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
}).AddMvc();
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "AT_API");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "AT_API V2");
        options.RoutePrefix = "";


    });
}

app.UseHttpsRedirection();
app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
using System.Text.Json.Serialization;
using DiplomaThesisDigitalization.Data;
using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Helpers;
using DiplomaThesisDigitalization.Services;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// Krijimi i ndërtuesit të aplikacionit web
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Shtimi i shërbimeve MVC për trajtimin e kërkesave HTTP
builder.Services.AddControllers();

// Shtimi i shërbimeve të kufizuara për injektimin e varësive (depenendency)
builder.Services.AddScoped<JwtHelper>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFieldService, FieldService>();
builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<IMentorService, MentorService>();
builder.Services.AddScoped<IFieldService, FieldService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITitleService, TitleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IFacultyService, FacultyService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


// Shtimi i shërbimeve për dokumentimin e Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Shtimi i Entity Framework DbContext me një "connection string"
builder.Services.AddDbContext<ThesisDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("ThesisDbConnectionString")));

// Ndërtimi i aplikacionit
var app = builder.Build();

// Konfigurimi i Swagger UI për "development environment"
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Konfigurimi i CORS për të lejuar kërkesat nga një origjinë e caktuar
app.UseCors(options => options.WithOrigins(new[] { "http://localhost:3000" }).AllowAnyHeader()
            .AllowAnyMethod().AllowCredentials());

// Enabling HTTPS redirection.
app.UseHttpsRedirection();

// Enabling authorization.
app.UseAuthorization();

// Mapping controllers to handle incoming HTTP requests.
app.MapControllers();

// Ekzekutimi i aplikacionit
app.Run();

using System.Text.Json.Serialization;
using DiplomaThesisDigitalization.Data;
using DiplomaThesisDigitalization.Data.UnitOfWork;
using DiplomaThesisDigitalization.Helpers;
using DiplomaThesisDigitalization.Services;
using DiplomaThesisDigitalization.Services.IServices;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ThesisDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("ThesisDbConnectionString")));
var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.WithOrigins(new[] { "http://localhost:3000" }).AllowAnyHeader()
            .AllowAnyMethod().AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();

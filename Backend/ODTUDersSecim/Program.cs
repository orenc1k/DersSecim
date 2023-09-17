using ODTUDersSecim.Models;
using ODTUDersSecim.Services;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

// Add configuration settings from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json");

// Add services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<SubjectsService>();
builder.Services.AddScoped<SubjectSectionsService>();
builder.Services.AddScoped<SectionDaysService>();
builder.Services.AddScoped<MustCourseService>();
builder.Services.AddScoped<AvailableCoursesService>();
builder.Services.AddScoped<ElectiveCoursesService>();

builder.Services.AddSwaggerGen();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<ODTUDersSecimDBContext>(options =>
{
    _ = options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), o => o.CommandTimeout(360));

});

var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();






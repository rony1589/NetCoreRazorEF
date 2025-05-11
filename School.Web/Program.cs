using Microsoft.EntityFrameworkCore;
using School.Application;
using School.Application.Mapping;
using School.Infrastructure;
using School.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

//configurar la cadena de conexion
builder.Services.AddDbContext<SchoolDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDbConnection")));

//Inyección de dependencias de infraestructura
builder.Services.AddInfrastructure();

// //Inyección de dependencias de Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

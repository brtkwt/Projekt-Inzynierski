using Microsoft.EntityFrameworkCore;
using Projekt_Inżynierski.Data;
using Projekt_Inżynierski.Data.Repository;
using Projekt_Inżynierski.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();  // dodane

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, ProductRepository>();    // dodane
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();  // dodane
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();    // dodane

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // dodane

app.Run();

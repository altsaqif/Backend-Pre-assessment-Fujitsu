using Microsoft.EntityFrameworkCore;
using GudangBarangAPI.Data;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Gudang Barang API", 
        Version = "v1",
        Description = "API for managing warehouses and items",
        TermsOfService = new Uri("https://termsofservice.com/term"),
        Contact = new OpenApiContact
        {
            Name = "Al Tsaqif Nugraha Ahmad",
            Email = "altsaqifnugraha19@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/al-tsaqif-nugraha-ahmad-0149921b2/")
        },
        License = new OpenApiLicense
        {
            Name = "Nugraha Copyright 2024",
            Url = new Uri("https://google.com")
        } 
    });

    // Get XML comments file path
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    
    // Include XML comments in Swagger
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gudang Barang API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
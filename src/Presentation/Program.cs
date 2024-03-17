using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Protheus API",
        Version = "2023.1.0.0",
        Description = "Cadastro de produto e atualização de estoque",
        Contact = new OpenApiContact
        {
            Email = "gabrielgbr.contato@gmail.com",
            Name = "Gabriel Silva",
            Url = new Uri("https://www.linkedin.com/in/gbrgabriel/")
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://www.mit.edu/~amini/LICENSE.md")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


var app = builder.Build();

var culture = new CultureInfo("en-US");

CultureInfo.CurrentCulture = culture;
CultureInfo.CurrentUICulture = culture;

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1);
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Protheus V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

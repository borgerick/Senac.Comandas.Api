using Comandas.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<ComandasDbContext>(options =>
    options.UseSqlite("Data Source=comandas.db")
    //options.UseSqlServer("") // Connection string can be added here
    );


// Adiciona CORS para permitir o frontend rodando em localhost:5500
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVSCodeFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5500", "http://127.0.0.1:5500")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Criar o banco de dados
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ComandasDbContext>();
    await db.Database.MigrateAsync();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Ativa o CORS antes do Authorization
app.UseCors("AllowVSCodeFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
using Microsoft.EntityFrameworkCore;
using RoofStockBackend.Contextos;
using RoofStockBackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"))
);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<SrvcUsuario>();
builder.Services.AddScoped<SrvcEstoque>();
builder.Services.AddScoped<SrvcEstoqueProduto>();
builder.Services.AddScoped<SrvcMarca>();
builder.Services.AddScoped<SrvcEmpresa>();
builder.Services.AddScoped<SrvcFechamentoEstoque>();
builder.Services.AddScoped<SrvcMovimentacaoEstoque>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

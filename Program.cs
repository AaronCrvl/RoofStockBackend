using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Modelos.DTO.Movimentação_Estoque;
using RoofStockBackend.Services;
using RoofStockBackend.Serviços;
using RoofStockBackend.Validadores;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("AzureDB"))
);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<SrvcAutenticacao>();
builder.Services.AddScoped<SrvcUsuario>();
builder.Services.AddScoped<SrvcEstoque>();
builder.Services.AddScoped<SrvcEstoqueProduto>();
builder.Services.AddScoped<SrvcMarca>();
builder.Services.AddScoped<SrvcEmpresa>();
builder.Services.AddScoped<SrvcFechamentoEstoque>();
builder.Services.AddScoped<SrvcMovimentacaoEstoque>();

builder.Services.AddScoped<IValidator<Produto>, VldrProduto>();
builder.Services.AddScoped<IValidator<Estoque>, VldrEstoque>();
builder.Services.AddScoped<IValidator<MovimentacaoEstoque>, VldrMovimentacaoEstoque>();
builder.Services.AddScoped<IValidator<ItemMovimentacaoEstoque>, VldrItemMovimentacaoEstoque>();
builder.Services.AddScoped<IValidator<FechamentoEstoque>, VldrFechamentoEstoque>();
builder.Services.AddScoped<IValidator<ItemFechamentoEstoque>, VldrItemFechamentoEstoque>();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new() { Title = "RoofStock´s API", Version = "v1" });

//    // Define the OAuth2.0 scheme that's in use (i.e., Implicit Flow)
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                },
//                Scheme = "oauth2",
//                Name = "Bearer",
//                In = ParameterLocation.Header,
//            },
//            new List<string>()
//        }
//    });
//});

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", opt =>
{
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        //ValidIssuer = "localhost",
        ValidateIssuer = false,
        ValidateAudience = false,       
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_keyyour_superyour_"))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}
if (!app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();

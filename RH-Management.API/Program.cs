using Microsoft.EntityFrameworkCore;
using RH_Management.API.Data;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Diretório atual: " + Directory.GetCurrentDirectory());
Console.WriteLine("Arquivos encontrados: " + string.Join(", ", Directory.GetFiles(Directory.GetCurrentDirectory())));

builder.Services.AddDbContext<RHContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RHConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();


// ESSA FOI A PARTE PARA TESTAR ANTES DO BANCO DE DADOS

/*builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Corrigido: Método assíncrono para inicialização
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RHContext>();

    if (!context.Funcionarios.Any())
    {
        var funcionario = new Funcionario
        {
            Nome = "Fulano de Teste",
            Cargo = "Analista",
            DataAdmissao = DateTime.Now,
            Salario = 3000,
            Status = true
        };

        context.Funcionarios.Add(funcionario);
        context.SaveChanges(); // Síncrono

        context.HistoricoAlteracoes.Add(new HistoricoAlteracao
        {
            FuncionarioId = funcionario.Id,
            CampoAlterado = "Cargo",
            ValorAntigo = null,
            ValorNovo = "Analista",
            DataAlteracao = DateTime.Now
        });

        context.SaveChanges(); // Síncrono
    }
}

app.Run();*/
using Microsoft.EntityFrameworkCore;
using Vitor.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>(options =>
    options.UseSqlite("Data Source=Alisson_Vitor.db"));

var app = builder.Build();

app.MapGet("/", () => "API de Folha de Pagamentos");
app.MapPost("/api/funcionario/cadastrar", async (Funcionario funcionario, AppDataContext db) =>
{
    db.Funcionarios.Add(funcionario);
    await db.SaveChangesAsync();
    return Results.Ok(funcionario);
});

app.MapGet("/api/funcionario/listar", async (AppDataContext db) =>
{
    var funcionarios = await db.Funcionarios.ToListAsync();
    return Results.Ok(funcionarios);
});

app.MapPost("/api/folha/cadastrar", async (Folha folha, AppDataContext db) =>
{
    var funcionario = await db.Funcionarios.FindAsync(folha.FuncionarioId);
    if (funcionario == null)
        return Results.NotFound();

    folha.CalcularFolha(funcionario);
    db.Folhas.Add(folha);
    await db.SaveChangesAsync();
    return Results.Ok(folha);
});

app.MapGet("/api/folha/listar", async (AppDataContext db) =>
{
    var folhas = await db.Folhas.Include(f => f.Funcionario).ToListAsync();
    return Results.Ok(folhas);
});

app.MapGet("/api/folha/buscar/{cpf}/{mes}/{ano}", async (string cpf, int mes, int ano, AppDataContext db) =>
{
    var folha = await db.Folhas
                        .Include(f => f.Funcionario)
                        .FirstOrDefaultAsync(f => f.Funcionario.CPF == cpf && f.Mes == mes && f.Ano == ano);

    if (folha == null)
        return Results.NotFound();

    return Results.Ok(folha);
});


app.Run();

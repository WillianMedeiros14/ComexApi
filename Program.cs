using System.Reflection;
using ComexAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

// Configuração inicial do host e configuração
var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        // Adiciona variáveis de ambiente à configuração
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        // Obtém a string de conexão diretamente da configuração
        var configuration = context.Configuration;
        var connectionString = configuration.GetValue<string>("DATABASE_URL");

        // Configura o contexto do banco de dados com a string de conexão
        services.AddDbContext<ProdutoContext>(options =>
            options.UseNpgsql(connectionString));

        // Outros serviços
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddControllers().AddNewtonsoftJson();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ComexAPI", Version = "v1" });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    })
    .Build(); // Cria o host

// Cria o WebApplication a partir do builder configurado
var app = WebApplication.CreateBuilder(args).Build();

// Configure o pipeline de requisições HTTP
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () =>
{
    return "Executando";
})
.WithName("/")
.WithOpenApi();

app.UseHttpsRedirection();
app.MapControllers();

// Aplica as migrações ao iniciar a aplicação
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<ProdutoContext>();
//     try
//     {
//         dbContext.Database.Migrate();
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine($"Error applying migrations: {ex.Message}");
//         Console.WriteLine(ex.StackTrace);
//         throw;
//     }
// }

app.Run();

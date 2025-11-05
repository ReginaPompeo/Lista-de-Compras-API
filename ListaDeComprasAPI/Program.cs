using ListaDeComprasAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Especificações sobre o banco de dados, nesse caso o MySQL ou seja ao colocar DbContext ele vai usar essas configurações de banco
builder.Services.AddDbContext<AppDbContext>(options => //Builder.Services é onde vocÊ registra o que o ASP.NET vai precisar injetar nos controladores e AddDbContext<AppDbContext> diz que você quer registrar o seu contexto AppDbContext como um serviço.
    options.UseMySql( //Estou dizendo pra ele se conectar ao MySQL
        builder.Configuration.GetConnectionString("DefaultConnection"), //O builder.Configuration acessa as configurações do seu appsettings.json. Já o GetConnectionString("DefaultConnection") busca a string de conexão nomeada "DefaultConnection".
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")) //Para descobrir automaticamente a versão do MySQL
    )
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

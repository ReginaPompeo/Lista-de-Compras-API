using Microsoft.EntityFrameworkCore; //É o ORM (Object-Relational Mapper) da Microsoft. Ele serve pra trabalhar com o banco de dados usando classes do C# sem mexer no SQL direito.
using ListaDeComprasAPI.Models;

namespace ListaDeComprasAPI.Data
{
    public class AppDbContext : DbContext //Criando a classe de contexto, ponto central da comunicação dcom o banco. DbContext é uma classe do EF Core
    {
      
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } //Esse construtor recebe as opções de configuração (string de conexão, tipo de banco, etc.)
        public DbSet<ItemModel> Itens { get; set; } //Essa é a parte que mapeia a tabela do banco e significa que no banco vai existir uma tabela chamada "Itens" que guarda os objetos do tipo ItemModel
    }
}

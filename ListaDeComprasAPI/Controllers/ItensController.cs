using ListaDeComprasAPI.Data; //Ele importa AppDbContext.
using ListaDeComprasAPI.Models; //Ele traz o ItemModel.
using Microsoft.AspNetCore.Mvc; //Esse traz classes do ASP.NET para criar controllers, atributos como [HttpGet], ActionResult, etc.
using Microsoft.EntityFrameworkCore; //Ele traz métodos assíncronos e EntityState usados com EF Core.
using Microsoft.Data.Sqlite;

namespace ListaDeComprasAPI.Controllers
{
    [Route("api/[controller]")] //Define os endpoints como api/itens
    [ApiController] //Ativa convenções automáticas (model binding/validation e respostas 400 automáticas quando o modelo é inválido).
    public class ItensController : ControllerBase //Classe base para controllers sem view (API).
    {
        private readonly AppDbContext _context; //Private porque só pode ser acessada dentro da classe do Controller, readonly porque você só pode atribuir um valor para essa variável uma única vez, não pode ter outros valores tipo _context = new AppDbContext();

        public ItensController(AppDbContext context)
        {
            _context = context; //Acessa o AppDbContext (conexão com o banco)e acessa a tabela Itens
        }

        //Para listar todos, o GET indica uma query 
        [HttpGet]
        public async Task<ActionResult<List<ItemModel>>> GetItens()
        {
            var itens = await _context.Itens.ToListAsync(); //Consulta o banco e traz todos os itens de forma assíncrona
            return Ok(itens);//Retorna 200 OK + a lista
        }

        //Para listar o item por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemModel>> GetItem(int id)
        {
            var item = await _context.Itens.FindAsync(id);//Esse FindAsync(id) é para ele buscar a chave primária
            if (item == null) return NotFound("Item não encontrado.");//Se não existe o item, o retorno é 404
            return Ok(item);
        }

        //Para criar um item
        [HttpPost]
        public async Task<ActionResult<ItemModel>> CreateItem(ItemModel item) //Recebe o ItemModel do corpo da requisição (binding automático).
        {
            _context.Itens.Add(item); //Add(item) marca para inserção
            await _context.SaveChangesAsync(); //SaveChangesAsync() executa o INSERT.
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item); //Retorna 201 Created e inclui cabeçalho Location apontando para o endpoint que retorna o recurso criado (GET /api/itens/{id}), além do item no corpo.
        }

        //Para atualizar itens através do Id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(int id, ItemModel item)
        {
            if (id != item.Id) return BadRequest("ID inválido."); //Verifica se o ID existe ou não
            var itemExistente = await _context.Itens.FindAsync(id); //Procura se o Id existe no banco
            if (itemExistente == null) return NotFound("Item não encontrado.");
            // Atualiza os campos manualmente
            itemExistente.Name = item.Name;
            itemExistente.Amount = item.Amount;
            itemExistente.Purchased = item.Purchased;
            await _context.SaveChangesAsync(); //Aplica a alteração no banco de dados
            return NoContent(); //Retorna 204 NO content dizendo que foi atualizado
        }

        //Deletar itens por Id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var item = await _context.Itens.FindAsync(id); //Procura se o Id existe no banco
            if (item == null) return NotFound("Item não encontrado.");
            _context.Itens.Remove(item); //Marca para exclusão
            await _context.SaveChangesAsync(); //Executa o delete
            return NoContent(); //Retorna 204 No content no final
        }

        //[HttpGet("testar-conexao")]
        //public ActionResult TestarConexao()
        //{
        //    try
        //    {
        //        using var connection = new MySqlConnection(_context.Database.GetConnectionString());
        //        connection.Open();
        //        return Ok("Conectado com sucesso!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Erro: {ex.Message}");
        //    }
        //}

        [HttpGet("testar-conexao")]
        public ActionResult TestarConexao()
        {
            try
            {
                var connectionString = _context.Database.GetConnectionString();
                using var connection = new SqliteConnection(connectionString);
                connection.Open();
                return Ok("Conectado com sucesso ao SQLite!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao conectar: {ex.Message}");
            }
        }

    }
}

//Os métodos são assíncronos (async + await) para não bloquear a thread do servidor durante operações de I/O (consulta ao banco).

//ActionResult<T> permite retornar um resultado tipado (T) ou outros ActionResults (como NotFound(), BadRequest()).
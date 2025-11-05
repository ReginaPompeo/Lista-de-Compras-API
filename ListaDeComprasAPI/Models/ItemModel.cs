namespace ListaDeComprasAPI.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; //É o mesmo que = ""; e serve para evitar erros dizendo que a string é nula, porque na verdade ela começa vazia mesmo, isso ajuda nas boas práticas e ajuda no entity framework ao criar o registro no banco já sabendo que o campo é vazio e não nulo.
        public int Amount {  get; set; }
        public bool Purchased { get; set; }
    }
}

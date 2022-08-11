using DesafioAsp.Data.Configurations;
using MongoDB.Driver;
using DesafioAsp.Models;
using DesafioAsp.Models.InputModels;

namespace DesafioAsp.Data.Repositories
{
    public class PokemonRepository : InterfacePokemon
    {

        private readonly IMongoCollection<Pokemon> _pokemon;

        public PokemonRepository(IDatabaseConfig databaseConfig)
        {
            var client = new MongoClient(databaseConfig.ConnectionString);
            var database = client.GetDatabase(databaseConfig.DatabaseName);

            _pokemon = database.GetCollection<Pokemon>("pokemon");
        }

        public void Adicionar(Pokemon pokemon)
        {
            _pokemon.InsertOne(pokemon);
        }

        public void Atualizar(string id, Pokemon pokemonAtualizado)
        {
            _pokemon.ReplaceOne(pokemon => pokemon.Id == id, pokemonAtualizado);
        }

        public IEnumerable<Pokemon> Buscar()
        {
           return _pokemon.Find(pokemon => true).ToList();
        }

        public Pokemon Buscar(string id)
        {
            return _pokemon.Find(pokemon => pokemon.Id == id).FirstOrDefault();
        }

        public void Remover(string id)
        {
            _pokemon.DeleteOne(pokemon=> pokemon.Id == id);
        }
    }
}

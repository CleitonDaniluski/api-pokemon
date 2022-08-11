using DesafioAsp.Models;

namespace DesafioAsp.Data.Repositories
{
    public interface InterfacePokemon
    {
        void Adicionar(Pokemon pokemon);

        void Atualizar(string id, Pokemon pokemonAtualizado);

        IEnumerable<Pokemon> Buscar();

        Pokemon Buscar(string id);

        void Remover(string id);
    }
}

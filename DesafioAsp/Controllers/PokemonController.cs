using Microsoft.AspNetCore.Mvc;
using DesafioAsp.Data.Repositories;
using DesafioAsp.Models;
using DesafioAsp.Models.InputModels;

namespace DesafioAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private InterfacePokemon _pokemonRepository;

        // Construtor
        public PokemonController(InterfacePokemon interfacePokemon)
        {
           _pokemonRepository = interfacePokemon;
        }

        // GET: api/listapokemon
        [HttpGet]
        public IActionResult Get()
        {
            var pokemon = _pokemonRepository.Buscar();
            return Ok(pokemon);
        }

        // GET api/pokemon/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var pokemon = _pokemonRepository.Buscar(id);

            if (pokemon == null)
                return NotFound();

            return Ok(pokemon);
        }

        // POST api/pokemon
        [HttpPost]
        public IActionResult Post([FromBody] PokemonModel pokemonNovo)
        {
            var pokemon = new Pokemon(pokemonNovo.Nome, pokemonNovo.Detalhes);

            _pokemonRepository.Adicionar(pokemon);

            return Created("", pokemonNovo);
        }

        // PUT api/pokemon/{id}
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] PokemonModel pokemonAtualizado)
        {
            var pokemonExistente = _pokemonRepository.Buscar(id);

            if (pokemonExistente == null)
                return NotFound();

            pokemonExistente.AtualizarPokemon(pokemonAtualizado.Nome, pokemonAtualizado.Detalhes, pokemonAtualizado.Capturado);

            _pokemonRepository.Atualizar(id, pokemonExistente);

            return Ok(pokemonExistente);
        }

        // DELETE api/pokemon/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var pokemon = _pokemonRepository.Buscar(id);

            if (pokemon == null)
                return NotFound();

            _pokemonRepository.Remover(id);

            return NoContent();
        }
    }
}
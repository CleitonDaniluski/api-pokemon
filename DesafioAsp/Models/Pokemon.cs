using System;

namespace DesafioAsp.Models
{
    public class Pokemon
    {
        public Pokemon(string nome, string detalhes)
        {
            Id = Guid.NewGuid().ToString();
            Nome = nome;
            Detalhes = detalhes;
            DataCaptura = DateTime.Now;
            Capturado = false;

        }

        public string Id { get; private set; }

        public string Nome { get; private set; }

        public string Detalhes { get; private set; }

        public DateTime? DataCaptura { get; private set; }

        public bool Capturado { get; private set; }

        public void AtualizarPokemon(string nome, string detalhes, bool? capturado = false)
        {
            Nome = nome;
            Detalhes = detalhes;
            Capturado = capturado ?? false;
            DataCaptura = Capturado ? DateTime.Now : null;
        }
    }
}

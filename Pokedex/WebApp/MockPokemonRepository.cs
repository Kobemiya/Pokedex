using System.Collections.Generic;
using System.Threading.Tasks;
using PokedexBackend.Dbo;
using PokedexBackend.DataAccess.Repositories;

namespace WebApp.MockRepositories
{
    public class MockPokemonsRepository : IPokemonsRepository
    {
        public Task<IEnumerable<Pokemon>> GetAll(string includeTables = "")
        {
            var pokemons = new List<Pokemon>
            {
                new Pokemon { Id = 1, Name = "Pikachu", Type1 = "Eau", ImagePath = "/Screenshot_2.jpg"},
                new Pokemon { Id = 2, Name = "Salamèche", Type1 = "Feu", Type2 = "Herbe", ImagePath = "/Salameche.png"},
            };

            return Task.FromResult<IEnumerable<Pokemon>>(pokemons);
        }
        public Task<Pokemon?> GetById(long id, string includeTables = "")
        {
            throw new NotImplementedException();
        }

        public Task<Pokemon> Insert(Pokemon entity)
        {
            throw new NotImplementedException();
        }

        public Task<Pokemon> Update(Pokemon entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(long idEntity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAttack(long pokemon_id, long attack_id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAttack(long pokemon_id, long attack_id)
        {
            throw new NotImplementedException();
        }
    }
}
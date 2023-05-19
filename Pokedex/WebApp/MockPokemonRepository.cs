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
                new Pokemon { Id = 1, Name = "Pikachu", Type1 = "Electrique", ImagePath = "/Screenshot_2.jpg"},
                new Pokemon { Id = 2, Name = "Salamèche", Type1 = "Feu", Type2 = "Normal", ImagePath = "/Salameche.png"},
                new Pokemon { Id = 3, Name = "Dummy1", Type1 = "Eau", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 4, Name = "Dummy2", Type1 = "Acier", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 5, Name = "Dummy3", Type1 = "Ténèbres", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 6, Name = "Dummy4", Type1 = "Spectre", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 7, Name = "Dummy5", Type1 = "Dragon", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 8, Name = "Dummy6", Type1 = "Insecte", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 9, Name = "Dummy7", Type1 = "Glace", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 10, Name = "Dummy8", Type1 = "Roche", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 11, Name = "Dummy9", Type1 = "Sol", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 12, Name = "Dummy10", Type1 = "Poison", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 13, Name = "Dummy11", Type1 = "Vol", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 14, Name = "Dummy12", Type1 = "Psy", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 15, Name = "Dummy13", Type1 = "Electrique", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 16, Name = "Dummy14", Type1 = "Combat", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 17, Name = "Dummy15", Type1 = "Normal", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 18, Name = "Dummy16", Type1 = "Plante", ImagePath = "/Screenshot_2.jpg" }
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
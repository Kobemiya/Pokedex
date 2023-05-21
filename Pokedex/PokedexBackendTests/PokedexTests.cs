using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PokedexBackend.DataAccess.EfModels;
using PokedexBackend.DataAccess.Repositories;
using Attack = PokedexBackend.Dbo.Attack;
using Pokemon = PokedexBackend.Dbo.Pokemon;

namespace PokedexBackend.Tests.DataAccess.Repositories
{
    [TestFixture]
    public class RepositoryTests
    {
        private Mock<PokedexDotNetContext> _contextMock;
        private Mock<ILogger<CRUDRepository<PokedexBackend.DataAccess.EfModels.Pokemon, Dbo.Pokemon>>> _loggerPokemonMock;
        private Mock<ILogger<CRUDRepository<PokedexBackend.DataAccess.EfModels.Attack, Dbo.Attack>>> _loggerAttackMock;
        private Mock<ILogger<CRUDRepository<PokedexBackend.DataAccess.EfModels.User, Dbo.User>>> _loggerUserMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<PokedexDotNetContext>();
            _loggerPokemonMock = new Mock<ILogger<CRUDRepository<PokedexBackend.DataAccess.EfModels.Pokemon, Pokemon>>>();
            _loggerAttackMock = new Mock<ILogger<CRUDRepository<PokedexBackend.DataAccess.EfModels.Attack, Attack>>>();
            _loggerUserMock = new Mock<ILogger<CRUDRepository<User, Dbo.User>>>();
            _mapperMock = new Mock<IMapper>();
        }


        [Test]
        public async Task AddPokemon()
        {
            var repository = new PokemonsRepository(_contextMock.Object, _loggerPokemonMock.Object, _mapperMock.Object);
            var pokemon = new Pokemon 
            {
                 Id = 1, Name = "Pikachu", Type1 = "Electrique",
            };

            var result = await repository.Insert(pokemon);

            Assert.NotNull(result);
        }
        
        [Test]
        public async Task AddAttack()
        {
            var repository = new AttacksRepository(_contextMock.Object, _loggerAttackMock.Object, _mapperMock.Object);
            var attack = new Attack
            {
                 Id = 1, Name = "Coup d'Jus", Accuracy = 100, Damage = 65, Description = "Électrise l'adversaire", Type = "Electrique"
            };

            var result = await repository.Insert(attack);

            Assert.NotNull(result);
        }
        
        [Test]
        public async Task AddUser()
        {
            var repository = new UsersRepository(_contextMock.Object, _loggerUserMock.Object, _mapperMock.Object);
            var user = new PokedexBackend.Dbo.User 
            {
                 Id = 1, Username = "john.doe", Password = "p4ssw0rd"
            };

            var result = await repository.Insert(user);

            Assert.NotNull(result);
        }
        
        [Test]
        public async Task DeletePokemon()
        {
            var repository = new PokemonsRepository(_contextMock.Object, _loggerPokemonMock.Object, _mapperMock.Object);
            var pokemon = new Pokemon 
            {
                 Id = 1, Name = "Pikachu", Type1 = "Electrique",
            };

            await repository.Insert(pokemon);
            var result = await repository.Delete(pokemon.Id);

            Assert.True(result);
            Assert.IsEmpty(await repository.GetAll());
        }
        
        [Test]
        public async Task DeleteAttack()
        {
            var repository = new AttacksRepository(_contextMock.Object, _loggerAttackMock.Object, _mapperMock.Object);
            var attack = new Attack
            {
                 Id = 1, Name = "Coup d'Jus", Accuracy = 100, Damage = 65, Description = "Électrise l'adversaire", Type = "Electrique"
            };

            await repository.Insert(attack);
            var result = await repository.Delete(attack.Id);

            Assert.True(result);
            Assert.IsEmpty(await repository.GetAll());
        }
        
        [Test]
        public async Task DeleteUser()
        {
            var repository = new UsersRepository(_contextMock.Object, _loggerUserMock.Object, _mapperMock.Object);
            var user = new PokedexBackend.Dbo.User 
            {
                 Id = 1, Username = "john.doe", Password = "p4ssw0rd"
            };

            await repository.Insert(user);
            var result = await repository.Delete(user.Id);

            Assert.True(result);
            Assert.IsEmpty(await repository.GetAll());
        }

        [Test]
        public async Task GetPokemonById()
        {
            var repository = new PokemonsRepository(_contextMock.Object, _loggerPokemonMock.Object, _mapperMock.Object);
            var pokemon = new Pokemon 
            {
                 Id = 1, Name = "Pikachu", Type1 = "Electrique",
            };
            await repository.Insert(pokemon);

            var result = await repository.GetById(1);

            Assert.Equals(result, pokemon);
        }
        
        [Test]
        public async Task GetAttackById()
        {
            var repository = new AttacksRepository(_contextMock.Object, _loggerAttackMock.Object, _mapperMock.Object);
            var attack = new Attack
            {
                 Id = 1, Name = "Coup d'Jus", Accuracy = 100, Damage = 65, Description = "Électrise l'adversaire", Type = "Electrique"
            };
            await repository.Insert(attack);

            var result = await repository.GetById(1);

            Assert.Equals(result, attack);
        }
        
        [Test]
        public async Task GetUserById()
        {
            var repository = new UsersRepository(_contextMock.Object, _loggerUserMock.Object, _mapperMock.Object);
            var user = new PokedexBackend.Dbo.User 
            {
                 Id = 1, Username = "john.doe", Password = "p4ssw0rd"
            };
            await repository.Insert(user);

            var result = await repository.GetById(1);

            Assert.Equals(result, user);
        }

        [Test]
        public async Task GetAllPokemons()
        {
            var repository = new PokemonsRepository(_contextMock.Object, _loggerPokemonMock.Object, _mapperMock.Object);
            var pikachu = new Pokemon 
            {
                 Id = 1, Name = "Pikachu", Type1 = "Electrique",
            };
            var salameche = new Pokemon 
            {
                 Id = 2, Name = "Salamèche", Type1 = "Feu",
            };
            var carapuce = new Pokemon 
            {
                 Id = 3, Name = "Carapuce", Type1 = "Eau",
            };
            
            await repository.Insert(pikachu);
            await repository.Insert(salameche);
            await repository.Insert(carapuce);

            var result = await repository.GetAll();

            var list = new List<Pokemon>();
            list.Add(pikachu);
            list.Add(salameche);
            list.Add(carapuce);
            
            var expected = Task.FromResult<IEnumerable<Pokemon>>(list);
            Assert.Equals(result, expected);
        }
        
        [Test]
        public async Task GetAllAttacks()
        {
            var repository = new AttacksRepository(_contextMock.Object, _loggerAttackMock.Object, _mapperMock.Object);
            var attack1 = new Attack 
            {
                 Id = 1, Name = "Coup d'Jus", Accuracy = 100, Damage = 65, Description = "Électrise l'adversaire", Type = "Electrique"
            };
            var attack2 = new Attack 
            {
                 Id = 2, Name = "Tonnerre", Accuracy = 90, Damage = 80, Description = "Électrise moyennement l'adversaire", Type = "Electrique"
            };
            var attack3 = new Attack 
            {
                 Id = 3, Name = "Tonnerre", Accuracy = 800, Damage = 90, Description = "Électrise beaucoup l'adversaire", Type = "Electrique"
            };
            
            await repository.Insert(attack1);
            await repository.Insert(attack2);
            await repository.Insert(attack3);

            var result = await repository.GetAll();

            var list = new List<Attack>();
            list.Add(attack1);
            list.Add(attack2);
            list.Add(attack3);
            
            var expected = Task.FromResult<IEnumerable<Attack>>(list);
            Assert.Equals(result, expected);
        }
        
        [Test]
        public async Task GetAllUser()
        {
            var repository = new UsersRepository(_contextMock.Object, _loggerUserMock.Object, _mapperMock.Object);
            var user1 = new PokedexBackend.Dbo.User
            {
                 Id = 1, Username = "john.doe", Password = "p4ssw0rd"
            };
            var user2 = new PokedexBackend.Dbo.User
            {
                 Id = 2, Username = "jane.doe", Password = "p4ss"
            };
            var user3 = new PokedexBackend.Dbo.User
            {
                 Id = 3, Username = "james.doe", Password = "w0rd"
            };
            
            await repository.Insert(user1);
            await repository.Insert(user2);
            await repository.Insert(user3);

            var result = await repository.GetAll();

            var list = new List<PokedexBackend.Dbo.User>();
            list.Add(user1);
            list.Add(user2);
            list.Add(user3);
            
            var expected = Task.FromResult<IEnumerable<PokedexBackend.Dbo.User>>(list);
            Assert.Equals(result, expected);
        }
        
        
        [Test]
        public async Task GetPokemonType()
        {
            var pokemon = new Pokemon 
            {
                 Id = 1, Name = "Pikachu", Type1 = "Electrique",
            };

            var result = pokemon.GetType();

            Assert.Equals(result, "Electrique");
        }
        
        [Test]
        public async Task GetAttackType()
        {
            var attack = new Attack 
            {
                 Id = 1, Name = "Coup d'Jus", Accuracy = 100, Damage = 65, Description = "Électrise l'adversaire", Type = "Electrique"
            };

            var result = attack.GetType();

            Assert.Equals(result, "Electrique");
        }
    }
}

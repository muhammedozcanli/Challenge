using Challenge.Persistance.Base.Entities;
using Challenge.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Xunit;
using FluentAssertions;

namespace Challenge.Persistence.Test.RepositoryTests
{
    public class TestEntity : BaseEntity
    {
        public string Name { get; set; }
    }

    public class BaseRepositoryTests : IDisposable
    {
        protected readonly ChallengeDBContext _context;
        protected readonly BaseRepository<TestEntity> _repository;
        protected readonly DbContextOptions<ChallengeDBContext> _options;

        public BaseRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ChallengeDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ChallengeDBContext(_options);
            _repository = new BaseRepository<TestEntity>(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void Add_ShouldAddEntityToDatabase()
        {
            // Arrange
            var entity = new TestEntity { Name = "Test Entity" };

            // Act
            _repository.Add(entity);
            _repository.SaveChanges();

            // Assert
            var savedEntity = _context.Set<TestEntity>().FirstOrDefault();
            savedEntity.Should().NotBeNull();
            savedEntity.Name.Should().Be("Test Entity");
        }

        [Fact]
        public void Get_ShouldReturnCorrectEntity()
        {
            // Arrange
            var entity = new TestEntity { Name = "Test Entity" };
            _context.Set<TestEntity>().Add(entity);
            _context.SaveChanges();

            // Act
            var result = _repository.Get(e => e.Name == "Test Entity");

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Test Entity");
        }

        [Fact]
        public void Update_ShouldModifyExistingEntity()
        {
            // Arrange
            var entity = new TestEntity { Name = "Original Name" };
            _context.Set<TestEntity>().Add(entity);
            _context.SaveChanges();

            // Act
            entity.Name = "Updated Name";
            _repository.Update(entity);
            _repository.SaveChanges();

            // Assert
            var updatedEntity = _context.Set<TestEntity>().FirstOrDefault();
            updatedEntity.Should().NotBeNull();
            updatedEntity.Name.Should().Be("Updated Name");
        }

        [Fact]
        public void Delete_ShouldRemoveEntityFromDatabase()
        {
            // Arrange
            var entity = new TestEntity { Name = "Test Entity" };
            _context.Set<TestEntity>().Add(entity);
            _context.SaveChanges();

            // Act
            _repository.Delete(entity);
            _repository.SaveChanges();

            // Assert
            var deletedEntity = _context.Set<TestEntity>().FirstOrDefault();
            deletedEntity.Should().BeNull();
        }

        [Fact]
        public void GetList_ShouldReturnFilteredEntities()
        {
            // Arrange
            var entities = new[]
            {
                new TestEntity { Name = "Entity 1" },
                new TestEntity { Name = "Entity 2" },
                new TestEntity { Name = "Different" }
            };
            _context.Set<TestEntity>().AddRange(entities);
            _context.SaveChanges();

            // Act
            var results = _repository.GetList(e => e.Name.StartsWith("Entity"));

            // Assert
            results.Should().HaveCount(2);
            results.Should().AllSatisfy(e => e.Name.Should().StartWith("Entity"));
        }

        [Fact]
        public async Task GetListAsync_ShouldReturnAllEntitiesWhenFilterIsNull()
        {
            // Arrange
            var entities = new[]
            {
                new TestEntity { Name = "Entity 1" },
                new TestEntity { Name = "Entity 2" },
                new TestEntity { Name = "Entity 3" }
            };
            await _context.Set<TestEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            // Act
            var results = await _repository.GetListAsync(null);

            // Assert
            results.Should().HaveCount(3);
        }

        [Fact]
        public void AddRange_ShouldAddMultipleEntities()
        {
            // Arrange
            var entities = new List<TestEntity>
            {
                new TestEntity { Name = "Entity 1" },
                new TestEntity { Name = "Entity 2" }
            };

            // Act
            _repository.AddRange(entities);
            _repository.SaveChanges();

            // Assert
            var savedEntities = _context.Set<TestEntity>().ToList();
            savedEntities.Should().HaveCount(2);
            savedEntities.Should().ContainSingle(e => e.Name == "Entity 1");
            savedEntities.Should().ContainSingle(e => e.Name == "Entity 2");
        }

        [Fact]
        public void UpdateRange_ShouldModifyMultipleEntities()
        {
            // Arrange
            var entities = new List<TestEntity>
            {
                new TestEntity { Name = "Original 1" },
                new TestEntity { Name = "Original 2" }
            };
            _context.Set<TestEntity>().AddRange(entities);
            _context.SaveChanges();

            // Act
            entities.ForEach(e => e.Name = e.Name.Replace("Original", "Updated"));
            _repository.UpdateRange(entities);
            _repository.SaveChanges();

            // Assert
            var updatedEntities = _context.Set<TestEntity>().ToList();
            updatedEntities.Should().HaveCount(2);
            updatedEntities.Should().AllSatisfy(e => e.Name.Should().StartWith("Updated"));
        }
    }
} 
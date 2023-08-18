using Domain;
using Infrastructure;
using Infrastructure.Repositories.Interfaces;
using Shouldly;

namespace Tests.infrastructure
{
    [TestFixture]
    public class RoleRepositoryTest : TestBase
    {
        private IRoleRepository _repository;
        private ApplicationDbContext _context;

        [OneTimeSetUp]
        public void SetUp()
        {
            _context = GetContext();

            _repository = GetService<IRoleRepository>();
        }

        [Test]
        public async Task GetRolesTest()
        {
            //Arrange
            _context.Add(new Role { Name = "User" });
            _context.SaveChanges();

            //Act
            var roles = _repository.GetRoles().ToList();

            //Assert
            _repository.ShouldNotBeNull();
            roles.ShouldNotBeEmpty();
        }


    }
}

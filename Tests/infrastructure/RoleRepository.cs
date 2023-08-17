using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repositories.Interfaces;
using Domain;
using SpecsFor.Core;
using RoleRepositorySUT = Infrastructure.Repositories.Implementations.RoleRepository;
using SpecsFor.StructureMap;
using Shouldly;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Infrastructure;

namespace Tests.infrastructure
{
    public class RoleRepositoryTest
    {
        #region TESTS
        public class When_getting_roles : SpecsFor<RoleRepositorySUT>
        {
            private List<Role> _result;

            protected override void Given()
            {
                Given<RoleRepositoryReturnsRoles>();
               
            }

            protected override  void When()
            {
                _result = SUT.GetRoles().ToList();
            }


            [Test, Category(nameof(RoleRepositoryTest))]
            public void Then_it_should_call_roles()
            {
                GetMockFor<IRoleRepository>()
                    .Verify(x => x.GetRoles(), Times.Once);
            }

            [Test, Category(nameof(RoleRepositoryTest))]
            public void Then_it_should_return_valid_data()
            {
                _result.ShouldNotBeNull();
            }
        }


        #endregion
        #region GIVEN
        public class RoleRepositoryReturnsRoles : IContext<RoleRepositorySUT>
        {
            private IQueryable<Role> _result;
            public void Initialize(ISpecs<RoleRepositorySUT> state)
            {
                var role = new Role();
                var context = new Mock<ApplicationDbContext>();
                var dbSetMock = new Mock<DbSet<Role>>();
                context.Setup(x => x.Set<Role>()).Returns(dbSetMock.Object);
               // dbSetMock.Setup(x => x.Add(It.IsAny<Role>())).Returns(role);

                //var peopleContextMock = new Mock<ApplicationDbContext>();
                //peopleContextMock.Setup(pc => pc.Set<Role>()).Returns(dbSetMock.Object);
                //var entityRepository = new RoleRepository<Role>(peopleContextMock.Object);

                //var jokesRepository = new Mock<IRoleRepository<Role>>();

                state.GetMockFor<IRoleRepository>()
                    .Setup(i => i.GetRoles())
                    .Returns(_result);
            }
        }

        #endregion
    }
}

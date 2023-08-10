using Domain;
using Infrastructure.Repositories.Implementations;
using Infrastructure.Repositories.Interfaces;

namespace graphql_skeleton.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class RolesQueries
    {
        private readonly IRoleRepository _repository;

        public RolesQueries(IRoleRepository repository)
        {
            _repository = repository;
        }

        [GraphQLDescription("Returns the specified Roles")]
        public IQueryable<Role> GetList()
          =>   _repository.GetRoles();
    }
}

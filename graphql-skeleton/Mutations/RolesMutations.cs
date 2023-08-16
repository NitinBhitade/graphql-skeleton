using Application.Authorization;
using Domain;
using Domain.Common;
using Infrastructure.Repositories.Interfaces;

namespace graphql_skeleton.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class RolesMutations
    {
        private readonly IRoleRepository _repository;

        public RolesMutations(IRoleRepository repository)
        {
            _repository = repository;
        }

        [GraphQLDescription("add the specified Roles")]
        public async Task<AddRolePayload> AddRoleAsync(AddRoleInput input, CancellationToken cancellationToken)
        {
            if (input.Name.HasValue && input.Name.Value is null)
                return new AddRolePayload(
                    new UserError("Name cannot be null", "NAME_NULL"));

            var role = new Role() { Name = input.Name };
            await _repository.Add(role, cancellationToken);

            return new AddRolePayload(role);
        }
    }
}

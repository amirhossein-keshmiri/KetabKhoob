using Common.Application;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Repository;

namespace Shop.Application.Roles.Create;

internal class CreateRoleCommandHandler : IBaseCommandHandler<CreateRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var permissions = new List<RolePermission>();
        request.Permissions.ForEach(f =>
        {
            permissions.Add(new RolePermission(f));
        });
        var role = new Role(request.Title, permissions);
        _roleRepository.Add(role);
        await _roleRepository.Save();

        return OperationResult.Success();
    }
}


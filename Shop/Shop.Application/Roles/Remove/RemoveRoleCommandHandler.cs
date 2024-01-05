using Common.Application;
using Shop.Domain.CategoryAgg;
using Shop.Domain.RoleAgg.Repository;

namespace Shop.Application.Roles.Remove;
internal class RemoveRoleCommandHandler : IBaseCommandHandler<RemoveRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public RemoveRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    public async Task<OperationResult> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _roleRepository.DeleteRole(request.RoleId);
        if (result)
        {
            await _roleRepository.Save();
            return OperationResult.Success();
        }

        return OperationResult.Error("امکان حذف این نقش وجود ندارد");
    }
}

using Common.Application;
using Shop.Domain.RoleAgg.Repository;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.SetRole;
internal class SetUserRoleCommandHandler : IBaseCommandHandler<SetUserRoleCommand>
{
    private readonly IUserRepository _userRepository;

    public SetUserRoleCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(SetUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        var userRoles = new List<UserRole>();
        request.Roles.ForEach(role =>
        {
            userRoles.Add(new UserRole(role));
        });

        user.SetUserRole(userRoles);
        await _userRepository.Save();
        return OperationResult.Success();

    }
}


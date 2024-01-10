using Common.Application;

namespace Shop.Application.Users.SetRole;
public record SetUserRoleCommand(long UserId, List<long> Roles) : IBaseCommand;



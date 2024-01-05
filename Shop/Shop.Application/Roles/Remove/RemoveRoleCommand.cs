using Common.Application;

namespace Shop.Application.Roles.Remove;
public record RemoveRoleCommand(long RoleId) : IBaseCommand;



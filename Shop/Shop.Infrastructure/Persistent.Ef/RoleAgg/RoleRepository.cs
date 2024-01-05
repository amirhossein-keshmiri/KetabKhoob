using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Repository;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.Dapper;

namespace Shop.Infrastructure.Persistent.Ef.RoleAgg;
internal class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    private readonly DapperContext _dapperContext;
    public RoleRepository(ShopContext context, DapperContext dapperContext) : base(context)
    {
        _dapperContext = dapperContext;
    }

    public async Task<bool> DeleteRole(long roleId)
    {
        var role = await Context.Roles.
            Include(r=>r.Permissions).FirstOrDefaultAsync(r=>r.Id == roleId);

        if (role == null)
            return false;

        //Check If Any Users Have This Role 
        var sql = $"Select * from {_dapperContext.UserRoles} where RoleId=@id";
        using var context = _dapperContext.CreateConnection();
        var result = await context.QueryFirstOrDefaultAsync(sql, new { id = roleId });

        if(result != null)
            return false;

        if (role.Permissions.Any())
        {
            Context.RemoveRange(role.Permissions);
        }

        Context.RemoveRange(role);

        return true;

    }
}


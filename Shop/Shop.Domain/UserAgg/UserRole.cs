using Common.Domain;

namespace Shop.Domain.UserAgg
{
    public class UserRole : BaseEntity
    {
        public UserRole(int roleId)
        {
            RoleId = roleId;
        }

        public long UserId { get; internal set; }
        public int RoleId { get; private set; }
    }
}

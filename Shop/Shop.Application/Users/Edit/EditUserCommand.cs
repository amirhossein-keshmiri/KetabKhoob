using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.UserAgg.Enums;

namespace Shop.Application.Users.Edit;
public class EditUserCommand : IBaseCommand
{
    //public EditUserCommand(long userId, IFormFile? avatar, string name,
    //    string family, string phoneNumber, string email, Gender gender)
    //{
    //    UserId = userId;
    //    Avatar = avatar;
    //    Name = name;
    //    Family = family;
    //    PhoneNumber = phoneNumber;
    //    Email = email;
    //    Gender = gender;
    //}

    public long UserId { get; set; }
    public IFormFile? Avatar { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }
}

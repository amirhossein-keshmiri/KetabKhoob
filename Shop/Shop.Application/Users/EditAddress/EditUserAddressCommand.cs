using Common.Application;
using Common.Domain.ValueObjects;

namespace Shop.Application.Users.EditAddress;
public class EditUserAddressCommand : IBaseCommand
{
    public EditUserAddressCommand(long userId, long id, string state, string city, 
        string postalCode, string postalAddress, PhoneNumber phoneNumber, 
        string name, string family, string nationalCode)
    {
        UserId = userId;
        Id = id;
        State = state;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Name = name;
        Family = family;
        NationalCode = nationalCode;
    }

    public long UserId { get; set; }
    public long Id { get; private set; }
    public string State { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }
    public string PostalAddress { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string NationalCode { get; private set; }
}


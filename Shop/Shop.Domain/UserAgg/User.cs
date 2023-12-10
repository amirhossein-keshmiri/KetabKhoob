using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Services;
using System.Net;

namespace Shop.Domain.UserAgg
{
    public class User : AggregateRoot
    {
        public User(string name, string family, string phoneNumber, string email
            , string password, Gender gender, IUserDomainService domainUserService)
        {
            Guard(phoneNumber, email, domainUserService);
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
            AvatarName = "avatar.png";
            Gender = gender;
        }

        public string Name { get; private set; }
        public string Family { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string AvatarName { get; set; }
        public Gender Gender { get; private set; }
        public List<UserRole> Roles { get; private set; }
        public List<Wallet> Wallets { get; private set; }
        public List<UserAddress> Addresses { get; private set; }

        public void Edit(string name, string family, string phoneNumber, string email
            , Gender gender, IUserDomainService domainUserService)
        {
            Guard(phoneNumber, email,domainUserService);
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Email = email;
            Gender = gender;
        }

        public static User RegisterUser(string phoneNumber,string email,string password,
            IUserDomainService domainUserService)
        {
            return new User("","",phoneNumber,email,password,Gender.None,domainUserService);
        }
        public void SetAvatar(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
                imageName = "avatar.png";

            AvatarName = imageName;
        }
        public void AddAddress(UserAddress address)
        {
            address.UserId = Id;
            Addresses.Add(address);
        }

        public void EditAddress(UserAddress address)
        {
            var oldAddress = Addresses.FirstOrDefault(f=>f.Id ==  address.Id) 
                        ?? throw new NullOrEmptyDomainDataException("Address Not Found");

            //if (oldAddress == null)
            //    throw new NullOrEmptyDomainDataException("Address Not Found");

            Addresses.Remove(oldAddress);
            Addresses.Add(address);
        }

        public void DeleteAddress(long addressId)
        {
            var address = Addresses.FirstOrDefault(f => f.Id == addressId)
                       ?? throw new NullOrEmptyDomainDataException("Address Not Found");

            Addresses.Remove(address);
        }

        public void ChargeWallet(Wallet wallet)
        {
            wallet.UserId = Id;
            Wallets.Add(wallet);
        }

        public void SetRoles(List<UserRole> roles)
        {
            roles.ForEach(f=>f.UserId =  Id);
            Roles.Clear();
            Roles.AddRange(roles);
        }
        public void Guard(string phoneNumber, string email,IUserDomainService domainUserService)
        {
            NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
            NullOrEmptyDomainDataException.CheckString(email, nameof(email));

            if (phoneNumber.Length != 11)
                throw new InvalidDomainDataException("شماره موبایل نامعتبر است.");

            if(email.IsValidEmail() == false)
                throw new InvalidDomainDataException("ایمیل نامعتبر است.");

            if(email != Email)
                if(domainUserService.IsEmailExist(email))
                    throw new InvalidDomainDataException("ایمیل تکراری است.");
        }
    }
}

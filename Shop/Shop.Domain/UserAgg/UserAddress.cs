using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Shop.Domain.UserAgg
{
    public class UserAddress : BaseEntity
    {
        public UserAddress(string state, string city, string postalCode,
            string postalAddress, PhoneNumber phoneNumber, string name, string family, string nationalCode)
        {
            Guard(state, city, postalCode, postalAddress, phoneNumber, name, family, nationalCode);

            State = state;
            City = city;
            PostalCode = postalCode;
            PostalAddress = postalAddress;
            PhoneNumber = phoneNumber;
            Name = name;
            Family = family;
            NationalCode = nationalCode;
            ActiveAddress = false;
        }

        public long UserId { get; internal set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public string PostalAddress { get; private set; }
        public PhoneNumber PhoneNumber { get; set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string NationalCode { get; private set; }
        public bool ActiveAddress { get; set; }

        public void Edit(string state, string city, string postalCode,
           string postalAddress, PhoneNumber phoneNumber, string name, string family, string nationalCode)
        {
            Guard(state, city, postalCode, postalAddress,phoneNumber, name, family, nationalCode);

            State = state;
            City = city;
            PostalCode = postalCode;
            PostalAddress = postalAddress;
            PhoneNumber = phoneNumber;
            Name = name;
            Family = family;
            NationalCode = nationalCode;
        }

        public void SetActive()
        {
            ActiveAddress = true;
        }

        public void Guard(string state, string city, string postalCode,
           string postalAddress, PhoneNumber phoneNumber, string name, string family, string nationalCode)
        {
            if (phoneNumber == null)
                throw new NullOrEmptyDomainDataException();

            NullOrEmptyDomainDataException.CheckString(state, nameof(state));
            NullOrEmptyDomainDataException.CheckString(city, nameof(city));
            NullOrEmptyDomainDataException.CheckString(postalCode, nameof(postalCode));
            NullOrEmptyDomainDataException.CheckString(postalAddress, nameof(postalAddress));
            NullOrEmptyDomainDataException.CheckString(name, nameof(name));
            NullOrEmptyDomainDataException.CheckString(family, nameof(family));
            NullOrEmptyDomainDataException.CheckString(nationalCode, nameof(nationalCode));

            if(IranianNationalIdChecker.IsValid(nationalCode)==false)
                throw new InvalidDomainDataException("کدملی نامعتبر است.");

        }

    }
}

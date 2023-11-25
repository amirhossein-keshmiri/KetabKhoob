using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.SellerAgg
{
    public class SellerInventory : BaseEntity
    {
        public long SellerId { get; internal set; }
        public long Productid { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }

        public SellerInventory(long productid, int count, int price)
        {
            if (price < 1 || count < 0)
                throw new InvalidDomainDataException();
            Productid = productid;
            Count = count;
            Price = price;
        }
    }
}

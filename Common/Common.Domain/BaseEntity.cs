namespace Common.Domain
{
    public class BaseEntity
    {
        public long Id { get; private set; }
        public DateTime CreationDate { get; set; }
        public BaseEntity()
        {
            CreationDate = new DateTime();
        }

    }
}

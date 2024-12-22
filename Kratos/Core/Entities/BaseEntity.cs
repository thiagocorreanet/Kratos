namespace Core.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity(int id)
        {
            Id = id;
            CreatedAt = DateTime.Now;
            AlteredAt = DateTime.Now;
        }

        protected BaseEntity()
        {
            CreatedAt = DateTime.Now;
            AlteredAt = DateTime.Now;
        }

        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime AlteredAt { get; private set; }
    }
}

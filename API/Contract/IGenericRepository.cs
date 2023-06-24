namespace API.Contracts
{
    public interface IGenericRepository<Entity>
    {
        Entity? Create(Entity entity);
        bool Update (Entity entity);
        bool Delete (Guid guid);   
        IEnumerable<Entity> GetAll ();
        Entity? GetByGuid(Guid guid);
    }
}

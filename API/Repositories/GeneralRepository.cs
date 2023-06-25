using API.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using System.Xml.Linq;

namespace API.Repositories
{
    public class GeneralRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        protected readonly ProjectManagementDBContext _context;

        public GeneralRepository(ProjectManagementDBContext context)
        {
            _context = context;
        }
        public Entity? Create(Entity entity)
        {
            try
            {
                typeof(Entity).GetProperty("CreatedDate")!.SetValue(entity, DateTime.Now);
                typeof(Entity).GetProperty("ModifiedDate")!.SetValue(entity, DateTime.Now);

                _context.Set<Entity>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var entity = GetByGuid(guid);
                if (entity == null)
                {
                    return false;
                }

                _context.Set<Entity>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Entity> GetAll()
        {
            return _context.Set<Entity>().ToList();
        }

        public Entity? GetByGuid(Guid guid)
        {
            var entity = _context.Set<Entity>().Find(guid);
            _context.ChangeTracker.Clear();
            return entity;
        }


        public bool Update(Entity entity)
        {
            try
            {
                var guid = (Guid)typeof(Entity).GetProperty("Guid")!.GetValue(entity)!;

                var oldEntity = GetByGuid(guid);
                if (oldEntity == null)
                {
                    return false;
                }

                var getCreatedDate = typeof(Entity).GetProperty("CreatedDate")!.GetValue(oldEntity)!;

                typeof(Entity).GetProperty("CreatedDate")!.SetValue(entity, getCreatedDate);
                typeof(Entity).GetProperty("ModifiedDate")!.SetValue(entity, DateTime.Now);
                _context.Set<Entity>().Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SistemaGestionAlquilerVehiculos.Entities;
using System.Linq.Expressions;

namespace SistemaGestionAlquilerVehiculos.Repositories
{
    public interface IAlquilerRepository
    {
        IQueryable<Alquiler> GetAll(params Expression<Func<Alquiler, object>>[] includeProperties);
        IQueryable<Alquiler> FindBy(Expression<Func<Alquiler, bool>> predicate, params Expression<Func<Alquiler, object>>[] includeProperties);
        Task<Alquiler> Add(Alquiler entity);
        void Update(Alquiler entity);
        void Remove(Alquiler entity);
        Task SaveAsync();
    }

    public class AlquilerRepository : IAlquilerRepository
    {
        protected readonly DatabaseContext _context;
        protected readonly DbSet<Alquiler> _set;

        public AlquilerRepository(DatabaseContext context)
        {
            _context = context;
            _set = context.Set<Alquiler>();
        }

        public virtual IQueryable<Alquiler> GetAll(params Expression<Func<Alquiler, object>>[] includeProperties)
        {
            IQueryable<Alquiler> query = _set.AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsNoTracking();
        }

        public virtual IQueryable<Alquiler> FindBy(Expression<Func<Alquiler, bool>> predicate, params Expression<Func<Alquiler, object>>[] includeProperties)
        {
            IQueryable<Alquiler> query = _set.Where(predicate);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsNoTracking();
        }

        public virtual async Task<Alquiler> Add(Alquiler entity)
        {
            return (await _set.AddAsync(entity)).Entity;
        }

        public virtual void Update(Alquiler entity)
        {
            _set.Update(entity);
        }

        public virtual void Remove(Alquiler entity)
        {
            entity.Borrado = true;
            Update(entity);
        }

        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

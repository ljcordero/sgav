using Microsoft.EntityFrameworkCore;
using SistemaGestionAlquilerVehiculos.Entities;
using System.Linq.Expressions;

namespace SistemaGestionAlquilerVehiculos.Repositories
{
    public interface IVehiculoRepository
    {
        IQueryable<Vehiculo> GetAll(params Expression<Func<Vehiculo, object>>[] includeProperties);
        IQueryable<Vehiculo> FindBy(Expression<Func<Vehiculo, bool>> predicate, params Expression<Func<Vehiculo, object>>[] includeProperties);
        Task<Vehiculo> Add(Vehiculo entity);
        void Update(Vehiculo entity);
        void Remove(Vehiculo entity);
        Task SaveAsync();
    }

    public class VehiculoRepository : IVehiculoRepository
    {
        protected readonly DatabaseContext _context;
        protected readonly DbSet<Vehiculo> _set;

        public VehiculoRepository(DatabaseContext context)
        {
            _context = context;
            _set = context.Set<Vehiculo>();
        }

        public virtual IQueryable<Vehiculo> GetAll(params Expression<Func<Vehiculo, object>>[] includeProperties)
        {
            IQueryable<Vehiculo> query = _set.AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsNoTracking();
        }

        public virtual IQueryable<Vehiculo> FindBy(Expression<Func<Vehiculo, bool>> predicate, params Expression<Func<Vehiculo, object>>[] includeProperties)
        {
            IQueryable<Vehiculo> query = _set.Where(predicate);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsNoTracking();
        }

        public virtual async Task<Vehiculo> Add(Vehiculo entity)
        {
            return (await _set.AddAsync(entity)).Entity;
        }

        public virtual void Update(Vehiculo entity)
        {
            _set.Update(entity);
        }

        public virtual void Remove(Vehiculo entity)
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

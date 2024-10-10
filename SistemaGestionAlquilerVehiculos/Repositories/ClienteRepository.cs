using Microsoft.EntityFrameworkCore;
using SistemaGestionAlquilerVehiculos.Entities;
using System.Linq.Expressions;

namespace SistemaGestionAlquilerVehiculos.Repositories
{
    public interface IClienteRepository
    {
        IQueryable<Cliente> GetAll(params Expression<Func<Cliente, object>>[] includeProperties);
        IQueryable<Cliente> FindBy(Expression<Func<Cliente, bool>> predicate, params Expression<Func<Cliente, object>>[] includeProperties);
        Task<Cliente> Add(Cliente entity);
        void Update(Cliente entity);
        void Remove(Cliente entity);
        Task SaveAsync();
    }

    public class ClienteRepository : IClienteRepository
    {
        protected readonly DatabaseContext _context;
        protected readonly DbSet<Cliente> _set;

        public ClienteRepository(DatabaseContext context)
        {
            _context = context;
            _set = context.Set<Cliente>();
        }

        public virtual IQueryable<Cliente> GetAll(params Expression<Func<Cliente, object>>[] includeProperties)
        {
            IQueryable<Cliente> query = _set.AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsNoTracking();
        }

        public virtual IQueryable<Cliente> FindBy(Expression<Func<Cliente, bool>> predicate, params Expression<Func<Cliente, object>>[] includeProperties)
        {
            IQueryable<Cliente> query = _set.Where(predicate);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsNoTracking();
        }

        public virtual async Task<Cliente> Add(Cliente entity)
        {
            return (await _set.AddAsync(entity)).Entity;
        }

        public virtual void Update(Cliente entity)
        {
            _set.Update(entity);
        }

        public virtual void Remove(Cliente entity)
        {
            entity.Borrado = true;
            entity.Cedula = $"{entity.Cedula} - Borrado - {entity.Id}";
            Update(entity);
        }

        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

using Discoteque.Data.IRepositories;
using Discoteque.Data.Models;
using Discoteque.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Discoteque.Data;

public class UnitOfWork : IUnitOfWork, IDisposable {
    private readonly DiscotequeContext _context;
    private bool _disposed = false;
    private IRepository<int, Artist> _artistRespository;

    public UnitOfWork(DiscotequeContext context) {
        _context = context;
    }

    public IRepository<int, Artist> ArtistRepository {
        get {
            // The null-coalescing assignment operator ??= assigns the value of its right-hand operand to 
            // its left-hand operand only if the left-hand operand evaluates to null. The ??= operator doesn't 
            // evaluate its right-hand operand if the left-hand operand evaluates to non-null.
            _artistRespository ??= new Repository<int, Artist>(_context);
            return _artistRespository;
        }
    }

    public async Task SaveAsync()
    {
        try {
            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException ex) {
            // if can't save, it just reloads the last version of it
            ex.Entries.Single().Reload();  
        }
    }
#region IDisposable
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed) {
            if (disposing) {
                _context.DisposeAsync();
            }
        }
        _disposed = true;
    }

    public void Dispose() {
        Dispose(true);
    }

#endregion

}
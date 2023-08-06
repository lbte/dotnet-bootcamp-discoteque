using Discoteque.Data.IRepositories;
using Discoteque.Data.Models;
using Discoteque.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Discoteque.Data;

public class UnitOfWork : IUnitOfWork, IDisposable {
    private readonly DiscotequeContext _context;
    private bool _disposed = false;
    private IRepository<int, Artist> _artistRepository;
    private IRepository<int, Album> _albumRepository;
    private IRepository<int, Song> _songRepository;
    private IRepository<int, Tour> _tourRepository;

    public UnitOfWork(DiscotequeContext context) {
        _context = context;
    }

    public IRepository<int, Artist> ArtistRepository {
        get {
            // The null-coalescing assignment operator ??= assigns the value of its right-hand operand to 
            // its left-hand operand only if the left-hand operand evaluates to null. The ??= operator doesn't 
            // evaluate its right-hand operand if the left-hand operand evaluates to non-null.
            _artistRepository ??= new Repository<int, Artist>(_context);
            return _artistRepository;
        }
    }
    
    public IRepository<int, Album> AlbumRepository {
        get {
            _albumRepository ??= new Repository<int, Album>(_context);
            return _albumRepository;
        }
    }
    
    public IRepository<int, Song> SongRepository {
        get {
            _songRepository ??= new Repository<int, Song>(_context);
            return _songRepository;
        }
    }
    
    public IRepository<int, Tour> TourRepository {
        get {
            _tourRepository ??= new Repository<int, Tour>(_context);
            return _tourRepository;
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
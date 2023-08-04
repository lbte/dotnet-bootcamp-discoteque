using Discoteque.Data.IRepositories;
using Discoteque.Data.Models;

namespace Discoteque.Data;

public interface IUnitOfWork {
    IRepository<int, Artist> ArtistRepository{ get;}

    Task SaveAsync();
}
using Discoteque.Data.Dto;
using Discoteque.Data.Models;

namespace Discoteque.Business.IServices;

public interface IAlbumService {

    /// <summary>
    /// Find all albums
    /// </summary>
    /// <param name="areReferencesLoaded">Returns associated artists per album if true</param>
    /// <returns>A <see cref="List"/> of <see cref="Album"/></returns>
    Task <IEnumerable<Album>> GetAlbumsAsync(bool areReferencesLoaded);
    
    /// <summary>
    /// Finds all albums published in a year
    /// </summary>
    /// <param name="year"> A gregorian year between 1900 and the current year</param>
    /// <returns>A <see cref="List"/> of <see cref="Album"/></returns>
    Task <IEnumerable<Album>> GetAlbumsByYear(int year);
    
    /// <summary>
    /// Finds all albums released from initialYear to maxYear
    /// </summary>
    /// <param name="initialYear">The initial year. Minimum value is 1900</param>
    /// <param name="maxYear">The maximun year. Maximum value is 2023</param>
    /// <returns>A <see cref="List"/> of <see cref="Album"/></returns>
    Task <IEnumerable<Album>> GetAlbumsByYearRange(int initialYear, int maxYear);
    
    /// <summary>
    /// Finds all albums with the assigned genre
    /// </summary>
    /// <param name="genre"></param>
    /// <returns>A <see cref="List"/> of <see cref="Album"/></returns>
    Task <IEnumerable<Album>> GetAlbumsByGenre(Genres genre);
    
    /// <summary>
    /// Finds all albums released by <see cref="Artist.Name"/>
    /// </summary>
    /// <param name="artist">The name of the artist</param>
    /// <returns>A <see cref="List"/> of <see cref="Album"/></returns>
    Task <IEnumerable<Album>> GetAlbumsByArtist(string artist);

    /// <summary>
    /// Finds an album by its id in the DB
    /// </summary>
    /// <param name="id">The unique id of the album</param>
    /// <returns>An <see cref="Album"/></returns>
    Task <Album> GetById(int id);

    /// <summary>
    /// Creates a new <see cref="Album"/> entity in the DB
    /// </summary>
    /// <param name="album">A new album entity</param>
    /// <returns>The created album with an assigned Id</returns>
    Task <EntityMessage<Album>> CreateAlbum(Album album);

    /// <summary>
    /// Updates the <see cref="Album"/> entity in the DB
    /// </summary>
    /// <param name="album">The album entity to update</param>
    /// <returns>The new album with updated fields when successful</returns>
    Task <Album> UpdateAlbum(Album album);
}

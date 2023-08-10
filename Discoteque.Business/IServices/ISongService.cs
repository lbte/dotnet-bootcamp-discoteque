using Discoteque.Data.Dto;
using Discoteque.Data.Models;
namespace Discoteque.Business.IServices;

public interface ISongService
{
    /// <summary>
    /// Find all songs in the DB
    /// </summary>
    /// <param name="areReferencesLoaded">Returns associated albums per song if true</param>
    /// <returns>A <see cref="List"/> of <see cref="Song"/></returns>
    Task<IEnumerable<Song>> GetSongsAsync(bool areReferencesLoaded);
    
    /// <summary>
    /// A list of songs contained in <see cref="Album"/>
    /// </summary>
    /// <param name="albumId">The id of the album</param>
    /// <returns>A <see cref="List" /> of <see cref="Song"/> </returns>
    Task<IEnumerable<Song>> GetSongsByAlbum(int albumId);
    
    /// <summary>
    /// Get a song by its DB Identity
    /// </summary>
    /// <param name="id">The unique id of the element</param>
    /// <returns>A <see cref="Song"/> </returns>
    /// 
    Task<Song> GetById(int id);
    
   /// <summary>
   /// Get a song by its <see cref="Album"/> released year
   /// </summary>
   /// <param name="year">The year the Album was released</param>
   /// <returns>A <see cref="List" /> of <see cref="Song"/> </returns>
    Task<IEnumerable<Song>> GetSongsByYear(int year);
    
    
    /// <summary>
    /// Creates a new <see cref="Song"/> entity in the DB. 
    /// </summary>
    /// <param name="song">A new song entity</param>
    /// <returns>The created song with an Id assigned</returns>
    Task<EntityMessage<Song>> CreateSong(Song song);
    
    /// <summary>
    /// Creates a new set of <see cref="Song"/> entities in the DB.
    /// </summary>
    /// <param name="songs">A <see cref="List"/> of entities from Song</param>
    /// <returns></returns>
    Task<EntityMessage<Song>> CreateSongsInBatch(List<Song> songs);
    
    /// <summary>
    /// Updates the <see cref="Song"/> entity in the DB
    /// </summary>
    /// <param name="song">The Song entity to update</param>
    /// <returns>The new song with updated fields if successful</returns>
    Task<Song> UpdateSong(Song song);
}
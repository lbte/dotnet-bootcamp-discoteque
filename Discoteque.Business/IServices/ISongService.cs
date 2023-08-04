using Discoteque.Data.Models;
namespace Discoteque.Data.Services;

public interface ISongService
{
    /// <summary>
    /// Finds all songs in the EF DB
    /// </summary>
    /// <param name="areReferencesLoaded">Returns associated albums per song if true</param>
    /// <returns>A <see cref="List" /> of <see cref="Song"/> </returns>
    Task<IEnumerable<Song>> GetSongsAsync(bool areReferencesLoaded);
    
    
    /// <summary>
    /// A list of songs released by a <see cref="Album.Name"/>
    /// </summary>
    /// <param name="album">The name of the album</param>
    /// <returns>A <see cref="List" /> of <see cref="Song"/> </returns>
    Task<IEnumerable<Song>> GetSongsByAlbum(string album);
    
    /// <summary>
    /// Get an song by its EF DB Identity
    /// </summary>
    /// <param name="id">The unique ID of the element</param>
    /// <returns>A <see cref="Song"/> </returns>
    Task<Song> GetById(int id);
    
    /// <summary>
    /// Creates a new <see cref="Song"/> entity in Database. 
    /// </summary>
    /// <param name="song">A new song entity</param>
    /// <returns>The created song with an Id assigned</returns>
    Task<Song> CreateSong(Song song);
    
    /// <summary>
    /// Updates the <see cref="Song"/> entity in EF DB
    /// </summary>
    /// <param name="song">The Song entity to update</param>
    /// <returns>The new song with updated fields if successful</returns>
    Task<Song> UpdateSong(Song song);
}
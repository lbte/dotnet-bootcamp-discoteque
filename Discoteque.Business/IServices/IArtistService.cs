using Discoteque.Data.Dto;
using Discoteque.Data.Models;

namespace Discoteque.Business.IServices;

public interface IArtistService {
    // Task es lo que le dice a la interfaz que se van a convertir en tasks asíncronos
    // el task está obligado a tener el await en algún sitio

    /// <summary>
    /// Finds all artists in the DB
    /// </summary>
    /// <returns> A <see cref="List"/> of <see cref="Artist"/></returns>
    Task<IEnumerable<Artist>> GetArtistsAsync();
    
    /// <summary>
    /// Finds an artist by its id in the DB 
    /// </summary>
    /// <param name="id">The unique id of the artist</param>
    /// <returns>An <see cref="Artist"/></returns>
    Task<Artist> GetById(int id);
    
    /// <summary>
    /// Creates a new <see cref="Artist"/> entity in the DB
    /// </summary>
    /// <param name="artist">A new artist entity</param>
    /// <returns>The created artist with an assigned id</returns>
    Task<EntityMessage<Artist>> CreateArtist(Artist artist);
    
    /// <summary>
    /// Updates the <see cref="Artist"/> entity in the DB
    /// </summary>
    /// <param name="artist">The artist entity to update</param>
    /// <returns>The new artist with updated fields when successful</returns>
    Task<Artist> UpdateArtist(Artist artist);


}
using Discoteque.Data.Dto;
using Discoteque.Data.Models;

namespace Discoteque.Business.IServices;

// INTERFAZ
public interface IArtistService {
    // Task es lo que le dice a la interfaz que se van a convertir en tasks asíncronos
    // el task está obligado a tener el await en algún sitio
    Task<IEnumerable<Artist>> GetArtistsAsync();
    Task<Artist> GetById(int id);
    Task<ArtistMessage> CreateArtist(Artist artist);
    Task<Artist> UpdateArtist(Artist artist);


}
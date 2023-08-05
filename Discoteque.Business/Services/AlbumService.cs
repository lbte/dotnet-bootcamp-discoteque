using System.Net;
using Discoteque.Business.IServices;
using Discoteque.Data;
using Discoteque.Data.Dto;
using Discoteque.Data.Models;

namespace Discoteque.Business.Services;

public class AlbumService : IAlbumService {

    private readonly IUnitOfWork _unitOfWork;

    public AlbumService(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new <see cref="Album"/> entity in the DB
    /// </summary>
    /// <param name="album">A new album entity</param>
    /// <returns>The created album with an assigned Id</returns>
    public async Task<AlbumMessage> CreateAlbum(Album newAlbum)
    {
        try {
            // the artist must exists
            var artist = await _unitOfWork.ArtistRepository.FindAsync(newAlbum.ArtistId);

            // TODO: Condition for the forbidden words
            if(artist == null || newAlbum.Cost < 0 || newAlbum.Year < 1900 || newAlbum.Year > 2023) {
                return BuildResponse(HttpStatusCode.BadRequest, BaseMessageStatus.BAD_REQUEST_400);
            }

            await _unitOfWork.AlbumRepository.AddAsync(newAlbum);
            await _unitOfWork.SaveAsync();
        } catch (Exception) {
            return BuildResponse(HttpStatusCode.InternalServerError, BaseMessageStatus.INTERNAL_SERVER_ERROR_500);
        }

        return BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new(){newAlbum});
    }

    /// <summary>
    /// Find all albums
    /// </summary>
    /// <param name="areReferencesLoaded">Returns associated artists per album if true</param>
    /// <returns>A <see cref="List"/> of <see cref="Album"/></returns>
    public async Task<IEnumerable<Album>> GetAlbumsAsync(bool areReferencesLoaded)
    {
        IEnumerable<Album> albums;
        if (areReferencesLoaded) {
            albums = await _unitOfWork.AlbumRepository.GetAllAsync(null, x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        } else {
            albums = await _unitOfWork.AlbumRepository.GetAllAsync();
        }
        return albums;
    }

    /// <summary>
    /// Finds all albums released by <see cref="Artist.Name"/>
    /// </summary>
    /// <param name="artist">The name of the artist</param>
    /// <returns>A <see cref="List"/> of <see cref="Album"/></returns>
    public async Task<IEnumerable<Album>> GetAlbumsByArtist(string artist)
    {
        return await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Artist.Name.ToLower().Equals(artist.ToLower()), x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
    }

    /// <summary>
    /// Finds all albums with the assigned genre
    /// </summary>
    /// <param name="genre"></param>
    /// <returns>A <see cref="List"/> of <see cref="Album"/></returns>
    public async Task<IEnumerable<Album>> GetAlbumsByGenre(Genres genre)
    {
        return await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Genre.Equals(genre), x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
    }

    /// <summary>
    /// Finds all albums published in a year
    /// </summary>
    /// <param name="year"> A gregorian year between 1900 and the current year</param>
    /// <returns>A <see cref="List"/> of <see cref="Album"/></returns>
    public async Task<IEnumerable<Album>> GetAlbumsByYear(int year)
    {
        return await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Year == year, x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
    }

    /// <summary>
    /// Finds all albums released from initialYear to maxYear
    /// </summary>
    /// <param name="initialYear">The initial year. Minimum value is 1900</param>
    /// <param name="maxYear">The maximun year. Maximum value is 2023</param>
    /// <returns>A <see cref="List"/> of <see cref="Album"/></returns>
    public async Task<IEnumerable<Album>> GetAlbumsByYearRange(int initialYear, int maxYear)
    {
        return await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Year >= initialYear && x.Year <= maxYear, x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
    }

    /// <summary>
    /// Finds an album by its id in the DB
    /// </summary>
    /// <param name="id">The unique id of the album</param>
    /// <returns>An <see cref="Album"/></returns>
    public async Task<Album> GetById(int id)
    {
        return await _unitOfWork.AlbumRepository.FindAsync(id);
    }

    /// <summary>
    /// Updates the <see cref="Album"/> entity in the DB
    /// </summary>
    /// <param name="album">The album entity to update</param>
    /// <returns>The new album with updated fields when successful</returns>
    public async Task<Album> UpdateAlbum(Album album)
    {
        await _unitOfWork.AlbumRepository.Update(album);
        await _unitOfWork.SaveAsync();

        return album;
    }

    // TODO: Method to not accept album with the forbidden words in the name

    #region Messages
    private static AlbumMessage BuildResponse(HttpStatusCode statusCode, string message) {
        return new AlbumMessage{
            Message = message,
            TotalElements = 0,
            StatusCode = statusCode
        };
    }

    private static AlbumMessage BuildResponse(HttpStatusCode statusCode, string message, List<Album> album) {
        return new AlbumMessage{
            Message = message,
            TotalElements = album.Count,
            StatusCode = statusCode,
            Albums = album
        };
    }
    #endregion
}
using System.Net;
using Discoteque.Business.IServices;
using Discoteque.Business.Utils;
using Discoteque.Data;
using Discoteque.Data.Dto;
using Discoteque.Data.Models;

namespace Discoteque.Business.Services;

public class SongService : ISongService
{
    private readonly IUnitOfWork _unitOfWork;

    public SongService(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new <see cref="Song"/> entity in the DB. 
    /// </summary>
    /// <param name="song">A new song entity</param>
    /// <returns>The created song with an Id assigned</returns>
    public async Task<EntityMessage<Song>> CreateSong(Song newSong)
    {
        try {
            var album = await _unitOfWork.AlbumRepository.FindAsync(newSong.AlbumId);
            if(album == null) {
                return BuildResponseClass<Song>.BuildResponse(HttpStatusCode.NotFound, EntityMessageStatus.ALBUM_NOT_FOUND);
            }

            await _unitOfWork.SongRepository.AddAsync(newSong);
            await _unitOfWork.SaveAsync();
        } catch (Exception ex) {
            return BuildResponseClass<Song>.BuildResponse(HttpStatusCode.InternalServerError, ex.Message);
        }

        return BuildResponseClass<Song>.BuildResponse(HttpStatusCode.OK, EntityMessageStatus.OK_200, new(){newSong});
    }

    /// <summary>
    /// Creates a new set of <see cref="Song"/> entities in the DB.
    /// </summary>
    /// <param name="songs">A <see cref="List"/> of entities from Song</param>
    /// <returns></returns>
    public async Task<EntityMessage<Song>> CreateSongsInBatch(List<Song> songs) {
        try {
            foreach (var song in songs) {
                var album = await _unitOfWork.AlbumRepository.FindAsync(song.AlbumId);
                if(album == null) {
                    return BuildResponseClass<Song>.BuildResponse(HttpStatusCode.NotFound, EntityMessageStatus.ALBUM_NOT_FOUND);
                }
                await _unitOfWork.SongRepository.AddAsync(song);
            }
            await _unitOfWork.SaveAsync();
        } catch (Exception) {
            return BuildResponseClass<Song>.BuildResponse(HttpStatusCode.InternalServerError, EntityMessageStatus.INTERNAL_SERVER_ERROR_500);
        }
        return BuildResponseClass<Song>.BuildResponse(HttpStatusCode.OK, EntityMessageStatus.OK_200, songs);
    }

    /// <summary>
    /// Get a song by its DB Identity
    /// </summary>
    /// <param name="id">The unique id of the element</param>
    /// <returns>A <see cref="Song"/> </returns>
    public async Task<Song> GetById(int id)
    { 
        return await _unitOfWork.SongRepository.FindAsync(id);
    }

    /// <summary>
    /// Find all songs in the DB
    /// </summary>
    /// <param name="areReferencesLoaded">Returns associated albums per song if true</param>
    /// <returns>A <see cref="List"/> of <see cref="Song"/></returns>
    public async Task<IEnumerable<Song>> GetSongsAsync(bool areReferencesLoaded)
    {
        IEnumerable<Song> songs;
        if (areReferencesLoaded) {
            songs = await _unitOfWork.SongRepository.GetAllAsync(null, x => x.OrderBy(x => x.Id), new Album().GetType().Name);
        } else {
            songs = await _unitOfWork.SongRepository.GetAllAsync();
        }
        return songs;
    }

    /// <summary>
    /// A list of songs contained in <see cref="Album"/>
    /// </summary>
    /// <param name="albumId">The id of the album</param>
    /// <returns>A <see cref="List" /> of <see cref="Song"/> </returns>
    public async Task<IEnumerable<Song>> GetSongsByAlbum(int albumId)
    {
        return await _unitOfWork.SongRepository.GetAllAsync(x => x.AlbumId == albumId);
    }   

    /// <summary>
    /// Get a song by its <see cref="Album"/> released year
    /// </summary>
    /// <param name="year">The year the Album was released</param>
    /// <returns>A <see cref="List" /> of <see cref="Song"/> </returns>
    public async Task<IEnumerable<Song>> GetSongsByYear(int year)
    {
        return await _unitOfWork.SongRepository.GetAllAsync(x => x.Album.Year == year, includeProperties: new Album().GetType().Name);
    }

    /// <summary>
    /// Updates the <see cref="Song"/> entity in the DB
    /// </summary>
    /// <param name="song">The Song entity to update</param>
    /// <returns>The new song with updated fields if successful</returns>
    public async Task<Song> UpdateSong(Song song)
    {
        await _unitOfWork.SongRepository.Update(song);
        await _unitOfWork.SaveAsync();
        return song;
    }

}
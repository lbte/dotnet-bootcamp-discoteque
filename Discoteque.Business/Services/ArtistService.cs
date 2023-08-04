using System.Net;
using Discoteque.Business.IServices;
using Discoteque.Data;
using Discoteque.Data.Dto;
using Discoteque.Data.Models;

namespace Discoteque.Business.Services;

public class ArtistService : IArtistService
{
    private readonly IUnitOfWork _unitOfWork;

    public ArtistService(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    // public async Task<ArtistMessage> CreateArtist(Artist artist)
    public async Task<ArtistMessage> CreateArtist(Artist artist)
    {
        // create the instance
        var newArtist = new Artist() {
            Name = artist.Name, 
            Label = artist.Label, 
            IsOnTour = artist.IsOnTour
        };

        try {
            if (artist.Name.Length > 100){
                return BuildResponse(HttpStatusCode.BadRequest, BaseMessageStatus.BAD_REQUEST_400);
            }

            await _unitOfWork.ArtistRepository.AddAsync(newArtist);
            await _unitOfWork.SaveAsync();
        } catch (Exception) {
            return BuildResponse(HttpStatusCode.BadRequest, BaseMessageStatus.INTERNAL_SERVER_ERROR_500);
        }

        // returns the artist that was created
        return BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new(){artist});
    }

    public async Task<IEnumerable<Artist>> GetArtistsAsync()
    {
        return await _unitOfWork.ArtistRepository.GetAllAsync();
    }

    public async Task<Artist> GetById(int id)
    {
        return await _unitOfWork.ArtistRepository.FindAsync(id);
    }

    public async Task<Artist> UpdateArtist(Artist artist)
    {
        await _unitOfWork.ArtistRepository.Update(artist);
        await _unitOfWork.SaveAsync();
        return artist;
    }

    #region Messages
    private static ArtistMessage BuildResponse(HttpStatusCode statusCode, string message) {
        return new ArtistMessage{
            Message = message,
            TotalElements = 0,
            StatusCode = statusCode
        };
    }

    private static ArtistMessage BuildResponse(HttpStatusCode statusCode, string message, List<Artist> artist) {
        return new ArtistMessage{
            Message = message,
            TotalElements = artist.Count,
            StatusCode = statusCode,
            Artists = artist
        };
    }
    #endregion

}
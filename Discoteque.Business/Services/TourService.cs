using System.Net;
using Discoteque.Business.IServices;
using Discoteque.Business.Utils;
using Discoteque.Data;
using Discoteque.Data.Dto;
using Discoteque.Data.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace Discoteque.Business.Services;

public class TourService : ITourService
{
    private readonly IUnitOfWork _unitOfWork;

    public TourService(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new <see cref="Tour"/> entity in the DB
    /// </summary>
    /// <param name="tour">A new tour entity</param>
    /// <returns>The created tour with an assigned id</returns>
    public async Task<EntityMessage<Tour>> CreateTour(Tour tour)
    {
        try {
            var artist = await _unitOfWork.ArtistRepository.FindAsync(tour.ArtistId);
            if (tour.TourDate.Year <= 2021 || artist == null) {
                return BuildResponseClass<Tour>.BuildResponse(HttpStatusCode.NotFound, EntityMessageStatus.ARTIST_NOT_FOUND);
            }

            await _unitOfWork.TourRepository.AddAsync(tour);
            await _unitOfWork.SaveAsync();
        } catch (Exception) {
            return BuildResponseClass<Tour>.BuildResponse(HttpStatusCode.InternalServerError, EntityMessageStatus.INTERNAL_SERVER_ERROR_500);
        }

        return BuildResponseClass<Tour>.BuildResponse(HttpStatusCode.OK, EntityMessageStatus.OK_200, new(){tour});
    }

    /// <summary>
    /// Find a tour by its id in the DB
    /// </summary>
    /// <param name="id">The unique id of the tour</param>
    /// <returns>A <see cref="Tour"/></returns>
    public async Task<Tour> GetTourById(int id)
    {
        return await _unitOfWork.TourRepository.FindAsync(id);
    }

    /// <summary>
    /// Find all tours in the DB
    /// </summary>
    /// <param name="areReferencesLoaded">Returns associated artists per tour if true</param>
    /// <returns>A <see cref="List"/> of <see cref="Tour"/></returns>
    public async Task<IEnumerable<Tour>> GetToursAsync(bool areReferencesLoaded)
    {
        IEnumerable<Tour> tours;
        if (areReferencesLoaded) {
            tours = await _unitOfWork.TourRepository.GetAllAsync(null, x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        } else {
            tours = await _unitOfWork.TourRepository.GetAllAsync();
        }
        return tours;
    }

    /// <summary>
    /// Finds all tours from an <see cref="Artist"/>
    /// </summary>
    /// <param name="artistId">The id from the Artist associated</param>
    /// <returns>A <see cref="List"/> of <see cref="Tour"/></returns>
    public async Task<IEnumerable<Tour>> GetToursByArtist(int artistId)
    {
        return await _unitOfWork.TourRepository.GetAllAsync(x => x.ArtistId == artistId, includeProperties: new Artist().GetType().Name);
    }

    /// <summary>
    /// Finds all tours from a specific city
    /// </summary>
    /// <param name="city"></param>
    /// <returns>A <see cref="List"/> of <see cref="Tour"/></returns>
    public async Task<IEnumerable<Tour>> GetToursByCity(string city)
    {
        return await _unitOfWork.TourRepository.GetAllAsync(x => x.City.ToLower().Equals(city.ToLower()));
    }

    /// <summary>
    /// Finds all tours from a specific year
    /// </summary>
    /// <param name="year">A gregorian year between 1900 and the current year (2023)</param>
    /// <returns>A <see cref="List"/> of <see cref="Tour"/></returns>
    public async Task<IEnumerable<Tour>> GetToursByYear(int year)
    {
        return await _unitOfWork.TourRepository.GetAllAsync(x => x.TourDate.Year == year);
    }

    /// <summary>
    /// Updates the <see cref="Tour"/> entity in the DB
    /// </summary>
    /// <param name="tour">The tour entity to update</param>
    /// <returns>The new tour with updated fields when successful</returns>
    public async Task<Tour> UpdateTour(Tour tour)
    {
        await _unitOfWork.TourRepository.Update(tour);
        await _unitOfWork.SaveAsync();
        return tour;
    }

}
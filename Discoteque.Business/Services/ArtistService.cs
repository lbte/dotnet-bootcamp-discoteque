using Discoteque.Business.IServices;
using Discoteque.Data.Models;

namespace Discoteque.Business.Services;


public class ArtistService : IArtistService
{
    private Random rand = new Random();
    public async Task<IEnumerable<Artist>> CreateArtist(Artist artist)
    {
        var artists = new List<Artist>();
        artists.Add(new Artist() {
            Id = rand.Next(1000, 1999), 
            Name = artist.Name, 
            Label = artist.Label, 
            IsOnTour = artist.IsOnTour
        });

        return artists;
    }

    public async Task<IEnumerable<Artist>> GetArtistsAsync()
    {
        var artists = new List<Artist>();
        artists.Add(new Artist() {Id = rand.Next(1000, 1999), Name = "Niall Horan", IsOnTour = true});
        artists.Add(new Artist() {Id = rand.Next(1000, 1999), Name = "Harry Styles", Label = "Pop", IsOnTour = true});
        artists.Add(new Artist() {Id = rand.Next(1000, 1999), Name = "The Lumineers", Label = "Indie", IsOnTour = false});

        return artists;
    }

    public Task<Artist> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Artist> UpdateArtist(Artist artist)
    {
        throw new NotImplementedException();
    }
}
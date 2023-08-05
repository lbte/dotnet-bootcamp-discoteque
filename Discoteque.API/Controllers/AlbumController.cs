using System.Net;
using Discoteque.Business.IServices;
using Discoteque.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Discoteque.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlbumController : ControllerBase {
    private readonly IAlbumService _albumService;

    public AlbumController(IAlbumService albumService) {
        _albumService = albumService;
    }

    [HttpGet]
    [Route("GetAlbums")]
    public async Task<IActionResult> GetAlbums(bool areReferencesLoaded = false){
        var albums = await _albumService.GetAlbumsAsync(areReferencesLoaded);
        return albums.Any() ? Ok(albums) : StatusCode(StatusCodes.Status404NotFound, "There were no albums found to show");
    }

    [HttpGet]
    [Route("GetAlbumById")]
    public async Task<IActionResult> GetAlbumById(int id){
        var album = await _albumService.GetById(id);
        return album != null ? Ok(album) : StatusCode(StatusCodes.Status404NotFound, $"There was no album found with the Id number {id}");
    }

    [HttpGet]
    [Route("GetAlbumsByYear")]
    public async Task<IActionResult> GetAlbumsByYear(int year){
        var albums = await _albumService.GetAlbumsByYear(year);
        //if the albums list is not empty show success, otherwise show message
        return albums.Any() ? Ok(albums) : StatusCode(StatusCodes.Status404NotFound, "There were no albums found in this year");
    }

    [HttpGet]
    [Route("GetAlbumsByYearRange")]
    public async Task<IActionResult> GetAlbumsByYearRange(int initialYear, int maxYear) {
        var albums = await _albumService.GetAlbumsByYearRange(initialYear, maxYear);
        return albums.Any() ? Ok(albums) : StatusCode(StatusCodes.Status404NotFound, "There were no albums found in this year range");
    }

    [HttpGet]
    [Route("GetAlbumsByGenre")]
    public async Task<IActionResult> GetAlbumsByGenre(Genres genre) {
        var albums = await _albumService.GetAlbumsByGenre(genre);
        return albums.Any() ? Ok(albums) : StatusCode(StatusCodes.Status404NotFound, $"There were no albums found with the genre {genre}");
    }

    [HttpGet]
    [Route("GetAlbumsByArtist")]
    public async Task<IActionResult> GetAlbumsByArtist(string artist) {
        var albums = await _albumService.GetAlbumsByArtist(artist);
        return albums.Any() ? Ok(albums) : StatusCode(StatusCodes.Status404NotFound, $"There were no albums found from the artist {artist}");
    }

    [HttpPost]
    [Route("CreateAlbum")]
    public async Task<IActionResult> CreateAlbum(Album album) {
        var newAlbum = await _albumService.CreateAlbum(album);
        return newAlbum.StatusCode == HttpStatusCode.OK ? Ok(newAlbum) : StatusCode((int)newAlbum.StatusCode, newAlbum);
    }

    [HttpPatch]
    [Route("UpdateAlbum")]
    public async Task<IActionResult> UpdateAlbum(Album album) {
        var updatedAlbum = await _albumService.UpdateAlbum(album);
        return updatedAlbum != null ? Ok(updatedAlbum) : StatusCode(StatusCodes.Status404NotFound, "The album was not updated");;
    }

    
}
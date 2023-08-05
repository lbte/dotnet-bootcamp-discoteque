using System.Data.SqlTypes;
using System.Net;
using Discoteque.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Discoteque.Business.IServices;

namespace Discoteque.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SongController : ControllerBase
{
    private readonly ISongService _songService;

    public SongController(ISongService songService)
    {
        _songService = songService;
    }

    [HttpGet]
    [Route("GetSongs")]
    public async Task<IActionResult> GetSongs()
    {
        var songs = await _songService.GetSongsAsync();
        return songs.Any() ? Ok(songs) : StatusCode(StatusCodes.Status404NotFound, "There were no songs found to show");
    }

    [HttpGet]
    [Route("GetSongById")]
    public async Task<IActionResult> GetSongById(int id)
    {
        var song = await _songService.GetById(id);
        return song != null ? Ok(song) : StatusCode(StatusCodes.Status404NotFound, $"There was no song found with the Id number {id}");
    }

    [HttpGet]
    [Route("GetSongsByAlbum")]
    public async Task<IActionResult> GetSongsByAlbum(int albumId)
    {
        var songs = await _songService.GetSongsByAlbum(albumId);
        return songs.Any() ? Ok(songs) : StatusCode(StatusCodes.Status404NotFound,  "There were no songs in this album");
    }

    [HttpGet]
    [Route("GetSongsByYear")]
    public async Task<IActionResult> GetSongsByYear(int year) {
        var songs = await _songService.GetSongsByYear(year);
        return songs.Any() ? Ok(songs) : StatusCode(StatusCodes.Status404NotFound,  "There were no songs in this year");
    }

    [HttpPost]
    [Route("CreateSong")]
    public async Task<IActionResult> CreateSongsAsync(Song Song)
    {
        var newSong = await _songService.CreateSong(Song);
        return newSong.StatusCode == HttpStatusCode.OK ? Ok(newSong) : StatusCode((int)newSong.StatusCode, newSong);
    }

    [HttpPatch]
    [Route("UpdateSong")]
    public async Task<IActionResult> UpdateSong(Song song) {
        var updatedSong = await _songService.UpdateSong(song);
        return Ok(updatedSong);
    }
}
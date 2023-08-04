using System.Data.SqlTypes;
using Discoteque.Data.Models;
using Discoteque.Data.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetSongs(bool areReferencesLoaded = false)
    {
        var songs = await _songService.GetSongsAsync(areReferencesLoaded);
        return Ok(songs);
    }

    [HttpGet]
    [Route("GetSongById")]
    public async Task<IActionResult> GetById(int id)
    {
        var song = await _songService.GetById(id);
        return Ok(song);
    }

    [HttpGet]
    [Route("GetSongsByAlbum")]
    public async Task<IActionResult> GetSongsByAlbum(string album)
    {
        var songs = await _songService.GetSongsByAlbum(album);
        return songs.Any() ? Ok(songs) : StatusCode(StatusCodes.Status404NotFound,  "There was no songs by this album");
    }

    [HttpPost]
    [Route("CreateSong")]
    public async Task<IActionResult> CreateSongsAsync(Song Song)
    {
        var result = await _songService.CreateSong(Song);
        return Ok(result);
    }
}
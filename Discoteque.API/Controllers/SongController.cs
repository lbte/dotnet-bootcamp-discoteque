using System.Data.SqlTypes;
using System.Net;
using System;
using Discoteque.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Discoteque.Business.IServices;
using Discoteque.Data.Dto;

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
    [Route("GetSongById")]
    public async Task<IActionResult> GetSongById(int id)
    {
        var song = await _songService.GetById(id);
        return song != null ? Ok(song) : StatusCode(StatusCodes.Status404NotFound, $"There was no song found with the Id number {id}");
    }

    [HttpGet]
    [Route("GetSongs")]
    public async Task<IActionResult> GetSongs(bool areReferencesLoaded = false)
    {
        var songs = await _songService.GetSongsAsync(areReferencesLoaded);
        return songs.Any() ? Ok(songs) : StatusCode(StatusCodes.Status404NotFound, "There were no songs found to show");
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
    public async Task<IActionResult> CreateSong(Song song)
    {
        var newSong = await _songService.CreateSong(song);
        return newSong.StatusCode == HttpStatusCode.OK ? Ok(newSong) : StatusCode((int)newSong.StatusCode, newSong);
    }

    [HttpPost]
    [Route("CreateSongs")]
    public async Task<IActionResult> CreateSongs(List<Song> songs)
    {
        var newSongs = await _songService.CreateSongsInBatch(songs);
        return newSongs.StatusCode == HttpStatusCode.OK ? Ok(newSongs) : StatusCode((int)newSongs.StatusCode, newSongs);
    }

    [HttpPatch]
    [Route("UpdateSong")]
    public async Task<IActionResult> UpdateSong(Song song) {
        var updatedSong = await _songService.UpdateSong(song);
        return updatedSong != null ? Ok(updatedSong) : StatusCode(StatusCodes.Status404NotFound, "The song was not updated");
    }
}
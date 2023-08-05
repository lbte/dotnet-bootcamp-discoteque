using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Discoteque.Business.IServices;
using Discoteque.Data.Models;
using System.Net;

namespace Discoteque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService) {
            // se recibe la instancia llena para ejecutar los métodos cuando alguien haga una solicitud
            _artistService = artistService; 
        }

        [HttpGet]
        [Route("GetAllArtistsAsync")]
        public async Task<IActionResult> GetAllArtistsAsync() {
            var artists = await _artistService.GetArtistsAsync();
            return artists.Any() ? Ok(artists) : StatusCode(StatusCodes.Status404NotFound, "There were no artists found to show");
        }

        [HttpPost]
        [Route("CreateArtistAsync")]
        public async Task<IActionResult> CreateArtistAsync(Artist artist) {
            var newArtist = await _artistService.CreateArtist(artist);
            return newArtist.StatusCode == HttpStatusCode.OK ? Ok(newArtist) : StatusCode((int)newArtist.StatusCode, newArtist);
        }

        [HttpPatch]
        [Route("UpdateArtistAsync")]
        public async Task<IActionResult> UpdateArtistAsync(Artist artist) {
            var updatedArtist = await _artistService.UpdateArtist(artist);
            return Ok(updatedArtist);
        }    
    }
}

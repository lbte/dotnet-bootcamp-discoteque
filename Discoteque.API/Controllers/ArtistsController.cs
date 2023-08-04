using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Discoteque.Business.IServices;
using Discoteque.Data.Models;

namespace Discoteque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        // constructor
        public ArtistsController(IArtistService artistService) {
            // se recibe la instancia llena para ejecutar los métodos cuando alguien haga una solicitud
            _artistService = artistService; 
        }

        [HttpGet]
        [Route("GetAllArtistsAsync")]
        public async Task<IActionResult> GetAllArtistsAsync() {
            var artists = await _artistService.GetArtistsAsync();
            return Ok(artists);
        }

        [HttpPost]
        [Route("CreateArtistAsync")]
        public async Task<IActionResult> CreateArtistAsync(Artist artist) {
            var artists = await _artistService.CreateArtist(artist);
            return Ok(artists);
        }

        [HttpPatch]
        [Route("UpdateArtistAsync")]
        public async Task<IActionResult> UpdateArtistAsync(Artist artist) {
            return Ok();
        }    
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Octapull.Application.Abstractions.Storage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Octapull.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IBlobService _blobService;

        public StorageController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpPost]
        public async Task<IResult> UploadAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Results.BadRequest("No file uploaded.");
            }

            var fileId = await _blobService.UploadAsync(file.OpenReadStream(), "documents", file.ContentType);
            return Results.Ok(fileId);
        }

        [HttpGet("{id}")]
        public async Task<IResult> DownloadAsync(Guid id)
        {
            FileResponse response = await _blobService.DownloadAsync(id, "documents");

            if (response == null)
            {
                return Results.NotFound("File not found!");
            }

            return Results.File(response.Stream, response.ContentType);
        }


        [HttpDelete("{id}")]
        public async Task<IResult> DeleteAsync(Guid id)
        {
            await _blobService.DeleteAsync(id, "documents");

            return Results.NoContent();
        }
    }
}

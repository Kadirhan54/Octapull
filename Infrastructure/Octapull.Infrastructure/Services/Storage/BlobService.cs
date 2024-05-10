using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Octapull.Application.Abstractions.Storage;

namespace Octapull.Infrastructure.Services.Storage
{
    internal sealed class BlobService : IBlobService
    {

        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<Guid> UploadAsync(Stream stream, string containerName, string contentType, CancellationToken cancellationToken = default)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            var fileId = Guid.NewGuid();

            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            await blobClient.UploadAsync(stream,
                new BlobHttpHeaders { ContentType = contentType },
                cancellationToken: cancellationToken);

            return fileId;
        }

        public async Task<FileResponse>? DownloadAsync(Guid fileId, string containerName, CancellationToken cancellationToken = default)
        {

            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString().ToLower());

            try
            {
                Response<BlobDownloadResult> response = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);

                return new FileResponse(response.Value.Content.ToStream(), response.Value.Details.ContentType);

            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task DeleteAsync(Guid fileId, string containerName, CancellationToken cancellationToken = default)
        {

            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, cancellationToken: cancellationToken);
        }
    }
}

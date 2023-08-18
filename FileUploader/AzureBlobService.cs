using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage;
using Azure.Storage.Blobs;

namespace FileUploader
{
	public class AzureBlobService
	{
        // First Step
        private readonly BlobServiceClient _blobServiceClient;


		// Second Step
        public AzureBlobService()
		{
            //var credential = new StorageSharedKeyCredential(_storageAccount, _accessKey);
            //var blobUri = $"https://{_storageAccount}.blob.core.windows.net";
            //_blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);

            // Get the credentials from azure key vault
            var client = new SecretClient(vaultUri: new Uri("https://kv-bookman-test-002.vault.azure.net/"), credential: new DefaultAzureCredential());
			KeyVaultSecret storageAccount = client.GetSecret("azblobstorageaccount");
            KeyVaultSecret accessKey = client.GetSecret("azblobaccesskey");

			// Create the blob service client
			var credential = new StorageSharedKeyCredential(storageAccount.Value, accessKey.Value);
			var blobUri = $"https://{storageAccount.Value}.blob.core.windows.net";
			_blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
        }

		// List all the Blob Containers
		public async Task ListBlobContainersAsync()
		{
			var containers = _blobServiceClient.GetBlobContainersAsync();
			

			await foreach (var container in containers)
			{
				Console.WriteLine(container.Name);
			}
		}

		// Upload files Asynchronously
		public async Task<List<Uri>> UploadFilesAsync()
		{
			var blobUris = new List<Uri>();
			string fileName = "book-image-1.jpeg";
			string filePath = Path.Combine($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Projects/FileUploader/FileUploader", fileName);
			var blobContainer = _blobServiceClient.GetBlobContainerClient("images");

			var blob = blobContainer.GetBlobClient($"today/{fileName}");
			var tomorrowBlob = blobContainer.GetBlobClient($"tomorrow/{fileName}");
			Console.WriteLine(blob.Uri.AbsoluteUri);

			await blob.UploadAsync(filePath, true);
			blobUris.Add(blob.Uri);
			await tomorrowBlob.UploadAsync(filePath, true);
			blobUris.Add(tomorrowBlob.Uri);


			return blobUris;
		}

		// Get flat list of blobs Asynchronously
		public async Task GetFlatBlobsListAsync()
		{
			var blobContainer = _blobServiceClient.GetBlobContainerClient("images");
			Console.WriteLine(blobContainer.Uri.AbsoluteUri);
			var blobs = blobContainer.GetBlobsAsync();


			await foreach (var blob in blobs)
			{
				Console.WriteLine("Blob name: {0}", blob.Properties.CopySource);
			}
		}

		// Get Hierarchical list of blobs Asynchronously
		public async Task GetHierarchicalBlobsListAsync()
		{
			var blobContainer = _blobServiceClient.GetBlobContainerClient("images");
			var blobs = blobContainer.GetBlobsByHierarchyAsync();


			await foreach (var blob in blobs)
			{
				if (blob.IsPrefix)
				{
					// Write out the prefix of the virtual directory.
					Console.WriteLine("Virtual directory prefix: {0}", blob.Prefix);

					// Call recursively with the prefix to traverse the virtual directory.
					await GetHierarchicalBlobsListAsync();
				}
				else
				{
					// Write out the name of the blob.
					Console.WriteLine("Blob name: {0}", blob.Blob.Name);
				}
			}
		}

		// Delete blob
		public async Task DeleteBlobAsync()
		{
			string fileName = "book-image-1.jpeg";
			var blobContainer = _blobServiceClient.GetBlobContainerClient("images");
			var blob = blobContainer.GetBlobClient($"today/{fileName}");

			bool deleteStatus = await blob.DeleteIfExistsAsync();

			if (deleteStatus == true)
			{
				Console.WriteLine($"The file '{fileName}' was successfully deleted from Azure Storage blob.");
			}
			else
			{
				Console.WriteLine($"Something went wrong while trying to delete the file '{fileName}'.");
			}
		}
	}
}


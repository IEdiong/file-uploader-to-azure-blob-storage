using FileUploader;

var service = new AzureBlobService();
await service.ListBlobContainersAsync();
await service.UploadFilesAsync();
await service.GetFlatBlobsListAsync();
Console.WriteLine("===================");
await service.GetHierarchicalBlobsListAsync();
//await service.DeleteBlobAsync();


//string fileName = "book-image.jpeg";
//string filePath = Path.Combine(AppContext.BaseDirectory, fileName);


//Console.WriteLine($"Full path to the file: {filePath}");
//Console.WriteLine(Directory.GetCurrentDirectory());
//Console.WriteLine(AppContext.BaseDirectory);
//Console.WriteLine(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location ?? string.Empty));
//Console.WriteLine($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Projects/FileUploader/FileUploader/book-image-1.jpeg");



/// Key vault

// Create a new secret client using the default credential from Azure.Identity using environment variables previously set,
// including AZURE_CLIENT_ID, AZURE_CLIENT_SECRET, and AZURE_TENANT_ID.
//var client = new SecretClient(vaultUri: new Uri("https://kv-bookman-test-002.vault.azure.net/"), credential: new DefaultAzureCredential());

// Create a new secret using the secret client.
//KeyVaultSecret secret = client.SetSecret("secret-name", "secret-value");

// Retrieve a secret using the secret client.
//secret = client.GetSecret("secret-name");
//KeyVaultSecret storageAccount = await client.GetSecretAsync("azblobstorageaccount");
//KeyVaultSecret accessKey = await client.GetSecretAsync("azblobaccesskey");

//Console.WriteLine($"{storageAccount.Name}: {storageAccount.Value}");
//Console.WriteLine($"{accessKey.Name}: {accessKey.Value}");



//Console.WriteLine(secret.Name);
//Console.WriteLine(secret.Value);
//Console.WriteLine(secret.Properties.Version);
//Console.WriteLine(secret.Properties.Enabled);

//var keyVaultUri = new Uri("https://kv-bookman-test-002.vault.azure.net/");
//var azureCredential = new DefaultAzureCredential();
//var secretClient = new SecretClient(keyVaultUri, azureCredential);
//var storageAccount = secretClient.GetSecret("azstorageblobaccount");
//var accessKey = secretClient.GetSecret("azaccesskey");


//Console.WriteLine($"Storage Account: ", storageAccount.Value);
//Console.WriteLine($"Access Key: ", accessKey.Value);


//Console.WriteLine("Hello, World!");


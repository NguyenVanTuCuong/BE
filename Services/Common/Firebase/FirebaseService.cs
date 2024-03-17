using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;

namespace Services.Common.Firebase
{
    public class FirebaseService : IFirebaseService
    {
        private readonly string BUCKET_NAME = "prn231-b934a.appspot.com";
        private readonly string GOOGLE_CREDENTIAL = "google-credential.json";


        private readonly StorageClient _storage;
        public FirebaseService()
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(GOOGLE_CREDENTIAL),
                ProjectId = "prn231-b934a"
            }
            );
            var builder = new StorageClientBuilder()
            {
                GoogleCredential = GoogleCredential.FromFile(GOOGLE_CREDENTIAL)
            };
            _storage = builder.Build();
        }

        public async Task<string?> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;
                string destinationObjectName = Path.GetFileName(file.FileName);
                var upload = await _storage.UploadObjectAsync(BUCKET_NAME, destinationObjectName, null, memoryStream);
                return upload.MediaLink;

            }
        }
    }
}

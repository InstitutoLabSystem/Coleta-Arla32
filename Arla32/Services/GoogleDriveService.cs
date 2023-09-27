using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


namespace Arla32.Services
{
    public class GoogleDriveService
    {
        private DriveService _driveService;

        public GoogleDriveService()
        {  // Autenticação usando as credenciais do arquivo JSON
            var credential = GoogleCredential.FromFile("Services/credentialsGoogleDrive.json")
                .CreateScoped(new[] { DriveService.Scope.DriveFile });

            _driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "NomeDoSeuApp"
            });
        }

    
        public async Task<string> UploadImageToFolderAsync(string filePath)
        {
            string folderId = "1tGyDNdL30GMNUaLjxlhimgzWq9JwUgF3"; // ID da pasta de destino

            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = Path.GetFileName(filePath),
                Parents = new List<string> { folderId } // Adiciona o ID da pasta de destino
            };

            FilesResource.CreateMediaUpload request;

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                request = _driveService.Files.Create(fileMetadata, stream, "image/jpg");
                request.Fields = "id";
                await request.UploadAsync();
            }

            var file = request.ResponseBody;
            return file.Id;
        }

    }
}

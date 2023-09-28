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
using static Arla32.Models.ColetaModel;

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

        public async Task<UploadResult> UploadImageToFolderAsync(IFormFile file)
        {
            string folderId = "1tGyDNdL30GMNUaLjxlhimgzWq9JwUgF3"; // ID da pasta de destino

            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = file.FileName,
                Parents = new List<string> { folderId } // Adiciona o ID da pasta de destino
            };

            FilesResource.CreateMediaUpload request;

            using (var stream = file.OpenReadStream())
            {
                request = _driveService.Files.Create(fileMetadata, stream, file.ContentType);
                request.Fields = "id, webViewLink"; // Solicita o webViewLink além do ID
                await request.UploadAsync();
            }

            var uploadedFile = request.ResponseBody;

            // Criar e retornar o objeto UploadResult com os dados necessários
            var uploadResult = new UploadResult
            {
                WebViewLink = uploadedFile.WebViewLink,
                ImgId = uploadedFile.Id  // Obtém o ID da imagem no Google Drive
            };

            return uploadResult;
        }


    }
}

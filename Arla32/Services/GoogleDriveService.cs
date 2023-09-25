//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Drive.v3;
//using Google.Apis.Services;
//using Google.Apis.Upload;
//using Google.Apis.Util.Store;
//using System;
//using System.IO;
//using System.Threading;
//using System.Threading.Tasks;


//namespace Arla32.Services
//{
//    public class GoogleDriveService
//    {
//        private DriveService _driveService;

//        public GoogleDriveService()
//        {
//            // Autenticação usando o arquivo de credenciais
//            UserCredential credential;
//            using (var stream = new FileStream("AIzaSyCtREZvaFkGAHe1hxo1nZY4OVyvyq3kWqk", FileMode.Open, FileAccess.Read))
//            {
//                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
//                    GoogleClientSecrets.Load(stream).Secrets,
//                    new[] { DriveService.Scope.DriveFile },
//                    "user",
//                    CancellationToken.None).Result;
//            }

//            _driveService = new DriveService(new BaseClientService.Initializer()
//            {
//                HttpClientInitializer = credential,
//                ApplicationName = "NomeDoSeuApp"
//            });
//        }

//        public async Task<string> UploadImageAsync(string filePath)
//        {
//            var fileMetadata = new Google.Apis.Drive.v3.Data.File
//            {
//                Name = Path.GetFileName(filePath)
//            };

//            FilesResource.CreateMediaUpload request;

//            using (var stream = new FileStream(filePath, FileMode.Open))
//            {
//                request = _driveService.Files.Create(fileMetadata, stream, "image/jpeg");
//                request.Fields = "id";
//                await request.UploadAsync();
//            }

//            var file = request.ResponseBody;
//            return file.Id;
//        }
//    }
//}

using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleDriveTreeView
{
    public class GoogleDriveAPI
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        private string[] Scopes = { DriveService.Scope.DriveReadonly };
        private string ApplicationName = "Drive API .NET Quickstart";
        private DriveService service { get; set;}

        public GoogleDriveAPI()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
   
        }

        public Google.Apis.Drive.v3.Data.File GetRootId()
        {
            var rootRequest = service.Files.Get("root");
            var root = rootRequest.Execute();
            return root;
        }

        public FileList GetDirectories(string parentId)
        {
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.Fields = "nextPageToken, files(id, name, parents)";
            listRequest.Q = $"mimeType = 'application/vnd.google-apps.folder' and '{parentId}' in parents";
            return listRequest.Execute();
        }

        public FileList GetFiles(string parentId)
        {
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.Fields = "nextPageToken, files(id, name, parents)";
            listRequest.Q = $"mimeType != 'application/vnd.google-apps.folder' and '{parentId}' in parents";
            return listRequest.Execute();
        }
    }
}

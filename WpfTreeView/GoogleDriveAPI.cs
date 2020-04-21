using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace GoogleDriveTreeView
{
    public static class GoogleDriveAPI
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        private static string[] Scopes = { DriveService.Scope.Drive };
        private static string ApplicationName = "Drive API .NET Quickstart";

        private static DriveService initService()
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

            return service;
        }
        /// <summary>
        /// Service field that let's the app to send requests.
        /// </summary>
        private static DriveService service = initService();

        /// <summary>
        /// Gets the id of the root directory on the drive (My Drive).
        /// </summary>
        /// <returns></returns>
        public static Google.Apis.Drive.v3.Data.File GetRootId()
        {
            var rootRequest = service.Files.Get("root");
            var root = rootRequest.Execute();
            return root;
        }

        /// <summary>
        /// Gets directories in the parent directory of given id. 
        /// </summary>
        /// <param name="parentId">The id of the parent directory.</param>
        /// <returns></returns>
        public static FileList GetDirectories(string parentId)
        {
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.Fields = "nextPageToken, files(id, name, parents)";
            listRequest.Q = $"mimeType = 'application/vnd.google-apps.folder' and '{parentId}' in parents";
            return listRequest.Execute();
        }

        /// <summary>
        /// Gets files in the parent directory of given id. 
        /// </summary>
        /// <param name="parentId">The id of the parent directory.</param>
        /// <returns></returns>
        public static FileList GetFiles(string parentId)
        {
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.Fields = "nextPageToken, files(id, name, parents)";
            listRequest.Q = $"mimeType != 'application/vnd.google-apps.folder' and '{parentId}' in parents";
            return listRequest.Execute();
        }

        /// <summary>
        /// Downloads the file
        /// </summary>
        /// <param name="fileId">The id of the file</param>
        /// <param name="fileName">The name of the file</param>
        /// <param name="savePath">The path where to save the file</param>
        public static void DownloadFile(string fileId, string fileName, string savePath)
        {

            savePath += '\\' + fileName;
            MemoryStream outputStream = new MemoryStream();
            service.Files.Get(fileId).Download(outputStream);
            using (var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                fileStream.Write(outputStream.GetBuffer(), 0, outputStream.GetBuffer().Length);
            }
        }

        /// <summary>
        /// Downloads the whole directory and it's content.
        /// </summary>
        /// <param name="dirId">The id of the driectory</param>
        /// <param name="dirName">The name of the directory</param>
        /// <param name="savePath">The path where to save the directory</param>
        public static void DownloadDirectory(string dirId, string dirName, string savePath)
        {
            savePath += '\\' + dirName;
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            // Recursively download nested directories.
            var dirs = GetDirectories(dirId);
            foreach (var dir in dirs.Files)
            {
                DownloadDirectory(dir.Id, dir.Name, savePath);
            }

            // Dowload all files in the directory.
            var fs = GetFiles(dirId);
            foreach (var file in fs.Files)
            {
                DownloadFile(file.Id, file.Name, savePath);
            }
        }

        public static void Delete(string itemId)
        {
            service.Files.Delete(itemId).Execute();
        }

        /// <summary>
        /// Creates a new directory on the drive
        /// </summary>
        public static void CreateDirectory(string dirName, string parentId)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = dirName,
                MimeType = "application/vnd.google-apps.folder"
            };
            fileMetadata.Parents = new List<string>();
            fileMetadata.Parents.Add(parentId);

            var dir = service.Files.Create(fileMetadata);
            dir.Fields = "id";
            dir.Execute();
        }

        /// <summary>
        /// Uploads a new file to the drive
        /// </summary>
        public static void UploadFile(string filePath, string parentId)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = DirectoryStructure.GetFileFolderName(filePath)
            };
            fileMetadata.Parents = new List<string>();
            fileMetadata.Parents.Add(parentId);

            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, null);
                request.Fields = "id, parents";
                request.Upload();
            }
        }
    }
}

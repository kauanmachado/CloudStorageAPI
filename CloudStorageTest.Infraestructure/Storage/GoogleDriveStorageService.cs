﻿using CloudStorageTest.Domain.Entities;
using CloudStorageTest.Domain.Storage;
using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Http;

namespace CloudStorageTest.Infraestructure.Storage;
public class GoogleDriveStorageService : IStorageService
{
    public string Upload(IFormFile file, User user)
    {
        var service = new DriveService();

        var driveFile = new Google.Apis.Drive.v3.Data.File
        {
            Name = file.Name,
            MimeType = file.ContentType,

        };

        var command = service.Files.Create(driveFile, file.OpenReadStream(), file.ContentType);
        var response = command.Upload();

        if (response.Status is not Google.Apis.Upload.UploadStatus.Completed)
            throw response.Exception;

        return command.ResponseBody.Id;
    }
}

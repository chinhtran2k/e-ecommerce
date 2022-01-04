﻿using EC.APPLICATION.Base.Interfaces;
using EC.CORE.Constants;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EC.APPLICATION.Base.Services
{
    public class FileStorageService : IStorageService
    {
        private readonly string _userContentFolder;

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, ApplicationConstant.USER_CONTENT_FOLDER_NAME);
        }
        public string GetFileUrl(string fileName)
        {
            return $"/{ApplicationConstant.USER_CONTENT_FOLDER_NAME}/{fileName}";
        }
        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }
    }
}

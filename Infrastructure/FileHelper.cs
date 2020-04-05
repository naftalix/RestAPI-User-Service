using System;
using System.Linq;
using Microsoft.AspNetCore.StaticFiles;

namespace UserManagment.Infrastructure
{
    public static class FileHelper
    {
        public static string GetMimeType(this string fileName)
        {
            // Make Sure Microsoft.AspNetCore.StaticFiles Nuget Package is installed
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        public static string EvaluateFileName(this string fileName, bool getPrefix = false)
        {
            if(fileName.NotEmpty())
            {
                var names = fileName.Split("_");

                return getPrefix ? names.First() : names.Last();

            }

            return fileName;
        }

    }
}

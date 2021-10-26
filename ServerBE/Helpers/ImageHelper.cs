using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace ServerBE.Helpers
{
    public class ImageHelper
    {
        private const string IMAGE_FOLDER_NAME = "images";
        public static string GetFileUrl(string fileName)
        {
            var url = string.Empty;
            if (string.IsNullOrEmpty(url))
            {
                url = $"{GetImageUrl()}//{fileName}";
            }
            return url;
        }

        public static string GetImageFolderPath()
        {
            return Path.Combine(WebHostEnvironmentHelper.GetWebRootPath(), IMAGE_FOLDER_NAME);
        }

        public static string GetImageUrl()
        {
            return $"{WebHostEnvironmentHelper.GetWebUrl()}//{IMAGE_FOLDER_NAME}";
        }
    }
}

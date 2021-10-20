using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerBE.Services
{
    public interface IFileStorageService
    {
        string GetFileUrl(string fileName);

        Task<string> SaveFileAsync(IFormFile file);

        Task DeleteFileAsync(string fileName);
    }
}

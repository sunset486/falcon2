using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using falcon2.Core.Models.FileUploader;


namespace falcon2.Core.Services
{
    public interface IFileUtilityService
    {
        Task<FileUploadResponse> UploadFiles(List<IFormFile> files);
        Task<IEnumerable<FileDownloadView>> DownloadFiles();
        Task<byte[]> DownloadFile(int id);
        Task<ObjectResult> ViewUploadedSpreadsheet(string fileName);
    }
}

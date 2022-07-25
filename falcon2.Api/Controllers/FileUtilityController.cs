using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml;
using falcon2.Core.Services;
using falcon2.Core.Models.FileUploader;
using falcon2.Api.Helpers;


namespace falcon2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class FileUtilityController : ControllerBase
    {
        IFileUtilityService _fileUtilityService;

        public FileUtilityController(IFileUtilityService fileUtilityService)
        {
            _fileUtilityService = fileUtilityService;
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFile([FromForm] List<IFormFile> files)
        {
            try
            {
                var uploadResponse = await _fileUtilityService.UploadFiles(files);
                if (uploadResponse.ErrorMessage != "")
                    return BadRequest(new { error = uploadResponse.ErrorMessage });
                return Ok(uploadResponse);
            }
            catch (AppException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
           
        }

        [HttpGet("DownloadSingleFile")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            try
            {
                var stream = await _fileUtilityService.DownloadFile(id);
                if (stream == null)
                    return NotFound();
                return new FileContentResult(stream, "application/octet-stream");
            }
            catch (AppException ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [HttpGet("DownloadFiles")]
        public async Task<List<FileDownloadView>> DownloadFiles()
        {
            try
            {
                var files = await _fileUtilityService.DownloadFiles();
                return files.ToList();
            }
            catch(AppException ex)
            {
                throw ex;
            }
        }

        [HttpGet("ViewSpreadsheet")]
        public async Task<IActionResult> ViewSpreadsheet(string spreadsheetName)
        {
            try
            {
                await _fileUtilityService.ViewUploadedSpreadsheet(spreadsheetName);
                return Ok("Spreadsheet loaded!");
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

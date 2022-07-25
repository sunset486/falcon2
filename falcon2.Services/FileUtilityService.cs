using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using falcon2.Core.Models.FileUploader;
using falcon2.Core.Services;
using falcon2.Data;

namespace falcon2.Services
{
    public class FileUtilityService : IFileUtilityService
    {
        public SuperDbContext _sdb;

        public FileUtilityService(SuperDbContext sdb)
        {
            _sdb = sdb;
        }
        public async Task<FileUploadResponse> UploadFiles(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            List<FileUploadResponseData> uploadedFiles = new List<FileUploadResponseData>();

            try
            {
                foreach (var f in files)
                {
                    string name = f.FileName.Replace(@"\\\\", @"\\");
                    if (f.Length > 0)
                    {
                        var memoryStream = new MemoryStream();
                        try
                        {
                            await f.CopyToAsync(memoryStream);

                            if (memoryStream.Length < 8388608)
                            {
                                var file = new UploadedFile()
                                {
                                    FileName = Path.GetFileName(name),
                                    FileSize = memoryStream.Length,
                                    UploadDate = DateTime.Now,
                                    UploadedBy = "admin",
                                    FileContent = memoryStream.ToArray()
                                };

                                _sdb.UploadedFiles.Add(file);
                                await _sdb.SaveChangesAsync();

                                uploadedFiles.Add(new FileUploadResponseData()
                                {
                                    Id = file.Id,
                                    Status = "OK",
                                    FileName = Path.GetFileName(name),
                                    ErrorMessage = ""
                                });
                            }
                            else
                            {
                                uploadedFiles.Add(new FileUploadResponseData()
                                {
                                    Id = 0,
                                    Status = "Error",
                                    FileName = Path.GetFileName(name),
                                    ErrorMessage = "File " + f + " failed to upload"
                                });
                            }
                        }
                        finally
                        {
                            memoryStream.Close();
                            memoryStream.Dispose();
                        }
                    }
                }
                return new FileUploadResponse()
                {
                    Data = uploadedFiles,
                    ErrorMessage = ""
                };
            }
            catch (Exception ex)
            {
                return new FileUploadResponse()
                {
                    ErrorMessage = ex.Message.ToString()
                };
            }
        }

        public async Task<IEnumerable<FileDownloadView>> DownloadFiles()
        {
            IEnumerable<FileDownloadView> downloadFiles = _sdb.UploadedFiles.ToList().Select(f => new FileDownloadView
            {
                Id = f.Id,
                FileName = f.FileName,
                FileSize = f.FileContent.Length
            });
            return downloadFiles;
        }

        public async Task<byte[]> DownloadFile(int id)
        {
            try
            {
                var selectedFile = _sdb.UploadedFiles
                    .Where(f => f.Id == id)
                    .SingleOrDefault();

                if (selectedFile == null)
                    return null;
                return selectedFile.FileContent;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ObjectResult> ViewUploadedSpreadsheet(string filename)
        {
            string pattern = @"^.*\.(xls|xlsx|csv)$";
            try
            {
                var excelFile = _sdb.UploadedFiles
                    .Where(fn => fn.FileName == filename)
                    .SingleOrDefault();

                if(filename == null)
                {
                    throw new BadHttpRequestException("Insert the name of the spreadsheet file! (file_name.xls/.xlsx/.csv)", 400);
                }
                else
                {
                    if(excelFile == null)
                    {
                        throw new BadHttpRequestException("File does not exist", 400);
                    }
                    else
                    {
                        var match = Regex.Match(excelFile.FileName, pattern, RegexOptions.IgnoreCase);
                        if(!match.Success)
                            throw new BadHttpRequestException("File is not in the xls/xlsx/csv format!", 204);
                        else
                        {
                            SpreadsheetLoading(excelFile.FileContent);
                            return new OkObjectResult(excelFile);
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message, 404);
            }
        }

        private static ExcelPackage SpreadsheetLoading(byte[] file)
        {
            using (MemoryStream ms = new MemoryStream(file))
            {
                ExcelPackage package = new ExcelPackage(ms);
                package.LoadAsync(ms);
                byte[] data = package.GetAsByteArray();
                ReadExcel(package);
                return package;
            }
        }

        private static void ReadExcel(ExcelPackage excel)
        {
            ExcelWorksheet ew = excel.Workbook.Worksheets.First();
            int numOfColumns = ew.Dimension.End.Column;
            int numOfRows = ew.Dimension.End.Row;

            for(int i = 1; i<=numOfRows; ++i)
            {
                for(int j = 1; j <= numOfColumns; ++j)
                {
                    Console.WriteLine(" Row:" + i + " column:" + j + " Value:" + ew.Cells[i, j].Value?.ToString().Trim());
                }
            }
        }
    }
}

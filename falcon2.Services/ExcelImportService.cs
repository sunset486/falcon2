using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using falcon2.Core;
using falcon2.Core.Models;
using falcon2.Core.Services;
using falcon2.Data;


namespace falcon2.Services
{
    public class ExcelImportService : IExcelImportService
    {
        public SuperDbContext _sdb;
        private readonly IUnitOfWork _unitOfWork;

        public ExcelImportService(SuperDbContext sdb, IUnitOfWork unitOfWork)
        {
            _sdb = sdb;
            _unitOfWork = unitOfWork;
        }
        public async Task<SuperHero> InsertHeroFromExcel(IFormFile excelFile, string columnName, string columnValue)
        {
            //throw new NotImplementedException();
            SuperHero superHero = new SuperHero();
            if(excelFile?.Length > 0)
            {
                var stream = excelFile.OpenReadStream();
                try
                {
                    using var package = new ExcelPackage(stream);
                    var worksheet = package.Workbook.Worksheets.First();
                    var colNam = worksheet.Cells[1, 2].Value.ToString();
                    if (colNam == columnName)
                    {
                        //var checkValue = worksheet.Cells.GetValue();
                        foreach(var cell in worksheet.Cells)
                        {
                            
                            if (cell.Value.ToString() == columnValue)
                            {
                                var valToInsert = columnValue;
                                
                                if (valToInsert != null)
                                {
                                    superHero.Name = valToInsert;
                                    await _unitOfWork.SuperHeroes.AddAsync(superHero);
                                    await _unitOfWork.CommitAsync();
                                } 
                            }
                            else
                            {
                                throw new BadHttpRequestException("Uploaded spreadsheet does not include the requested value");
                            }
                                
                        }
                    }
                    else
                    {
                        throw new BadHttpRequestException("The column from the uploaded spreadsheet is not valid", 400);
                    }
                }
                catch (Exception ex)
                {
                    throw new BadHttpRequestException(ex.Message, 404);
                }
            }
            return superHero;
        }

        public async Task<SuperPower> InsertPowerFromExcel(IFormFile excelFile, string columnName, string columnValue)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}

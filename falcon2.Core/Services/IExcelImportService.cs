using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using falcon2.Core;
using falcon2.Core.Models;


namespace falcon2.Core.Services
{
    public interface IExcelImportService
    {
        Task<SuperHero> InsertHeroFromExcel(IFormFile excelFile, string columnName, string columnValue);
        Task<SuperPower> InsertPowerFromExcel(IFormFile excelFile, string columnName, string columnValue);
    }
}

using System.Text;
using System.Reflection;
using OfficeOpenXml;
using falcon2.Core.Services;

namespace falcon2.Services
{
    public class SpreadsheetService<T> : ISpreadsheetService<T>
    {
        private IEnumerable<T>? _items;
        private string? _filename;
        private Type? _type;

        public Task GenerateSpreadsheet(IEnumerable<T> items, string filename)
        {
            _items = items;
            _filename = filename;
            _type = typeof(T);

            var rows = new List<string>();
            rows.Add(CreateSpreadsheetHeader());
            foreach (var item in _items)
            {
                rows.Add(CreateRow(item));
            }
            foreach(var row in rows)
            {
                Console.WriteLine(row.ToString());
            }

            return ExcelConversion(rows, _filename);
            
        }
        private string CreateSpreadsheetHeader()
        {
            var properties = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var builder = new StringBuilder();

            foreach (var property in properties)
            {
                builder.Append(property.Name).Append(';');
            }

            return builder.ToString()[..^1];
        }

        private string CreateRow(T item)
        {
            var properties = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var builder = new StringBuilder();

            foreach (var property in properties)
            {
                builder.Append(CreateItem(property.GetValue(item))).Append(';');
            }

            return builder.ToString()[..^1];
        }

        private string CreateItem(object item)
        {
            return item.ToString();
        }

        private Task ExcelConversion(List<string> table, string filename)
        {
            string tempFileName = $"{filename}.csv";
            var tempFile = File.WriteAllLinesAsync(tempFileName, table, Encoding.UTF8);
            string tempFilePath = Path.GetFullPath(tempFileName);
            
            ExcelPackage spreadsheet = new ExcelPackage();
            var sheet = spreadsheet.Workbook.Worksheets.Add("Sheet1");

            var excelFormat = new ExcelTextFormat();
            excelFormat.Delimiter = ';';
            excelFormat.EOL = "\r";
            
            if (tempFile != null)
            {
                sheet.Cells.LoadFromText(new FileInfo(tempFileName), excelFormat, OfficeOpenXml.Table.TableStyles.None, FirstRowIsHeader:true);
                File.Delete(tempFilePath);
            }
            return spreadsheet.SaveAsAsync(new FileInfo(filename + ".xlsx"));
        }
    }
}

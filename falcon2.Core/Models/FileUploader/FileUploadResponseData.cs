using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falcon2.Core.Models.FileUploader
{
    public class FileUploadResponseData
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string FileName { get; set; }
        public string ErrorMessage { get; set; }
    }
}

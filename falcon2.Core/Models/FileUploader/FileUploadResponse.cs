using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falcon2.Core.Models.FileUploader
{
    public class FileUploadResponse
    {
        public string ErrorMessage { get; set; }
        public List<FileUploadResponseData> Data { get; set; }

    }
}

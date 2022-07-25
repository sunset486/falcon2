using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falcon2.Core.Models.FileUploader
{
    public class UploadedFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadedBy { get; set; }
    }
}

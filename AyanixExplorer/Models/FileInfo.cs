using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AyanixExplorer.Models
{
    class FileInfo
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileSize { get; set; }
        public long FileSize_Value { get; set; }
        public string FileExt { get; set; }
        public string FilePath { get; set; }
        public string FileDescription { get; set; }

        public bool File_Hidden { get; set; }
        public bool File_System { get; set; }
        public bool File_ReadOnly { get; set; }

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

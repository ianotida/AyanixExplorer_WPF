using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AyanixExplorer.Models
{
    class LogicalDrives
    {
        public string DriveName { get; set; }
        public string DriveLetter { get; set; }
        public string DriveDescription { get; set; }
        public int DriveType { get; set; }

        public string FileSystem { get; set; }
        public string TotalSize { get; set; }
        public string TotalFree { get; set; }
    }
}

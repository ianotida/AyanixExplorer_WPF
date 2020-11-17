using System;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AyanixExplorer
{
    class SystemWMI
    {
        public static List<Models.LogicalDrives> Get_SysDrives(string sHost)
        {
            List<Models.LogicalDrives> LogDrvs = new List<Models.LogicalDrives>();

            Models.LogicalDrives LogDrv;

            ConnectionOptions Opt = new ConnectionOptions();
            Opt.Impersonation = ImpersonationLevel.Impersonate;

            ManagementScope Mscope = new ManagementScope("\\\\" + sHost + "\\root\\cimv2", Opt);
            Mscope.Connect();

            //try
            //{

            ObjectQuery OQ1 = new ObjectQuery("SELECT * FROM Win32_LogicalDisk");
            ManagementObjectSearcher ObjSch = new ManagementObjectSearcher(Mscope, OQ1);


            foreach (ManagementObject m in ObjSch.Get())
            {
                LogDrv = new Models.LogicalDrives();

                int iDrvType = Convert.ToInt32(m["DriveType"].ToString());

                if (iDrvType <= 3) // DISK  OR FLASH DRIVE
                {
                    LogDrv.DriveName = m["VolumeName"] == null ? "" : m["VolumeName"].ToString();
                }

                if (iDrvType == 4 || iDrvType > 5) // NET DRIVE
                {
                    LogDrv.DriveName = m["ProviderName"] == null ? "" : m["ProviderName"].ToString();
                }

                if (iDrvType == 5) // CD DRIVE 
                {
                    LogDrv.DriveName = m["VolumeName"] == null ? "DVD Drive" : m["VolumeName"].ToString();
                }

                LogDrv.DriveType = iDrvType;
                LogDrv.DriveLetter = m["Caption"].ToString();
                LogDrv.DriveDescription = m["Description"] == null ? "" : m["Description"].ToString();
                LogDrv.FileSystem = m["FileSystem"] == null ? "" : m["FileSystem"].ToString();
                LogDrv.TotalFree = m["FreeSpace"] == null ? "" : m["FreeSpace"].ToString();
                LogDrv.TotalSize = m["Size"] == null ? "" : m["Size"].ToString();

                LogDrvs.Add(LogDrv);
            }

            //}
            //catch (Exception) { }

            return LogDrvs;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;


namespace AyanixExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string ThisPC = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ThisPC = Environment.MachineName;

            Render_TV1();
        }

        private void Render_Files(string sPath)
        {
            lvFiles.Items.Clear();

            if(sPath != "")
            {

                try
                {
                    DirectoryInfo DirInfo = new DirectoryInfo(sPath);
                    FileInfo[] flInfo = DirInfo.GetFiles();

                    foreach (FileInfo fld in flInfo)
                    {
                        lvFiles.Items.Add(fld);
                    }
                }
                catch(Exception x)
                {
                    MessageBox.Show(x.Message, "Error in : " + sPath,MessageBoxButton.OK,MessageBoxImage.Error);
                }

            }
        }



        // ------------------------------------------------------------------------------------------------------------------------------
        // CREATE TREE NODES
        // ------------------------------------------------------------------------------------------------------------------------------
        private void Render_TV1()
        {
            TV.Items.Clear();

            List<Models.LogicalDrives> LogDrives = SystemWMI.Get_SysDrives(".");

            // FIXED ITEMS
            TreeViewItem TVParent = Controls.Tree_Root("PC", ThisPC, "MYPC", Bitmaps.BI_PC);
            TreeViewItem TVInfo = Controls.Tree_Root("PCInfo", "Dashboard", "Dashboard", Bitmaps.BI_Chip);
            TreeViewItem TVLocal = Controls.Tree_Root("PCDisk", "Local Drives", "LocalDisk", Bitmaps.BI_Hdd);
            TreeViewItem TVNet = Controls.Tree_Root("PCNet", "Mapped Drives", "NetDrive", Bitmaps.BI_Net);
            TreeViewItem TVCPL = Controls.Tree_Root("PCCTL", "Control Panel", "Control", Bitmaps.BI_Config);

            TreeViewItem TVNode;
            TreeViewItem TVNode2;


            string sKey = "", sName = "";

            // SYSTEM DRIVES            
            foreach (Models.LogicalDrives Drv in LogDrives)
            {
                TVNode = new TreeViewItem();

                sKey = Drv.DriveLetter.Substring(0, 1);
                sName = Drv.DriveName + " (" + Drv.DriveLetter + ")";

                switch (Drv.DriveType)
                {
                    case 1:
                    case 2:     TVNode = Controls.Tree_Node(sKey, sName, "FD", Bitmaps.BI_PenDrv);  break;
                    case 4:     TVNode = Controls.Tree_Node(sKey, sName, "ND", Bitmaps.BI_Net);     break;
                    case 3:     TVNode = Controls.Tree_Node(sKey, sName, "HD", Bitmaps.BI_Hdd);     break;
                    case 5:     TVNode = Controls.Tree_Node(sKey, sName, "CD", Bitmaps.BI_CD);      break;
                    default:    TVNode = Controls.Tree_Node(sKey, sName, "UD", Bitmaps.BI_Hdd);     break;
                }

                if (Drv.TotalFree != "")
                {
                    DirectoryInfo[] getDir = new DirectoryInfo(Drv.DriveLetter + "\\").GetDirectories();

                    foreach (DirectoryInfo DirInfo in getDir)
                    {
                        TVNode2 = Controls.Tree_Node("FLD" + DirInfo.GetHashCode().ToString(), DirInfo.Name, DirInfo.FullName, Bitmaps.BI_Folder);
                       
                        TVNode2.Selected += TVNode_Selected;
                        TVNode2.Expanded += TVNode_Expanded;

                        TVNode.Items.Add(TVNode2);
                    }

                    TVNode.Expanded += TVNode_Expanded;
                }

                if (Drv.DriveType == 4)
                {
                    TVNet.Items.Add(TVNode);
                }
                else
                {
                    TVLocal.Items.Add(TVNode);
                }
            }

            TVParent.Items.Add(TVInfo);
            TVParent.Items.Add(TVLocal);
            TVParent.Items.Add(TVNet);
            TVParent.Items.Add(TVCPL);

            TV.Items.Add(TVParent);
        }

        
        private void TVNode_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem TVI = (TreeViewItem)sender;

            //if (TVI.Items.Count == 0 && TVI.Name.Substring(0,3) == "FLD")
            //{
            //    TreeViewItem TVNode;

            //    foreach (DirectoryInfo DirInfo in new DirectoryInfo(TVI.Tag.ToString() + "\\").GetDirectories())
            //    {
            //        TVNode = Controls.Tree_Node("FLD" + DirInfo.GetHashCode().ToString(), DirInfo.Name, DirInfo.FullName, Bitmaps.BI_Folder);
            //        TVNode.MouseDoubleClick += TVNode_MouseDoubleClick;
            //        TVNode.Selected += TVNode_Selected;
            //        TVNode.Expanded += TVNode_Expanded;
            //        TVI.Items.Add(TVNode);
            //    }
           // TVI.IsExpanded = true;

            Render_Files(TVI.Tag.ToString());

        }

        private void TVNode_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem NodeTVI = (TreeViewItem)sender;

            foreach(TreeViewItem TVI in NodeTVI.Items)
            {
                if (TVI.Items.Count == 0 && TVI.Name.Substring(0, 3) == "FLD")
                {
                    TreeViewItem TVNode;

                    try
                    {
                        foreach (DirectoryInfo DirInfo in new DirectoryInfo(TVI.Tag.ToString() + "\\").GetDirectories())
                        {
                            TVNode = Controls.Tree_Node("FLD" + DirInfo.GetHashCode().ToString(), DirInfo.Name, DirInfo.FullName, Bitmaps.BI_Folder);
                           
                            TVNode.Selected += TVNode_Selected;
                            TVNode.Expanded += TVNode_Expanded;

                            TVI.Items.Add(TVNode);
                        }
                    }
                    catch (Exception) { }
                }
            }
        }









        private void TVNode_AddDelegates(object sender, TreeViewItem TVNode )
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                TreeViewItem NodeTVI = (TreeViewItem)sender;

                NodeTVI.Items.Add(TVNode);
            }));
        }


    }
}

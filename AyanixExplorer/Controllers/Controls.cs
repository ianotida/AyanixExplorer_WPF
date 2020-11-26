using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AyanixExplorer
{
    class Controls
    {
        // ------------------------------------------------------------------------------------------------------------------------------
        // CUSTOM TREE METHODS
        // ------------------------------------------------------------------------------------------------------------------------------

        public static TreeViewItem Tree_Root(string sKey, string sValue, string sTag, BitmapImage bImg)
        {
            StackPanel SPanel = new StackPanel();
            SPanel.Orientation = Orientation.Horizontal;

            Image ImgF = new Image();
            ImgF.Source = bImg;
            ImgF.Height = 16;

            Label lblName = new Label();
            lblName.Content = sValue;

            SPanel.Children.Add(ImgF);
            SPanel.Children.Add(lblName);

            return new TreeViewItem { Name = sKey, Header = SPanel, Tag = sTag, IsExpanded = true };
        }

        public static TreeViewItem Tree_Node(string sKey, string sValue, string sTag, BitmapImage bImg)
        {
            StackPanel SPanel = new StackPanel();
            SPanel.Orientation = Orientation.Horizontal;

            Image ImgF = new Image();
            ImgF.Source = bImg;
            ImgF.Height = 16;

            Label lblName = new Label();
            lblName.Content = sValue;
            lblName.Padding = new Thickness(5, 2, 0, 2);

            SPanel.Children.Add(ImgF);
            SPanel.Children.Add(lblName);

            return new TreeViewItem { Name = sKey, Header = SPanel, Tag = sTag };
        }

    }


   




}

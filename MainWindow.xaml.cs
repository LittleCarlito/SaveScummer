using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Scummer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<String, String> textBoxList= new Dictionary<string, string>
        {
            {"saveLocation", "" },
            {"targetLocation", "" },
            {"backupLocation", "" }

        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click_X(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click__(object sender, RoutedEventArgs e)
        {
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.WindowState = WindowState.Minimized;
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() => this.WindowStyle = WindowStyle.None));
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = "";
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "No Location";
            }
           else if (textBoxList.ContainsKey(tb.Name))
            {
                textBoxList[tb.Name] = tb.Text;
            }
        }
    }
}

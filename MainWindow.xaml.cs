using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Button = System.Windows.Controls.Button;
using TextBox = System.Windows.Controls.TextBox;

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
        private void Button_Click_BrowserSelect(object sender, RoutedEventArgs e)
        {
            //TODO: Make the browser select display folder location if one is set or default to C:\ if none set
            Button button = (Button)sender;
            String assetName = nameRipper(button.Name, "Button");
            
            if (true)
            {
                if (textBoxList.ContainsKey(assetName))
                {
                    using (FolderBrowserDialog dlg = new FolderBrowserDialog())
                    {
                        textBoxList[assetName] = dlg.SelectedPath;
                        dlg.ShowNewFolderButton = true;
                        DialogResult result = dlg.ShowDialog();
                        if(result == System.Windows.Forms.DialogResult.OK)
                        {
                            textBoxList[assetName] = dlg.SelectedPath;
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException("Asset type not recognized");
                }
            }
        }

        private String nameRipper(String input, String type)
        {
            int trimTo = input.LastIndexOf(type.Substring(0,1));
            return input.Substring(0, trimTo);
        }
    }
}

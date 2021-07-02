using System;
using System.IO;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Forms;
using Button = System.Windows.Controls.Button;
using TextBox = System.Windows.Controls.TextBox;

namespace Scummer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private static String defaultPath = Path.GetPathRoot(Environment.GetEnvironmentVariable("UserProfile"));
        public event PropertyChangedEventHandler PropertyChanged;

        //Refactor these to their own class
        private String _savePath = defaultPath;
        public String savePath
        {
            get { return _savePath; }
            set
            {
                if (_savePath != value)
                {
                    _savePath = value;
                    OnPropertyChanged();
                }
            }
        }
        private String _targetPath = defaultPath;
        public String targetPath
        {
            get { return _targetPath; }
            set
            {
                if (_targetPath != value)
                {
                    _targetPath = value;
                    OnPropertyChanged();
                }
            }
        }
        private String _backupPath = defaultPath;
        public String backupPath
        {
            get { return _backupPath; }
            set
            {
                if (_backupPath != value)
                {
                    _backupPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
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

        private void Button_Click_BrowserSelect(object sender, RoutedEventArgs e)
        {
            //TODO: Make the browser select display folder location if one is set or default to C:\ if none set
            Button button = (Button)sender;
            String assetName = nameRipper(button.Name);
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    if (assetName == "target")
                    {
                        targetPath = dlg.SelectedPath;
                    }
                    else
                    {
                        backupPath = dlg.SelectedPath;
                    }
                }
            }
        }

        private void saveLocationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            String assetName = nameRipper(button.Name);
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.InitialDirectory = savePath;
                DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    savePath = dlg.FileName;
                }
            }
        }

        private String nameRipper(String input)
        {
            int trimTo = input.LastIndexOf("L");
            return input.Substring(0, trimTo);
        }
    }
}

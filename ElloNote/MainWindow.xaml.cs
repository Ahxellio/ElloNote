using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Text;
using System.Drawing.Imaging;
using WPF.ColorPicker;
using WPF.ColorPicker.Code;

namespace ElloNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            fontsComboBox.SelectedIndex = 0;
            valuesComboBox.SelectedIndex = 0;

        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInput_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDrawing_Click(object sender, RoutedEventArgs e)
        {

        }

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Text files (.txt)|*.txt|All files (*.*)|*.*";
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (dlg.ShowDialog() == true)
            {
                FileStream fs = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write);
               
                
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text files (.txt)|*.txt|All files (*.*)|*.*";
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (dlg.ShowDialog() == true)
            {
                FileStream fs = new FileStream(dlg.FileName, FileMode.Open);
            }
        }

        private void ComboBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void fontsComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var fontCollections = new InstalledFontCollection();
            var ff = fontCollections.Families;
            foreach(var font in ff)
            {
                fontsComboBox.Items.Add(font.Name);
            }
        }

        private void valuesComboBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void valuesComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            int[] values = new int[16] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach(var value in values)
            {
                valuesComboBox.Items.Add(value);
            }    
        }

        private void boldTextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void underlinedTextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void obliqueTextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dashbordText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void colorDlgButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void colorChangingButton_Click(object sender, RoutedEventArgs e)
        {
            Color color;
            ColorPickerWindow.ShowDialog(out color);
            SolidColorBrush brush = new SolidColorBrush(color);
            colorChangingButton.Foreground = brush;
            dashbordText.Foreground = brush;
        }
    }
}

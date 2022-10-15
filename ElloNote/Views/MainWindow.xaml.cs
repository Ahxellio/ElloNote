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

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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

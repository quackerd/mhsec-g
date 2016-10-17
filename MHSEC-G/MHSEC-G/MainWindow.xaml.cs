using System;
using System.Collections.Generic;
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

namespace MHSEC_G
{
    public partial class MainWindow : Window
    {
        private const string Version = "0.1";
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "MHSEC-G Ver" + Version;
        }

        private void button_load_click(object sender, RoutedEventArgs e)
        {
           
//            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
//            {
//                System.IO.StreamReader sr = new
//                   System.IO.StreamReader(openFileDialog1.FileName);
//                MessageBox.Show(sr.ReadToEnd());
//                sr.Close();
//            }
        }
    }
}

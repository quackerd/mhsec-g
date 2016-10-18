using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace MHSEC_G
{
    public partial class MainWindow : Window
    {
        private readonly byte[] dummy_data = new byte[Model.SAVE_FILE_SIZE];
        private const string Version = "0.1";
        public MainWindow()
        {
            InitializeComponent();
            Array.Clear(dummy_data, 0, dummy_data.Length);
            this.Title = "MHSEC-G Ver" + Version;
            DataContext = new ViewModel(dummy_data);
        }

        private void button_load_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "MHST Save files|mhr_game0.sav|sav files (*.sav)|*.sav|All files (*.*)|*.*";
            dialog.Title = "Please select your save file.";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] buffer = File.ReadAllBytes(dialog.FileName);
                if (buffer.Length != Model.SAVE_FILE_SIZE)
                {
                    System.Windows.Forms.MessageBox.Show(
                        "Wrong save file size! Expected: " + Model.SAVE_FILE_SIZE + ".",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
                }
                else
                {
                    DataContext = new ViewModel(buffer);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using MHSEC_G.Annotations;
using MessageBox = System.Windows.MessageBox;

namespace MHSEC_G
{
    /// <summary>
    /// Interaction logic for MonsterWindow.xaml
    /// </summary>
    public partial class MonsterWindow : Window, INotifyPropertyChanged
    {
        private readonly Monster _monster;
        public Monster monster => _monster;

        public MonsterWindow(Monster monster)
        {
            this._monster = monster;
            DataContext = this;
            InitializeComponent();
        }

        private void button_mimport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
            dialog.Title = "Please select the monster file.";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] buffer = File.ReadAllBytes(dialog.FileName);
                if (buffer.Length != Offsets.SIZE_MONSTER)
                {
                    System.Windows.Forms.MessageBox.Show(
                        "Wrong monster file size!",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    _monster.setByteArray(buffer);
                }
            }
        }

        private void button_mdelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirm = MessageBox.Show("Are you sure you want to delete the monster?", "MHSEC-G", MessageBoxButton.YesNo);
            if (confirm == MessageBoxResult.Yes)
            {
                _monster.setByteArray(Offsets.MONSTER_NULL_TEMPLATE);
            }
        }

        private void button_mdel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
            dialog.Title = "Please select the export location.";
            dialog.FileName = _monster.name + ".bin";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] binary = _monster.toByteArray();
                File.WriteAllBytes(dialog.FileName, binary);
                MessageBox.Show("Exported to \"" + dialog.FileName + "\"", "MHSEC-G", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void button_mexp_Click(object sender, RoutedEventArgs e)
        {
            _monster.exp = Offsets.LIMIT_MONSTER_EXP;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void button_mdel_genes_Click(object sender, RoutedEventArgs e)
        {
            GeneWindow geneWnd = new GeneWindow(_monster.genes);
            geneWnd.Show();
        }
    }
}

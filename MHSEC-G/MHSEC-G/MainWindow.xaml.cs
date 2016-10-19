using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace MHSEC_G
{
    public partial class MainWindow : Window
    {
        private ViewModel view_model;
        private readonly byte[] dummy_data = new byte[Model.SAVE_FILE_SIZE];
        private const string Version = "0.1";
        public MainWindow()
        {
            InitializeComponent();
            Item.read_item_mappings();
            Array.Clear(dummy_data, 0, dummy_data.Length);
            this.Title = "MHSEC-G Ver" + Version;
            view_model = new ViewModel(dummy_data);
            DataContext = view_model;
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
                    view_model = new ViewModel(buffer);
                    DataContext = view_model;
                }
            }
        }

        private void button_char_money_Click(object sender, RoutedEventArgs e)
        {
            view_model.character.money = Character.LIMIT_MONEY;
        }

        private void button_char_exp_Click(object sender, RoutedEventArgs e)
        {
            view_model.character.exp = Character.LIMIT_EXP;
        }

        private void button_item_all_Click(object sender, RoutedEventArgs e)
        {
            List<Item> items = view_model.items;
            for (uint i = 0; i < items.Count; i++)
            {
                if (items.ElementAt((int)i).id == 1227)
                    break;

                items.ElementAt((int)i).count = 986;
            }
        }

        private void button_item_existing_Click(object sender, RoutedEventArgs e)
        {
            List<Item> items = view_model.items;
            for (uint i = 0; i < items.Count; i++)
            {
                if (items.ElementAt((int) i).id == 1227)
                    break;

                if (items.ElementAt((int)i).count != 0)
                {
                    items.ElementAt((int) i).count = 999;
                }
            }
        }

        private void button_mexp_Click(object sender, RoutedEventArgs e)
        {
            view_model.cur_monster.exp = Monster.LIMIT_MONSTER_EXP;
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllBytes("C:\\Users\\hyper\\Desktop\\mhr_save0.hacked", view_model.model.save_file);
            MessageBox.Show("Saved to \"mhr_save0.hacked\"","MHSEC-G",MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

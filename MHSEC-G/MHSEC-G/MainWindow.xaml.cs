using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace MHSEC_G
{
    public partial class MainWindow : Window
    {
        private ViewModel view_model;
        private readonly byte[] dummy_data = new byte[Model.SAVE_FILE_SIZE];

        private static string get_app_version()
        {
            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            string version = v.Major+ "." + v.Minor + v.Build;
            return version;
        }

        public MainWindow()
        {
            InitializeComponent();
            button_save.IsEnabled = false;
            Item.read_item_mappings();
            Monster.read_gene_mapping();
            Array.Clear(dummy_data, 0, dummy_data.Length);
            this.Title = "MHSEC-G Ver " + get_app_version();
            view_model = new ViewModel(dummy_data);
            DataContext = view_model;
        }

        private void button_load_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "MHST Save file|mhr_game0.sav|SAV files (*.sav)|*.sav|All files (*.*)|*.*";
            dialog.Title = "Please select your save file.";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] buffer = File.ReadAllBytes(dialog.FileName);
                if (buffer.Length != Model.SAVE_FILE_SIZE)
                {
                    System.Windows.Forms.MessageBox.Show(
                        "Wrong save file size! Expected: " + Model.SAVE_FILE_SIZE + " Got: " + buffer.Length,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    view_model = new ViewModel(buffer);
                    DataContext = view_model;
                    button_save.IsEnabled = true;
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
                if (items.ElementAt((int) i).offset >= Item.OFFSETA_FIRST_KEY_ITEM)
                    break;

                items.ElementAt((int) i).count = 986;
            }
        }

        private void button_item_existing_Click(object sender, RoutedEventArgs e)
        {
            List<Item> items = view_model.items;
            for (uint i = 0; i < items.Count; i++)
            {
                if (items.ElementAt((int) i).offset >= Item.OFFSETA_FIRST_KEY_ITEM)
                    break;

                if (items.ElementAt((int) i).count != 0)
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
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "SAV files (*.sav)|*.sav|All files (*.*)|*.*";
            dialog.Title = "Please select the save location.";
            dialog.FileName = "mhr_game0.sav";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllBytes(dialog.FileName, view_model.model.save_file);
                MessageBox.Show("Saved to \"" + dialog.FileName + "\"", "MHSEC-G", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void button_give_epony_Click(object sender, RoutedEventArgs e)
        {
            EggFragment.write_dlc_egg_fragment(view_model.egg_fragments, view_model.model, 0x4);
        }

        private void button_give_bear_Click(object sender, RoutedEventArgs e)
        {
            EggFragment.write_dlc_egg_fragment(view_model.egg_fragments, view_model.model, 0x5);
        }

        private void button_give_mtiggy_Click(object sender, RoutedEventArgs e)
        {
            EggFragment.write_dlc_egg_fragment(view_model.egg_fragments, view_model.model, 0x20);
        }

        private void button_give_okirin_Click(object sender, RoutedEventArgs e)
        {
            EggFragment.write_dlc_egg_fragment(view_model.egg_fragments, view_model.model, 0x21);
        }

        private void button_give_dino_Click(object sender, RoutedEventArgs e)
        {
            EggFragment.write_dlc_egg_fragment(view_model.egg_fragments, view_model.model, 0x6);
        }

        private void button_give_wm_Click(object sender, RoutedEventArgs e)
        {
            EggFragment.write_dlc_egg_fragment(view_model.egg_fragments, view_model.model, 0x1F);
        }

        private void button_give_pd_Click(object sender, RoutedEventArgs e)
        {
            EggFragment.write_dlc_egg_fragment(view_model.egg_fragments, view_model.model, 0x3);
        }

        private void button_about_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MHSEC-G Version " + get_app_version() +"\nDeveloped by secXsQuared", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void button_mdel_Click(object sender, RoutedEventArgs e)
        {
            if (view_model.monsters.Count <= 1)
            {
                MessageBox.Show("Cannot delete the last monster.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Monster to_delete = view_model.cur_monster;
            view_model.monsters.Remove(to_delete);
            view_model.cur_monster = view_model.monsters.ElementAt(0);

            byte[] template = Properties.Resources.monster_null_template;
            Array.Copy(template, 0, view_model.model.save_file, to_delete.offset, Monster.SIZE_MONSTER);
        }
    }
}

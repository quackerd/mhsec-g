using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MHSEC_G.Annotations;
using MessageBox = System.Windows.MessageBox;

namespace MHSEC_G
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private static string get_app_version()
        {
            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            string version = v.Major + "." + v.Minor + v.Build;
            return version;
        }

        public MainWindow()
        {
            InitializeComponent();
            button_save.IsEnabled = false;
            byte[] dummy = new byte[Offsets.SAVE_FILE_SIZE_JPN];
            Array.Clear(dummy, 0, dummy.Length);
            this.Title = "MHSEC-G Ver " + get_app_version();

            Offsets.init(dummy);
            init(dummy);

            DataContext = this;
        }

        private void button_load_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "MHST Save file|mhr_game0.sav|SAV files (*.sav)|*.sav|All files (*.*)|*.*";
            dialog.Title = "Please select your save file.";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] buffer = File.ReadAllBytes(dialog.FileName);
                if (buffer.Length != Offsets.SAVE_FILE_SIZE_JPN && buffer.Length != Offsets.SAVE_FILE_SIZE_NA)
                {
                    System.Windows.Forms.MessageBox.Show(
                        "Unsupported save file!",
                        "MHSEC-G",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    if (buffer.Length == Offsets.SAVE_FILE_SIZE_JPN)
                    {
                        label_save_ver.Content = "JPN";
                    }
                    else
                    {
                        label_save_ver.Content = "EUR/USA";
                    }
                    Offsets.init(buffer);
                    init(buffer);
                    OnPropertyChanged(null);
                    button_save.IsEnabled = true;
                }
            }
        }

        private void button_char_money_Click(object sender, RoutedEventArgs e)
        {
            _character.money = Offsets.LIMIT_MONEY;
        }

        private void button_char_exp_Click(object sender, RoutedEventArgs e)
        {
            _character.exp = Offsets.LIMIT_EXP;
        }

        private void button_item_all_Click(object sender, RoutedEventArgs e)
        {
            List<Item> items = _items;
            for (uint i = 0; i < items.Count; i++)
            {
                if (items.ElementAt((int) i).offset >= Offsets.OFFSETA_FIRST_KEY_ITEM)
                    break;

                items.ElementAt((int) i).count = 986;
            }
        }

        private void button_item_existing_Click(object sender, RoutedEventArgs e)
        {
            List<Item> items = _items;
            for (uint i = 0; i < items.Count; i++)
            {
                if (items.ElementAt((int) i).offset >= Offsets.OFFSETA_FIRST_KEY_ITEM)
                    break;

                if (items.ElementAt((int) i).count != 0)
                {
                    items.ElementAt((int) i).count = 999;
                }
            }
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "SAV files (*.sav)|*.sav|All files (*.*)|*.*";
            dialog.Title = "Please select the save location.";
            dialog.FileName = "mhr_game0.sav";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllBytes(dialog.FileName, _model);
                MessageBox.Show("Saved to \"" + dialog.FileName + "\"", "MHSEC-G", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void button_about_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MHSEC-G Version " + get_app_version() + "\nDeveloped by secXsQuared", "About",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private byte[] _model;
        public byte[] model => _model;

        private Character _character;
        public Character character => _character;

        private ObservableCollection<Armor> _armors;
        public ObservableCollection<Armor> armors => _armors;

        private ObservableCollection<Monster> _monsters;
        public ObservableCollection<Monster> monsters => _monsters;

        private ObservableCollection<Talisman> _talismans;
        public ObservableCollection<Talisman> talismans => _talismans;

        private ObservableCollection<Egg> _eggs;
        public ObservableCollection<Egg> eggs => _eggs;

        private ObservableCollection<Weapon> _weapons;
        public ObservableCollection<Weapon> weapons => _weapons;

        private List<Item> _items;
        public List<Item> items => _items;

        private ObservableCollection<EggFragment> _egg_fragments;
        public ObservableCollection<EggFragment> egg_fragments => _egg_fragments;

        public void init(byte[] save)
        {
            if (save == null)
            {
                BugCheck.bug_check(BugCheck.ErrorCode.VIEWMODEL_NULL_SAVE, "The save file reference is NULL.");
            }
            _model = save;
            _character = new Character(_model);
            _items = Item.read_all_items(_model);
            _monsters = new ObservableCollection<Monster>(Monster.read_all_monsters(_model));
            _egg_fragments = EggFragment.read_all_egg_fragments(_model);
            _talismans = Talisman.read_all_talismans(_model);
            _weapons = Weapon.read_all_weapons(_model);
            _armors = Armor.read_all_armors(_model);
            _eggs = Egg.read_all_eggs(_model);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Monster_grid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGridRow row = sender as DataGridRow;
                if (row != null)
                {
                    MonsterWindow monsterWnd = new MonsterWindow(row.DataContext as Monster);
                    monsterWnd.Show();
                }
            }
        }

        private void Egg_grid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGridRow row = sender as DataGridRow;
                if (row != null)
                {
                    GeneWindow geneWnd = new GeneWindow((row.DataContext as Egg).genes);
                    geneWnd.Show();
                }
            }
        }

        private void button_eggs_dummy_Click(object sender, RoutedEventArgs e)
        {
            if (this.egg_grid.SelectedItem == null)
            {
                MessageBox.Show("Please select an egg.", "MHSEC-G", MessageBoxButton.OK);
            }
            else
            {
                (this.egg_grid.SelectedItem as Egg).setByteArray(Properties.Resources.egg_dummy_template);
            }
        }

        private void button_eggs_delete_Click(object sender, RoutedEventArgs e)
        {
            if (this.egg_grid.SelectedItem == null)
            {
                MessageBox.Show("Please select an egg.", "MHSEC-G", MessageBoxButton.OK);
            }
            else
            {
                MessageBoxResult confirm = MessageBox.Show("Are you sure you want to delete the egg?", "MHSEC-G",
                    MessageBoxButton.YesNo);
                if (confirm == MessageBoxResult.Yes)
                {
                    (this.egg_grid.SelectedItem as Egg).setByteArray(Properties.Resources.egg_null_template);
                }
            }
        }
    }
}
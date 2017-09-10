using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public partial class PartyWindow : Window, INotifyPropertyChanged
    {
        private readonly byte[] _model;

        public PartyWindow(byte[] model)
        {
            _model = model;
            InitializeComponent();
            DataContext = this;
        }

        private void set_mon_by_id(string value, uint id)
        {
            uint parsed;
            if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
            {
                Helper.write_uint16_le(_model, Offsets.OFFSETA_MONSTER_PARTY + id * 4, parsed);
                OnPropertyChanged(null);
            }
            else
            {
                MessageBox.Show("UID must be at most 0xFFFF", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private string get_mon_by_id(uint id)
        {
            return Helper.byte_to_uint16_le(_model, Offsets.OFFSETA_MONSTER_PARTY + id * 4).ToString("X4");
        }

        public string mon1
        {
            get => get_mon_by_id(0);
            set { set_mon_by_id(value, 0); }
        }

        public string mon2
        {
            get => get_mon_by_id(1);
            set { set_mon_by_id(value, 1); }
        }

        public string mon3
        {
            get => get_mon_by_id(2);
            set { set_mon_by_id(value, 2); }
        }

        public string mon4
        {
            get => get_mon_by_id(3);
            set { set_mon_by_id(value, 3); }
        }

        public string mon5
        {
            get => get_mon_by_id(4);
            set { set_mon_by_id(value, 4); }
        }

        public string mon6
        {
            get => get_mon_by_id(5);
            set { set_mon_by_id(value, 5); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

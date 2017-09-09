using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class Weapon : INotifyPropertyChanged
    {
        private const int OFFSETA_WEAPON_START = 0x39D0;
        private const int OFFSETA_WEAPON_END = 0x55EF;
        // 200 weapons
        private const int SIZE_WEAPON = 0x24;

        private const int OFFSETR_CLASS = 0x0;
        private const int OFFSETR_ID = 0x2;
        private const int OFFSETR_LEVEL = 0x4;

        private readonly uint _offset;
        private readonly Model _model;
        public uint index => (_offset - OFFSETA_WEAPON_START) / SIZE_WEAPON + 1;

        public Weapon(Model model, uint offset)
        {
            _offset = offset;
            _model = model;
        }

        public string clazz
        {
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Model.write_uint16_le(_model.save_file, _offset + OFFSETR_CLASS, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed Weapon Class - must be at most 0xFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(clazz));
            }
            get
            {
                return Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_CLASS).ToString("X4");
            }
        }

        public string id
        {
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Model.write_uint16_le(_model.save_file, _offset + OFFSETR_ID, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed ID - must be at most 0xFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(id));
            }
            get
            {
                return Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_ID).ToString("X4");
            }
        }


        public string level
        {
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Model.write_uint16_le(_model.save_file, _offset + OFFSETR_LEVEL, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed level - must be at most 0xFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(level));
            }
            get
            {
                return Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_LEVEL).ToString("X4");
            }
        }

        public static ObservableCollection<Weapon> read_all_weapons(Model model)
        {
            ObservableCollection<Weapon> ret = new ObservableCollection<Weapon>();
            for (uint i = OFFSETA_WEAPON_START; i < OFFSETA_WEAPON_END; i += SIZE_WEAPON)
            {
                ret.Add(new Weapon(model, i));
            }
            return ret;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

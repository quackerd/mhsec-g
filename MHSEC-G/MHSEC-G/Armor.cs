using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class Armor : INotifyPropertyChanged
    {
        private const uint OFFSETA_ARM = 0x55F0;
        private const uint OFFSETA_ARM_END = 0x720E;
        // 200 armors
        private const uint SIZE_ARM = 0x24;
        private const uint OFFSETR_ARM_ID = 0x2;
        private const uint OFFSETR_ARM_LEVEL = 0x4;
        private const uint OFFSETR_ARM_14h = 0x14;
        private const uint OFFSETR_ARM_18h = 0x18;
        private const uint OFFSETR_ARM_1C = 0x1c;

        private readonly uint _offset;
        private readonly Model _model;
        public uint index => (_offset - OFFSETA_ARM) / SIZE_ARM + 1;

        public Armor(Model model, uint offset)
        {
            _offset = offset;
            _model = model;
        }

        public string id
        {
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Model.write_uint16_le(_model.save_file, _offset + OFFSETR_ARM_ID, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed int - must be at most 0xFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(id));
            }
            get
            {
                return Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_ARM_ID).ToString("X4");
            }
        }

        public string level
        {
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Model.write_uint16_le(_model.save_file, _offset + OFFSETR_ARM_LEVEL, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed int - must be at most 0xFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(level));
            }
            get
            {
                return Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_ARM_LEVEL).ToString("X4");
            }
        }


        public string unknown_14h
        {
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed))
                {
                    Model.write_uint32_le(_model.save_file, _offset + OFFSETR_ARM_14h, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed int - must be at most 0xFFFFFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(unknown_14h));
            }
            get
            {
                return Model.byte_to_uint32_le(_model.save_file, _offset + OFFSETR_ARM_14h).ToString("X8");
            }
        }

        public string unknown_18h
        {
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed))
                {
                    Model.write_uint32_le(_model.save_file, _offset + OFFSETR_ARM_18h, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed int - must be at most 0xFFFFFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(unknown_18h));
            }
            get
            {
                return Model.byte_to_uint32_le(_model.save_file, _offset + OFFSETR_ARM_18h).ToString("X8");
            }
        }

        public string unknown_1ch
        {
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed))
                {
                    Model.write_uint32_le(_model.save_file, _offset + OFFSETR_ARM_1C, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed int - must be at most 0xFFFFFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(unknown_1ch));
            }
            get
            {
                return Model.byte_to_uint32_le(_model.save_file, _offset + OFFSETR_ARM_1C).ToString("X8");
            }
        }

        public static ObservableCollection<Armor> read_all_armors(Model model)
        {
            ObservableCollection<Armor> ret = new ObservableCollection<Armor>();
            for (uint i = OFFSETA_ARM; i < OFFSETA_ARM_END; i += SIZE_ARM)
            {
                ret.Add(new Armor(model, i));
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

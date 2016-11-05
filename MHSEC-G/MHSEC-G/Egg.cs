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
    internal class Egg : INotifyPropertyChanged
    {
        private const int OFFSETA_EGG_START = 0x53EC0;
        private const int OFFSETA_EGG_END = 0x54505;
        // 200 weapons
        private const int SIZE_EGG = 0x92;

        private const int OFFSETR_SPE = 0x0;
        private const int OFFSETR_WGT = 0x2E;

        private readonly uint _offset;
        private readonly Model _model;

        public Egg(Model model, uint offset)
        {
            _offset = offset;
            _model = model;
        }


        public string spe
        {
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Model.write_byte(_model.save_file, _offset + OFFSETR_SPE, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed int - must be at most 0xFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(spe));
            }
            get
            {
                return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_SPE]).ToString("X2");
            }
        }

        public string wgt
        {
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Model.write_byte(_model.save_file, _offset + OFFSETR_WGT, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed int - must be at most 0xFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(wgt));
            }
            get
            {
                return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_WGT]).ToString("X2");
            }
        }

        public static ObservableCollection<Egg> read_all_eggs(Model model)
        {
            ObservableCollection<Egg> ret = new ObservableCollection<Egg>();
            for (uint i = OFFSETA_EGG_START; i < OFFSETA_EGG_END; i += SIZE_EGG)
            {
                if (Model.byte_to_uint(model.save_file[i + OFFSETR_SPE]) == 0xFF)
                {
                    continue;
                }
                ret.Add(new Egg(model, i));
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

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class Egg : INotifyPropertyChanged
    {
        private const int OFFSETA_EGG_START = 0x53EC0;
        private const int OFFSETA_EGG_END = 0x54597;
        private const int OFFSETR_EGG_GENE = 0x30;
        private const int SIZE_EGG_GENE = 0x2;
        private const int SIZE_EGG = 0x92;
        private const int OFFSETR_SPE = 0x0;
        private const int OFFSETR_WGT = 0x2E;

        private readonly Genes _genes;
        public Genes genes => _genes;

        private readonly uint _offset;
        private readonly Model _model;
        public uint index => (_offset - OFFSETA_EGG_START) / SIZE_EGG + 1;

        public Egg(Model model, uint offset)
        {
            _offset = offset;
            _model = model;
            _genes = new Genes(model, offset + OFFSETR_EGG_GENE, SIZE_EGG_GENE);
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
                ret.Add(new Egg(model, i));
            }
            return ret;
        }

    
        public void setByteArray(byte[] ret)
        {
            Array.Copy(ret, 0, _model.save_file, _offset, SIZE_EGG);
            OnPropertyChanged(null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

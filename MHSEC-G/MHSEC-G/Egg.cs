using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class Egg : InMemoryObject
    {

        private readonly Genes _genes;
        public Genes genes => _genes;

        public uint index => (_obj_offset - Offsets.OFFSETA_EGG_START) / Offsets.SIZE_EGG + 1;

        public Egg(byte[] model, uint objOffset) : base(model, objOffset, Offsets.SIZE_EGG)
        {
            _genes = new Genes(model, objOffset + Offsets.OFFSETR_EGG_GENE, Offsets.SIZE_EGG_GENE);
        }

        public string spe
        {
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Helper.write_byte(_data, _obj_offset + Offsets.OFFSETR_SPE, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed int - must be at most 0xFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(spe));
            }
            get
            {
                return Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_SPE]).ToString("X2");
            }
        }

        public string wgt
        {
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Helper.write_byte(_data, _obj_offset + Offsets.OFFSETR_WGT, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed int - must be at most 0xFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(wgt));
            }
            get
            {
                return Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_WGT]).ToString("X2");
            }
        }

        public static ObservableCollection<Egg> read_all_eggs(byte[] model)
        {
            ObservableCollection<Egg> ret = new ObservableCollection<Egg>();
            for (uint i = Offsets.OFFSETA_EGG_START; i < Offsets.OFFSETA_EGG_END; i += Offsets.SIZE_EGG)
            {
                ret.Add(new Egg(model, i));
            }
            return ret;
        }
    }
}

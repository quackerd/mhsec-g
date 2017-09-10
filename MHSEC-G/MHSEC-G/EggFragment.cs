using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class EggFragment : InMemoryObject
    {
        public uint idx => (_obj_offset - Offsets.OFFSETA_EGG_FRAGMENTS) / Offsets.SIZE_EGG_FRAGMENT + 1;

        public EggFragment(byte[] model, uint objOffset) : base(model, objOffset, Offsets.SIZE_EGG_FRAGMENT)
        {
        }

        public string spe
        {
            get { return Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_EF_SPE]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0x0E)
                {
                    Helper.write_byte(_data, _obj_offset + Offsets.OFFSETR_EF_SPE, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed Species value - must be at most 0xE", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(spe));
            }
        }

        public string pos
        {
            get { return Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_EF_POS]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0x08)
                {
                    Helper.write_byte(_data, _obj_offset + Offsets.OFFSETR_EF_POS, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed Position value - must be at most 0x8", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(pos));
            }
        }

        public string new_flag
        {
            get { return Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_EF_NEW]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0x01)
                {
                    Helper.write_byte(_data, _obj_offset + Offsets.OFFSETR_EF_NEW, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed New value - must be 0 or 1", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(new_flag));
            }
        }
        public string rarity
        {
            get { return Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_EF_RAR]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0x01)
                {
                    Helper.write_byte(_data, _obj_offset + Offsets.OFFSETR_EF_RAR, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed Rarity value - must be 0 or 1", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(rarity));
            }
        }

        public string color
        {
            get { return Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_EF_COL]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Helper.write_byte(_data, _obj_offset + Offsets.OFFSETR_EF_COL, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed Species value - must be at most 0xFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(color));
            }
        }

        public string dlc
        {
            get { return Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_EF_DLC]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Helper.write_byte(_data, _obj_offset + Offsets.OFFSETR_EF_DLC, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed DLC value - must be at most 0xFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(dlc));
            }
        }

        public string unknown_6h
        {
            get { return Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_EF_6H]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Helper.write_byte(_data, _obj_offset + Offsets.OFFSETR_EF_6H, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed Species value - must be at most 0xFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(unknown_6h));
            }
        }

        public string unknown_7h
        {
            get { return Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_EF_7H]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Helper.write_byte(_data, _obj_offset + Offsets.OFFSETR_EF_7H, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed 7h value - must be at most 0xFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(unknown_7h));
            }
        }

        public static ObservableCollection<EggFragment> read_all_egg_fragments(byte[] model)
        {
            ObservableCollection<EggFragment> ret = new ObservableCollection<EggFragment>();
            for (uint offset = Offsets.OFFSETA_EGG_FRAGMENTS; offset < Offsets.OFFSETA_EGG_FRAGMENTS_END; offset += Offsets.SIZE_EGG_FRAGMENT)
            {
                ret.Add(new EggFragment(model, offset));
            }
            return ret;
        }
    }
}

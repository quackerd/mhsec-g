using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class Talisman : InMemoryObject
    {
        public uint index => (_obj_offset - Offsets.OFFSETA_TALI) / Offsets.SIZE_TALI + 1;
        

        public Talisman(byte[] model, uint objOffset) : base(model, objOffset, Offsets.SIZE_TALI)
        {
        }

        public string rarity
        {
            get { return Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_TALI_RARITY]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Helper.write_byte(_data, _obj_offset + Offsets.OFFSETR_TALI_RARITY, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed Rarity - must be at most 0xFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(rarity));
            }
        }

        public string id
        {
            get { return Helper.byte_to_uint16_le(_data, _obj_offset + Offsets.OFFSETR_TALI_ID).ToString("X4"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Helper.write_uint16_le(_data, _obj_offset + Offsets.OFFSETR_TALI_ID, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed Talisman ID - must be at most 0xFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(id));
            }
        }

        public string new_flag
        {
            get { return Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_TALI_NEW]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0x1)
                {
                    Helper.write_byte(_data, _obj_offset + Offsets.OFFSETR_TALI_NEW, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed New flag - must be at most 0x1", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(new_flag));
            }
        }
        public string skill1
        {
            get { return Helper.byte_to_uint16_le(_data, _obj_offset + Offsets.OFFSETR_TALI_SKILL1).ToString("X4"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Helper.write_uint16_le(_data, _obj_offset + Offsets.OFFSETR_TALI_SKILL1, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed Skill 1 - must be at most 0xFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(skill1));
            }
        }

        public string skill2
        {
            get { return Helper.byte_to_uint16_le(_data, _obj_offset + Offsets.OFFSETR_TALI_SKILL2).ToString("X4"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Helper.write_uint16_le(_data, _obj_offset + Offsets.OFFSETR_TALI_SKILL2, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed Skill 2 - must be at most 0xFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(skill2));
            }
        }

        public string equipped
        {
            get { return Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_EQUIPPED]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Helper.write_byte(_data, _obj_offset + Offsets.OFFSETR_EQUIPPED, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed Equipped value - must be at most 0xFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(equipped));
            }
        }
        
        public static ObservableCollection<Talisman> read_all_talismans(byte[] model)
        {
            ObservableCollection<Talisman> ret = new ObservableCollection<Talisman>();
            for (uint offset = Offsets.OFFSETA_TALI; offset < Offsets.OFFSETA_TALI_END; offset += Offsets.SIZE_TALI)
            {
                ret.Add(new Talisman(model, offset));
            }
            return ret;
        }
    }
}


using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class Weapon : InMemoryObject
    {
        public uint index => (_obj_offset - Offsets.OFFSETA_WEAPON_START) / Offsets.SIZE_WEAPON + 1;

        public Weapon(byte[] model, uint objOffset) : base(model, objOffset, Offsets.SIZE_WEAPON)
        {
        }

        public string clazz
        {
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Helper.write_uint16_le(_data, _obj_offset + Offsets.OFFSETR_CLASS, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed Weapon Class - must be at most 0xFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(clazz));
            }
            get
            {
                return Helper.byte_to_uint16_le(_data, _obj_offset + Offsets.OFFSETR_CLASS).ToString("X4");
            }
        }

        public string equipped
        {
            get { return Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_WEAPON_EQUIPPED]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Helper.write_byte(_data, _obj_offset + Offsets.OFFSETR_WEAPON_EQUIPPED, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed Equipped value - must be at most 0xFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(equipped));
            }
        }

        public string id
        {
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Helper.write_uint16_le(_data, _obj_offset + Offsets.OFFSETR_ID, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed ID - must be at most 0xFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(id));
            }
            get
            {
                return Helper.byte_to_uint16_le(_data, _obj_offset + Offsets.OFFSETR_ID).ToString("X4");
            }
        }


        public string level
        {
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Helper.write_uint16_le(_data, _obj_offset + Offsets.OFFSETR_LEVEL, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed level - must be at most 0xFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(level));
            }
            get
            {
                return Helper.byte_to_uint16_le(_data, _obj_offset + Offsets.OFFSETR_LEVEL).ToString("X4");
            }
        }

        public static ObservableCollection<Weapon> read_all_weapons(byte[] model)
        {
            ObservableCollection<Weapon> ret = new ObservableCollection<Weapon>();
            for (uint i = Offsets.OFFSETA_WEAPON_START; i < Offsets.OFFSETA_WEAPON_END; i += Offsets.SIZE_WEAPON)
            {
                ret.Add(new Weapon(model, i));
            }
            return ret;
        }
    }
}

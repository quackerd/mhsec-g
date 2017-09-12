using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using MHSEC_G.Annotations;
using Microsoft.Win32;

namespace MHSEC_G
{
    public class Armor : InMemoryObject
    {
        public uint index => (_obj_offset - Offsets.OFFSETA_ARM) / Offsets.SIZE_ARM + 1;

        public Armor(byte[] save, uint objOffset) : base(save, objOffset, Offsets.SIZE_ARM)
        {
            
        }

        public string type
        {
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Helper.write_uint16_le(_data, _obj_offset + Offsets.OFFSETR_ARM_TYPE, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed int - must be at most 0xFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(type));
            }
            get
            {
                return Helper.byte_to_uint16_le(_data, _obj_offset + Offsets.OFFSETR_ARM_TYPE).ToString("X4");
            }
        }

        public string id
        {
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Helper.write_uint16_le(_data, _obj_offset + Offsets.OFFSETR_ARM_ID, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed int - must be at most 0xFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(id));
            }
            get
            {
                return Helper.byte_to_uint16_le(_data, _obj_offset + Offsets.OFFSETR_ARM_ID).ToString("X4");
            }
        }

        public string level
        {
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Helper.write_uint16_le(_data, _obj_offset + Offsets.OFFSETR_ARM_LEVEL, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed int - must be at most 0xFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(level));
            }
            get
            {
                return Helper.byte_to_uint16_le(_data, _obj_offset + Offsets.OFFSETR_ARM_LEVEL).ToString("X4");
            }
        }


        public string unknown_14h
        {
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed))
                {
                    Helper.write_uint32_le(_data, _obj_offset + Offsets.OFFSETR_ARM_14h, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed int - must be at most 0xFFFFFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(unknown_14h));
            }
            get
            {
                return Helper.byte_to_uint32_le(_data, _obj_offset + Offsets.OFFSETR_ARM_14h).ToString("X8");
            }
        }

        public string unknown_18h
        {
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed))
                {
                    Helper.write_uint32_le(_data, _obj_offset + Offsets.OFFSETR_ARM_18h, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed int - must be at most 0xFFFFFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(unknown_18h));
            }
            get
            {
                return Helper.byte_to_uint32_le(_data, _obj_offset + Offsets.OFFSETR_ARM_18h).ToString("X8");
            }
        }

        public string equipped
        {
            get { return Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_ARM_EQUIPPED]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Helper.write_byte(_data, _obj_offset + Offsets.OFFSETR_ARM_EQUIPPED, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed Equipped value - must be at most 0xFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(equipped));
            }
        }

        public string unknown_1ch
        {
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed))
                {
                    Helper.write_uint32_le(_data, _obj_offset + Offsets.OFFSETR_ARM_1C, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed int - must be at most 0xFFFFFFFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(unknown_1ch));
            }
            get
            {
                return Helper.byte_to_uint32_le(_data, _obj_offset + Offsets.OFFSETR_ARM_1C).ToString("X8");
            }
        }

        public static ObservableCollection<Armor> read_all_armors(byte[] save)
        {
            ObservableCollection<Armor> ret = new ObservableCollection<Armor>();
            for (uint i = Offsets.OFFSETA_ARM; i < Offsets.OFFSETA_ARM_END; i += Offsets.SIZE_ARM)
            {
                ret.Add(new Armor(save, i));
            }
            return ret;
        }
    }
}

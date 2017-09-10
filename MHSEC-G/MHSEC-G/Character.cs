using System;
using System.ComponentModel;
using System.Windows;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class Character : InMemoryObject
    {
        public uint level
        {
            get { return Helper.byte_to_uint(_data[Offsets.OFFSETA_CHAR_LEVEL]); }
            set
            {
                if (value <= Offsets.LIMIT_LEVEL)
                {
                    Helper.write_byte(_data, Offsets.OFFSETA_CHAR_LEVEL, value);
                }
                else
                {
                    MessageBox.Show("Level must be less than " + Offsets.LIMIT_LEVEL, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(level));
            }
        }

        public uint exp
        {
            get { return Helper.byte_to_uint32_le(_data, Offsets.OFFSETA_CHAR_EXP); }
            set
            {
                if (value <= Offsets.LIMIT_EXP)
                {
                    Helper.write_uint32_le(_data, Offsets.OFFSETA_CHAR_EXP, value);
                }
                else
                {
                    MessageBox.Show("Exp must be less than " + Offsets.LIMIT_EXP, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(exp));
            }
        }

        public uint money
        {
            get { return Helper.byte_to_uint32_le(_data, Offsets.OFFSETA_CHAR_MONEY); }
            set
            {
                if (value <= Offsets.LIMIT_MONEY)
                {
                    Helper.write_uint32_le(_data, Offsets.OFFSETA_CHAR_MONEY, value);
                }
                else
                {
                    MessageBox.Show("Money must be less than " + Offsets.LIMIT_MONEY, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(money));
            }
        }

        public string name
        {
            get { return Helper.read_unicode_string(_data, Offsets.OFFSETA_CHAR_NAME, Offsets.LENGTH_CHAR_NAME); }
            set
            {
                if (value.Length <= Offsets.LENGTH_CHAR_NAME && value.Length > 0)
                {
                    Helper.write_unicode_string(_data, Offsets.OFFSETA_CHAR_NAME, value, Offsets.LENGTH_CHAR_NAME);
                }
                else
                {
                    MessageBox.Show("Name must be 1-" + Offsets.LENGTH_CHAR_NAME + " characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(name));
            }
        }


        public Character(byte[] model) : base(model, Offsets.OFFSETA_CHAR_NAME, 0)
        {
        }

        public override byte[] toByteArray()
        {
            throw new NotImplementedException();
        }
        public override void setByteArray(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}

using System.ComponentModel;
using System.Windows;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class Character : INotifyPropertyChanged
    {
        private const uint OFFSETA_CHAR_NAME = 0x9DA0;
        private const uint LENGTH_CHAR_NAME = 6;
        private const uint OFFSETA_CHAR_MONEY = 0x5B404;
        private const uint OFFSETA_CHAR_EXP = 0x9E68;
        private const uint OFFSETA_CHAR_LEVEL = 0x9E64;
        public const uint LIMIT_LEVEL = 99;
        public const uint LIMIT_MONEY = 9999999;
        public const uint LIMIT_EXP = 25165822;

        private readonly Model _model;

        public uint level
        {
            get { return Model.byte_to_uint(_model.save_file[OFFSETA_CHAR_LEVEL]); }
            set
            {
                if (value <= LIMIT_LEVEL)
                {
                    Model.write_byte(_model.save_file, OFFSETA_CHAR_LEVEL, value);
                }
                else
                {
                    MessageBox.Show("Level must be less than " + LIMIT_LEVEL, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(level));
            }
        }

        public uint exp
        {
            get { return Model.byte_to_uint32_le(_model.save_file, OFFSETA_CHAR_EXP); }
            set
            {
                if (value <= LIMIT_EXP)
                {
                    Model.write_uint32_le(_model.save_file, OFFSETA_CHAR_EXP, value);
                }
                else
                {
                    MessageBox.Show("Exp must be less than " + LIMIT_EXP, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(exp));
            }
        }

        public uint money
        {
            get { return Model.byte_to_uint32_le(_model.save_file, OFFSETA_CHAR_MONEY); }
            set
            {
                if (value <= LIMIT_MONEY)
                {
                    Model.write_uint32_le(_model.save_file, OFFSETA_CHAR_MONEY, value);
                }
                else
                {
                    MessageBox.Show("Money must be less than " + LIMIT_MONEY, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(money));
            }
        }

        public string name
        {
            get { return Model.read_unicode_string(_model.save_file, OFFSETA_CHAR_NAME, LENGTH_CHAR_NAME); }
            set
            {
                if (value.Length <= LENGTH_CHAR_NAME && value.Length > 0)
                {
                    Model.write_unicode_string(_model.save_file, OFFSETA_CHAR_NAME, value, LENGTH_CHAR_NAME);
                }
                else
                {
                    MessageBox.Show("Name must be 1-" + LENGTH_CHAR_NAME + " characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(name));
            }
        }


        public Character(Model model)
        {
            _model = model;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

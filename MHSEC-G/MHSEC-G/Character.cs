using System.ComponentModel;
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

        private uint _char_level;
        public uint char_level
        {
            get { return _char_level; }
            set { _char_level = value; OnPropertyChanged(nameof(char_level)); }
        }

        private uint _char_exp;
        public uint char_exp
        {
            get { return _char_exp; }
            set { _char_exp = value; OnPropertyChanged(nameof(char_exp)); }
        }

        private uint _char_money;
        public uint char_money
        {
            get { return _char_money; }
            set { _char_money = value; OnPropertyChanged(nameof(char_money)); }
        }

        private string _char_name;
        public string char_name
        {
            get { return _char_name; }
            set { _char_name = value; OnPropertyChanged(nameof(char_name)); }
        }


        public Character(byte[] save_data)
        {
            _char_level = Model.byte_to_uint(save_data[OFFSETA_CHAR_LEVEL]);
            _char_exp = Model.byte_to_uint32_le(save_data, OFFSETA_CHAR_EXP);
            _char_money = Model.byte_to_uint32_le(save_data, OFFSETA_CHAR_MONEY);
            _char_name = Model.read_unicode_string(save_data, OFFSETA_CHAR_NAME, LENGTH_CHAR_NAME);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

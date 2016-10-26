using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class Talisman
    {
        private const uint OFFSETA_TALI = 0x7210;
        private const uint OFFSETA_TALI_END = 0x978F;
        private const uint SIZE_TALI = 0x30;
        private const uint OFFSETR_TALI_RARITY = 0x24;
        private const uint OFFSETR_TALI_NEW = 0x12;
        private const uint OFFSETR_TALI_SKILL1 = 0x28;
        private const uint OFFSETR_TALI_SKILL2 = 0x2A;
        private const uint OFFSETR_TALI_ID = 0x2;
        private const uint OFFSETR_EQUIPPED = 0x11;

        private readonly uint _offset;

        public uint offset
        {
            get { return _offset; }
        }

        private readonly Model _model;

        public Talisman(uint offset, Model model)
        {
            _model = model;
            _offset = offset;
        }

        public string rarity
        {
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_TALI_RARITY]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Model.write_byte(_model.save_file, _offset + OFFSETR_TALI_RARITY, parsed);
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
            get { return Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_TALI_ID).ToString("X4"); }
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Model.write_uint16_le(_model.save_file, _offset + OFFSETR_TALI_ID, parsed);
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
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_TALI_NEW]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0x1)
                {
                    Model.write_byte(_model.save_file, _offset + OFFSETR_TALI_NEW, parsed);
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
            get { return Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_TALI_SKILL1).ToString("X4"); }
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Model.write_uint16_le(_model.save_file, _offset + OFFSETR_TALI_SKILL1, parsed);
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
            get { return Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_TALI_SKILL2).ToString("X4"); }
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Model.write_uint16_le(_model.save_file, _offset + OFFSETR_TALI_SKILL2, parsed);
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
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_EQUIPPED]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Model.write_byte(_model.save_file, _offset + OFFSETR_EQUIPPED, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed Equipped value - must be at most 0xFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(equipped));
            }
        }
        
        public static ObservableCollection<Talisman> read_all_talismans(Model model)
        {
            ObservableCollection<Talisman> ret = new ObservableCollection<Talisman>();
            byte[] buffer = model.save_file;
            for (uint offset = OFFSETA_TALI; offset < OFFSETA_TALI_END; offset += SIZE_TALI)
            {
                if (Model.byte_to_uint16_le(buffer, offset + OFFSETR_TALI_ID)  == 0)
                    continue;
                ret.Add(new Talisman(offset, model));
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


using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class EggFragment : INotifyPropertyChanged
    {
        private const uint OFFSETA_EGG_FRAGMENTS = 0x9790;
        private const uint OFFSETA_EGG_FRAGMENTS_END = 0x9C3F;
        private const uint SIZE_EGG_FRAGMENT = 0xC;
        private const uint OFFSETR_EF_SPE = 0x0;
        private const uint OFFSETR_EF_POS = 0x1;
        private const uint OFFSETR_EF_NEW = 0x2;
        private const uint OFFSETR_EF_RAR = 0x3;
        private const uint OFFSETR_EF_COL = 0x4;
        private const uint OFFSETR_EF_DLC = 0x5;
        private const uint OFFSETR_EF_6H = 0x6;
        private const uint OFFSETR_EF_7H = 0x7;

        private readonly uint _offset;

        public uint offset
        {
            get { return _offset;}
        }
        private readonly Model _model;
        public EggFragment(uint offset, Model model)
        {
            _model = model;
            _offset = offset;
        }

        public string spe
        {
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_EF_SPE]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0x0E)
                {
                    Model.write_byte(_model.save_file, _offset + OFFSETR_EF_SPE, parsed);
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
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_EF_POS]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0x08)
                {
                    Model.write_byte(_model.save_file, _offset + OFFSETR_EF_POS, parsed);
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
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_EF_NEW]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0x01)
                {
                    Model.write_byte(_model.save_file, _offset + OFFSETR_EF_NEW, parsed);
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
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_EF_RAR]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0x01)
                {
                    Model.write_byte(_model.save_file, _offset + OFFSETR_EF_RAR, parsed);
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
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_EF_COL]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Model.write_byte(_model.save_file, _offset + OFFSETR_EF_COL, parsed);
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
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_EF_DLC]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Model.write_byte(_model.save_file, _offset + OFFSETR_EF_DLC, parsed);
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
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_EF_6H]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Model.write_byte(_model.save_file, _offset + OFFSETR_EF_6H, parsed);
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
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_EF_7H]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    Model.write_byte(_model.save_file, _offset + OFFSETR_EF_7H, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed 7h value - must be at most 0xFF", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(unknown_7h));
            }
        }

        public static ObservableCollection<EggFragment> read_all_egg_fragments(Model model)
        {
            ObservableCollection<EggFragment> ret = new ObservableCollection<EggFragment>();
            byte[] buffer = model.save_file;
            for (uint offset = OFFSETA_EGG_FRAGMENTS; offset < OFFSETA_EGG_FRAGMENTS_END; offset += SIZE_EGG_FRAGMENT)
            {
                if(buffer[offset] == 0)
                    continue;
                ret.Add(new EggFragment(offset, model));
            }
            return ret;
        }


        private static EggFragment egg_frag_offset_exist(ObservableCollection<EggFragment> fragments, uint offset)
        {
            for (uint i = 0; i < fragments.Count; i++)
            {
                if(fragments.ElementAt((int)i).offset == offset)
                    return fragments.ElementAt((int)i);
            }
            return null;
        }

        public static void write_dlc_egg_fragment(ObservableCollection<EggFragment> fragments, Model model, uint dlc)
        {
            for (uint offset = OFFSETA_EGG_FRAGMENTS; offset < OFFSETA_EGG_FRAGMENTS + 9*SIZE_EGG_FRAGMENT ; offset += SIZE_EGG_FRAGMENT)
            {
                EggFragment each_frag = egg_frag_offset_exist(fragments, offset);
                if (each_frag == null)
                {
                    each_frag = new EggFragment(offset, model);
                    fragments.Insert((int)((offset - OFFSETA_EGG_FRAGMENTS) / SIZE_EGG_FRAGMENT), each_frag);
                }
                each_frag.new_flag = "0";
                each_frag.spe = "08";
                each_frag.pos = ((offset-OFFSETA_EGG_FRAGMENTS) / SIZE_EGG_FRAGMENT).ToString();
                each_frag.rarity = "0";
                each_frag.color = "0";
                each_frag.dlc = dlc.ToString("X2");
                each_frag.unknown_6h = "0";
                each_frag.unknown_7h = "0";
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Windows;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class Monster : INotifyPropertyChanged
    {
        public const uint LIMIT_MONSTER_EXP = 0xFFFFFF;
        public const uint SIZE_MONSTER = 0x478;
        private const uint OFFSETA_MONSTER = 0xA150;
        private const uint OFFSETR_MONSTER_GENE = 0x424;
        private const uint SIZE_MONSTER_GENE = 0x4;
        private const uint OFFSETR_MONSTER_EXP = 0xE0;
        private const uint OFFSETR_MONSTER_HIV = 0xD8;
        private const uint OFFSETR_MONSTER_AIV = 0xD9;
        private const uint OFFSETR_MONSTER_DIV = 0xDA;
        private const uint OFFSETR_MONSTER_HPU = 0xD4;
        private const uint OFFSETR_MONSTER_APU = 0xD5;
        private const uint OFFSETR_MONSTER_DPU = 0xD6;
        private const uint OFFSETR_MONSTER_SKILL = 0x38;
        private const uint OFFSETR_MONSTER_LEVEL = 0x5C;
        private const uint LIMIT_MONSTER_LEVEL = 99;
        private const uint OFFSETR_MONSTER_NAME = 0;
        private const uint LIMIT_MONSTER_NAME = 10;
        private const uint OFFSETR_MONSTER_SPE = 0x30;
        private const uint OFFSETA_MONSTE_END = 0x4786F;
        private const uint OFFSETR_MONSTER_ATK = 0x48;
        private const uint OFFSETR_MONSTER_HP = 0x46;
        private const uint OFFSETR_MONSTER_DEF = 0x4A;

        private readonly Model _model;

        private readonly uint _offset;
        public uint offset => _offset;
        public uint box => (_offset - OFFSETA_MONSTER) / SIZE_MONSTER / 18 + 1;
        public uint slot => (_offset - OFFSETA_MONSTER) / SIZE_MONSTER % 18 + 1;

        private readonly Genes _genes;
        public Genes genes => _genes;

        public string spe
        {
            get => Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_SPE]).ToString("X2");
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    _model.save_file[_offset + OFFSETR_MONSTER_SPE] = (byte) (parsed & 0xFF);
                }
                else
                {
                    MessageBox.Show("Species must be at most 0xFF.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(spe));
            }
        }

        public uint atk
        {
            get => Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_MONSTER_ATK);
            set
            {
                if (value <= 0xFFFF)
                {
                    Model.write_uint16_le(_model.save_file, _offset + OFFSETR_MONSTER_ATK, value);
                }
                else
                {
                    MessageBox.Show("Atk must be at most " + UInt16.MaxValue, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(atk));
            }
        }

        public uint def
        {
            get => Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_MONSTER_DEF);
            set
            {
                if (value <= 0xFFFF)
                {
                    Model.write_uint16_le(_model.save_file, _offset + OFFSETR_MONSTER_DEF, value);
                }
                else
                {
                    MessageBox.Show("Def must be at most " + UInt16.MaxValue, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(def));
            }
        }

        public uint hp
        {
            get => Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_MONSTER_HP);
            set
            {
                if (value <= 0xFFFF)
                {
                    Model.write_uint16_le(_model.save_file, _offset + OFFSETR_MONSTER_HP, value);
                }
                else
                {
                    MessageBox.Show("HP must be at most " + UInt16.MaxValue, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(hp));
            }
        }

        public uint hiv
        {
            get => Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_HIV]);
            set
            {
                if (value <= 0xFF)
                {
                    _model.save_file[_offset + OFFSETR_MONSTER_HIV] = (byte) (value & 0xFF);
                }
                else
                {
                    MessageBox.Show("IV must be at most 255.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(hiv));
            }
        }

        public uint aiv
        {
            get => Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_AIV]);
            set
            {
                if (value <= 0xFF)
                {
                    _model.save_file[_offset + OFFSETR_MONSTER_AIV] = (byte) (value & 0xFF);
                }
                else
                {
                    MessageBox.Show("IV must be at most 255.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(aiv));
            }
        }

        public uint div
        {
            get => Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_DIV]);
            set
            {
                if (value <= 0xFF)
                {
                    _model.save_file[_offset + OFFSETR_MONSTER_DIV] = (byte) (value & 0xFF);
                }
                else
                {
                    MessageBox.Show("IV must be at most 255.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(div));
            }
        }

        public uint hpu
        {
            get => Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_HPU]);
            set
            {
                if (value <= 0xFF)
                {
                    _model.save_file[_offset + OFFSETR_MONSTER_HPU] = (byte) (value & 0xFF);
                }
                else
                {
                    MessageBox.Show("Power up must be at most 255.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(hpu));
            }
        }

        public uint apu
        {
            get => Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_APU]);
            set
            {
                if (value <= 0xFF)
                {
                    _model.save_file[_offset + OFFSETR_MONSTER_APU] = (byte) (value & 0xFF);
                }
                else
                {
                    MessageBox.Show("Power up must be at most 255.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(apu));
            }
        }

        public uint dpu
        {
            get => Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_DPU]);
            set
            {
                if (value <= 0xFF)
                {
                    _model.save_file[_offset + OFFSETR_MONSTER_DPU] = (byte) (value & 0xFF);
                }
                else
                {
                    MessageBox.Show("Power up must be at most 255.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(dpu));
            }
        }

        public string name
        {
            get => Model.read_unicode_string(_model.save_file, _offset + OFFSETR_MONSTER_NAME, LIMIT_MONSTER_NAME);
            set
            {
                if (value.Length <= 10 && value.Length > 0)
                {
                    Model.write_unicode_string(_model.save_file, _offset + OFFSETR_MONSTER_NAME, value,
                        LIMIT_MONSTER_NAME);
                }
                else
                {
                    MessageBox.Show("Name must be 1-10 characters.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(name));
            }
        }

        public uint exp
        {
            get => Model.byte_to_uint32_le(_model.save_file, _offset + OFFSETR_MONSTER_EXP);
            set
            {
                if (value <= LIMIT_MONSTER_EXP)
                {
                    Model.write_uint32_le(_model.save_file, _offset + OFFSETR_MONSTER_EXP, value);
                }
                else
                {
                    MessageBox.Show("Exp must be at most " + LIMIT_MONSTER_EXP, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(exp));
            }
        }

        public uint level
        {
            get => Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_LEVEL]);
            set
            {
                if (value <= LIMIT_MONSTER_LEVEL)
                {
                    _model.save_file[_offset + OFFSETR_MONSTER_LEVEL] = (byte) (value & 0xFF);
                }
                else
                {
                    MessageBox.Show("Level must be at most " + LIMIT_MONSTER_LEVEL, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(level));
            }
        }

        public string skill
        {
            get => Model.byte_to_uint16_le(_model.save_file, offset + OFFSETR_MONSTER_SKILL).ToString("X4");
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed))
                {
                    Model.write_uint16_le(_model.save_file, offset + OFFSETR_MONSTER_SKILL, parsed);
                }
                else
                {
                    MessageBox.Show("Malformed skill value - must be 0x0 to 0xFFFF.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                OnPropertyChanged(nameof(skill));
            }
        }
       

        public Monster(uint offset, Model model)
        {
            _offset = offset;
            _model = model;
            _genes = new Genes(model, offset + OFFSETR_MONSTER_GENE, SIZE_MONSTER_GENE);
        }

        public static List<Monster> read_all_monsters(Model model)
        {
            byte[] save = model.save_file;
            List<Monster> ret = new List<Monster>();
            for (uint i = OFFSETA_MONSTER; i < OFFSETA_MONSTE_END; i += SIZE_MONSTER)
            {
                ret.Add(new Monster(i, model));
            }
            return ret;
        }

        public byte[] getByteArray()
        {
            byte[] ret = new byte[SIZE_MONSTER];
            Array.Copy(_model.save_file, _offset, ret, 0, SIZE_MONSTER);
            return ret;
        }

        public void setByteArray(byte[] ret)
        {
            Array.Copy(ret, 0, _model.save_file, _offset, SIZE_MONSTER);
            OnPropertyChanged(null);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

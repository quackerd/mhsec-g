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
    public class Monster : InMemoryObject
    {
        public uint box => (_obj_offset - Offsets.OFFSETA_MONSTER) / Offsets.SIZE_MONSTER / 18 + 1;
        public uint slot => (_obj_offset - Offsets.OFFSETA_MONSTER) / Offsets.SIZE_MONSTER % 18 + 1;

        private readonly Genes _genes;
        public Genes genes => _genes;

        public string spe
        {
            get => Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_MONSTER_SPE]).ToString("X2");
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    _data[_obj_offset + Offsets.OFFSETR_MONSTER_SPE] = (byte) (parsed & 0xFF);
                }
                else
                {
                    MessageBox.Show("Species must be at most 0xFF.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(spe));
            }
        }

        public string uid
        {
            get => Helper.byte_to_uint16_le(_data, _obj_offset + Offsets.OFFSETR_MONSTER_UID).ToString("X4");
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFFFF)
                {
                    Helper.write_uint16_le(_data, _obj_offset + Offsets.OFFSETR_MONSTER_UID, parsed);
                }
                else
                {
                    MessageBox.Show("UID must be at most 0xFFFF", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(uid));
            }
        }


        public uint atk
        {
            get => Helper.byte_to_uint16_le(_data, _obj_offset + Offsets.OFFSETR_MONSTER_ATK);
            set
            {
                if (value <= 0xFFFF)
                {
                    Helper.write_uint16_le(_data, _obj_offset + Offsets.OFFSETR_MONSTER_ATK, value);
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
            get => Helper.byte_to_uint16_le(_data, _obj_offset + Offsets.OFFSETR_MONSTER_DEF);
            set
            {
                if (value <= 0xFFFF)
                {
                    Helper.write_uint16_le(_data, _obj_offset + Offsets.OFFSETR_MONSTER_DEF, value);
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
            get => Helper.byte_to_uint16_le(_data, _obj_offset + Offsets.OFFSETR_MONSTER_HP);
            set
            {
                if (value <= 0xFFFF)
                {
                    Helper.write_uint16_le(_data, _obj_offset + Offsets.OFFSETR_MONSTER_HP, value);
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
            get => Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_MONSTER_HIV]);
            set
            {
                if (value <= 0xFF)
                {
                    _data[_obj_offset + Offsets.OFFSETR_MONSTER_HIV] = (byte) (value & 0xFF);
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
            get => Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_MONSTER_AIV]);
            set
            {
                if (value <= 0xFF)
                {
                    _data[_obj_offset + Offsets.OFFSETR_MONSTER_AIV] = (byte) (value & 0xFF);
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
            get => Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_MONSTER_DIV]);
            set
            {
                if (value <= 0xFF)
                {
                    _data[_obj_offset + Offsets.OFFSETR_MONSTER_DIV] = (byte) (value & 0xFF);
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
            get => Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_MONSTER_HPU]);
            set
            {
                if (value <= 0xFF)
                {
                    _data[_obj_offset + Offsets.OFFSETR_MONSTER_HPU] = (byte) (value & 0xFF);
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
            get => Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_MONSTER_APU]);
            set
            {
                if (value <= 0xFF)
                {
                    _data[_obj_offset + Offsets.OFFSETR_MONSTER_APU] = (byte) (value & 0xFF);
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
            get => Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_MONSTER_DPU]);
            set
            {
                if (value <= 0xFF)
                {
                    _data[_obj_offset + Offsets.OFFSETR_MONSTER_DPU] = (byte) (value & 0xFF);
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
            get => Helper.read_unicode_string(_data, _obj_offset + Offsets.OFFSETR_MONSTER_NAME, Offsets.LIMIT_MONSTER_NAME);
            set
            {
                if (value.Length <= 10 && value.Length > 0)
                {
                    Helper.write_unicode_string(_data, _obj_offset + Offsets.OFFSETR_MONSTER_NAME, value,
                        Offsets.LIMIT_MONSTER_NAME);
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
            get => Helper.byte_to_uint32_le(_data, _obj_offset + Offsets.OFFSETR_MONSTER_EXP);
            set
            {
                if (value <= Offsets.LIMIT_MONSTER_EXP)
                {
                    Helper.write_uint32_le(_data, _obj_offset + Offsets.OFFSETR_MONSTER_EXP, value);
                }
                else
                {
                    MessageBox.Show("Exp must be at most " + Offsets.LIMIT_MONSTER_EXP, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(exp));
            }
        }

        public uint level
        {
            get => Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_MONSTER_LEVEL]);
            set
            {
                if (value <= Offsets.LIMIT_MONSTER_LEVEL)
                {
                    _data[_obj_offset + Offsets.OFFSETR_MONSTER_LEVEL] = (byte) (value & 0xFF);
                }
                else
                {
                    MessageBox.Show("Level must be at most " + Offsets.LIMIT_MONSTER_LEVEL, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(level));
            }
        }

        public string skill1
        {
            get => Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_MONSTER_SKILL]).ToString("X2");
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    _data[_obj_offset + Offsets.OFFSETR_MONSTER_SKILL] = (byte) (parsed & 0xFF);
                }
                else
                {
                    MessageBox.Show("Malformed skill value - must be <= 0xFF.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                OnPropertyChanged(nameof(skill1));
            }
        }

        public string skill2
        {
            get => Helper.byte_to_uint(_data[_obj_offset + Offsets.OFFSETR_MONSTER_SKILL + 1]).ToString("X2");
            set
            {
                uint parsed;
                if (Helper.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    _data[_obj_offset + Offsets.OFFSETR_MONSTER_SKILL + 1] = (byte)(parsed & 0xFF);
                }
                else
                {
                    MessageBox.Show("Malformed skill value - must be <= 0xFF.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                OnPropertyChanged(nameof(skill2));
            }
        }


        public Monster(byte[] model, uint objOffset) : base(model, objOffset, Offsets.SIZE_MONSTER)
        {
            _genes = new Genes(model, objOffset + Offsets.OFFSETR_MONSTER_GENE, Offsets.SIZE_MONSTER_GENE);
        }

        public static List<Monster> read_all_monsters(byte[] model)
        {
            byte[] save = model;
            List<Monster> ret = new List<Monster>();
            for (uint i = Offsets.OFFSETA_MONSTER; i < Offsets.OFFSETA_MONSTE_END; i += Offsets.SIZE_MONSTER)
            {
                ret.Add(new Monster(model, i));
            }
            return ret;
        }
    }
}

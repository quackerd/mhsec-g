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
        public static readonly List<uint> GENE_ID = new List<uint>();
        public static readonly List<string> GENE_NAME = new List<string>();

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

        public uint offset
        {
            get { return _offset; }
        }

        public string spe
        {
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_SPE]).ToString("X2"); }
            set
            {
                uint parsed;
                if (Model.parse_hex_string(value, out parsed) && parsed <= 0xFF)
                {
                    _model.save_file[_offset + OFFSETR_MONSTER_SPE] = (byte) (parsed & 0xFF);
                }
                else
                {
                    MessageBox.Show("Species must be at most 0xFF.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(spe));
            }
        }

        public uint atk
        {
            get { return Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_MONSTER_ATK); }
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
            get { return Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_MONSTER_DEF); }
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
            get { return Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_MONSTER_HP); }
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
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_HIV]); }
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
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_AIV]); }
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
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_DIV]); }
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
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_HPU]); }
            set
            {
                if (value <= 0xFF)
                {
                    _model.save_file[_offset + OFFSETR_MONSTER_HPU] = (byte) (value & 0xFF);
                }
                else
                {
                    MessageBox.Show("Power up must be at most 255.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(hpu));
            }
        }

        public uint apu
        {
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_APU]); }
            set
            {
                if (value <= 0xFF)
                {
                    _model.save_file[_offset + OFFSETR_MONSTER_APU] = (byte) (value & 0xFF);
                }
                else
                {
                    MessageBox.Show("Power up must be at most 255.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(apu));
            }
        }

        public uint dpu
        {
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_DPU]); }
            set
            {
                if (value <= 0xFF)
                {
                    _model.save_file[_offset + OFFSETR_MONSTER_DPU] = (byte) (value & 0xFF);
                }
                else
                {
                    MessageBox.Show("Power up must be at most 255.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(dpu));
            }
        }

        public string full_name
        {
            get { return name.Length == 0 ? "" : name + " [Lv." + level + "]"; }
        }

        public string name
        {
            get
            {
                return Model.read_unicode_string(_model.save_file, _offset + OFFSETR_MONSTER_NAME, LIMIT_MONSTER_NAME);
            }
            set
            {
                if (value.Length <= 10 && value.Length > 0)
                {
                    Model.write_unicode_string(_model.save_file, _offset + OFFSETR_MONSTER_NAME, value,
                        LIMIT_MONSTER_NAME);
                }
                else
                {
                    MessageBox.Show("Name must be 1-10 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(name));
                OnPropertyChanged(nameof(full_name));
            }
        }

        public uint exp
        {
            get { return Model.byte_to_uint32_le(_model.save_file, _offset + OFFSETR_MONSTER_EXP); }
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
            get { return Model.byte_to_uint(_model.save_file[_offset + OFFSETR_MONSTER_LEVEL]); }
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
                OnPropertyChanged(nameof(full_name));
            }
        }

        // 
        // Genes
        //

        private uint extract_gene(uint gene_idx)
        {
            if (gene_idx >= 9)
            {
                BugCheck.bug_check(BugCheck.ErrorCode.MON_GENE_IDX_OVERFLOW, "Invalid gene index: " + gene_idx);
            }
            return Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_MONSTER_GENE + gene_idx*SIZE_MONSTER_GENE);
        }

        private void set_gene_str(uint gene_idx, string val)
        {
            uint parsed;
            if (Model.parse_hex_string(val, out parsed))
            {
                set_gene(gene_idx, parsed);
            }
            else
            {
                MessageBox.Show("Malformed gene value - must be 0x0 to 0xFFFF.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void set_gene(uint gene_idx, uint val)
        {
            if (gene_idx >= 9)
            {
                BugCheck.bug_check(BugCheck.ErrorCode.MON_GENE_IDX_OVERFLOW, "Invalid gene index: " + gene_idx);
            }
            if (val <= 0xFFFF)
            {
                Model.write_uint16_le(_model.save_file, _offset + OFFSETR_MONSTER_GENE + gene_idx * SIZE_MONSTER_GENE,
                    val);
            }
            else
            {
                MessageBox.Show("Malformed gene value - must be 0x0 to 0xFFFF.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private int extract_gene_idx(uint gene_idx)
        {
            uint gene_id = extract_gene(gene_idx);
            int idx = GENE_ID.IndexOf(gene_id);
            if (idx == -1)
            {
                // unknown
                return GENE_NAME.Count - 1;
            }
            return idx;
        }

        private void set_gene_idx(uint gene_idx, int val)
        {
            if (val != -1 && val != GENE_NAME.Count - 1)
            {
                set_gene(gene_idx, GENE_ID.ElementAt(val));
            }
        }

        public int gene1_selected
        {
            get { return extract_gene_idx(0); }
            set
            {
                set_gene_idx(0, value);
                OnPropertyChanged(nameof(gene1));
                OnPropertyChanged(nameof(gene1_selected));
            }
        }

        public string gene1
        {
            get { return extract_gene(0).ToString("X4"); }
            set
            {
                set_gene_str(0, value);
                OnPropertyChanged(nameof(gene1));
                OnPropertyChanged(nameof(gene1_selected));
            }
        }

        public int gene2_selected
        {
            get { return extract_gene_idx(1); }
            set
            {
                set_gene_idx(1, value);
                OnPropertyChanged(nameof(gene2));
                OnPropertyChanged(nameof(gene2_selected));
            }
        }

        public string gene2
        {
            get { return extract_gene(1).ToString("X4"); }
            set
            {
                set_gene_str(1, value);
                OnPropertyChanged(nameof(gene2));
                OnPropertyChanged(nameof(gene2_selected));
            }
        }

        public int gene3_selected
        {
            get { return extract_gene_idx(2); }
            set
            {
                set_gene_idx(2, value);
                OnPropertyChanged(nameof(gene3));
                OnPropertyChanged(nameof(gene3_selected));
            }
        }


        public string gene3
        {
            get { return extract_gene(2).ToString("X4"); }
            set
            {
                set_gene_str(2, value);
                OnPropertyChanged(nameof(gene3));
                OnPropertyChanged(nameof(gene3_selected));
            }
        }

        public int gene4_selected
        {
            get { return extract_gene_idx(3); }
            set
            {
                set_gene_idx(3, value);
                OnPropertyChanged(nameof(gene4));
                OnPropertyChanged(nameof(gene4_selected));
            }
        }


        public string gene4
        {
            get { return extract_gene(3).ToString("X4"); }
            set
            {
                set_gene_str(3, value);
                OnPropertyChanged(nameof(gene4));
                OnPropertyChanged(nameof(gene4_selected));
            }
        }

        public int gene5_selected
        {
            get { return extract_gene_idx(4); }
            set
            {
                set_gene_idx(4, value);
                OnPropertyChanged(nameof(gene5));
                OnPropertyChanged(nameof(gene5_selected));
            }
        }


        public string gene5
        {
            get { return extract_gene(4).ToString("X4"); }
            set
            {
                set_gene_str(4, value);
                OnPropertyChanged(nameof(gene5));
                OnPropertyChanged(nameof(gene5_selected));
            }
        }

        public int gene6_selected
        {
            get { return extract_gene_idx(5); }
            set
            {
                set_gene_idx(5, value);
                OnPropertyChanged(nameof(gene6));
                OnPropertyChanged(nameof(gene6_selected));
            }
        }


        public string gene6
        {
            get { return extract_gene(5).ToString("X4"); }
            set
            {
                set_gene_str(5, value);
                OnPropertyChanged(nameof(gene6));
                OnPropertyChanged(nameof(gene6_selected));
            }
        }


        public int gene7_selected
        {
            get { return extract_gene_idx(6); }
            set
            {
                set_gene_idx(6, value);
                OnPropertyChanged(nameof(gene7));
                OnPropertyChanged(nameof(gene7_selected));
            }
        }


        public string gene7
        {
            get { return extract_gene(6).ToString("X4"); }
            set
            {
                set_gene_str(6, value);
                OnPropertyChanged(nameof(gene7));
                OnPropertyChanged(nameof(gene7_selected));
            }
        }


        public int gene8_selected
        {
            get { return extract_gene_idx(7); }
            set
            {
                set_gene_idx(7, value);
                OnPropertyChanged(nameof(gene8));
                OnPropertyChanged(nameof(gene8_selected));
            }
        }


        public string gene8
        {
            get { return extract_gene(7).ToString("X4"); }
            set
            {
                set_gene_str(7, value);
                OnPropertyChanged(nameof(gene8));
                OnPropertyChanged(nameof(gene8_selected));
            }
        }


        public int gene9_selected
        {
            get { return extract_gene_idx(8); }
            set
            {
                set_gene_idx(8, value);
                OnPropertyChanged(nameof(gene9));
                OnPropertyChanged(nameof(gene9_selected));
            }
        }


        public string gene9
        {
            get { return extract_gene(8).ToString("X4"); }
            set
            {
                set_gene_str(8, value);
                OnPropertyChanged(nameof(gene9));
                OnPropertyChanged(nameof(gene9_selected));
            }
        }

        public Monster(uint offset, Model model)
        {
            _offset = offset;
            _model = model;
        }

        public static List<Monster> read_all_monsters(Model model)
        {
            byte[] save = model.save_file;
            List<Monster> ret = new List<Monster>();
            for (uint i = OFFSETA_MONSTER; i < OFFSETA_MONSTE_END; i += SIZE_MONSTER)
            {
                if (save[i] != 0)
                {
                    byte[] each = new byte[SIZE_MONSTER];
                    Array.Copy(save, i, each, 0, SIZE_MONSTER);
                    ret.Add(new Monster(i, model));
                }
            }

            if (ret.Count == 0)
            {
                byte[] dummy = new byte[SIZE_MONSTER];
                Array.Clear(dummy, 0, (int) SIZE_MONSTER);
                // at least one monster
                ret.Add(new Monster(OFFSETA_MONSTER, model));
            }
            return ret;
        }


        public static void read_gene_mapping()
        {
            string line;
            StringReader file = new StringReader(Properties.Resources.gene);

            while ((line = file.ReadLine()) != null)
            {
                if (line.Length == 0)
                    continue;

                string[] eachline = line.Split('\t');
                if (eachline.Length != 2)
                {
                    BugCheck.bug_check(BugCheck.ErrorCode.MON_GENE_MAPPING_CORRUPTED,
                        "Invalid gene mapping file line:\n" + line);
                }

                GENE_ID.Add(uint.Parse(eachline[0], System.Globalization.NumberStyles.HexNumber));
                GENE_NAME.Add(eachline[1]);
            }
            GENE_NAME.Add("Custom");

            file.Close();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
